using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using PuppyApi.Domain.Contracts.Repositories;
using PuppyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyApi.Data.AzureStorage
{
    public class PottyBreakRepository : IPottyBreakRepository
    {
        private readonly CloudTable _tableReference;

        public PottyBreakRepository(IConfiguration configuration)
        {
            var storageConnectionString = configuration["AzureStorage:ConnectionString"];
            
            if (string.IsNullOrEmpty(storageConnectionString))
                throw new ArgumentNullException(nameof(storageConnectionString));

            var cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient         = cloudStorageAccount.CreateCloudTableClient();

            _tableReference         = tableClient.GetTableReference("puppytrackertable");
        }

        public async Task InitializeAsync()
        {
            await _tableReference.CreateIfNotExistsAsync();
        }

        public async Task DeleteAsync(PottyBreak pottyBreak)
        {
            var tableEntity     = pottyBreak.AsDynamicTableEntity();
            var deleteOperation = TableOperation.Delete(tableEntity);
            var tableResult     = await _tableReference.ExecuteAsync(deleteOperation);

            if(tableResult.HttpStatusCode >= 200 && tableResult.HttpStatusCode < 300)
            {
                // What to do
            }            
        }

        public async Task<IEnumerable<PottyBreak>> GetAllAsync(int max)
        {
            var query        = new TableQuery<DynamicTableEntity>().Take(max);
            var token        = new TableContinuationToken();
            var totalEntries = new List<DynamicTableEntity>();

            do
            {
                var page = await _tableReference.ExecuteQuerySegmentedAsync(query, token);
                token = page.ContinuationToken;
                totalEntries.AddRange(page.Results);

            } while (token is { } && totalEntries.Count < max);

            return totalEntries.Select(entity => entity.AsPottyBreak()).ToList();
        }

        public async Task<PottyBreak> GetById(Guid verifiedGuid)
        {
            var retrieveOperation = TableOperation.Retrieve<DynamicTableEntity>(PottyBreakHelpers.PottyBreakPartitionKey, verifiedGuid.ToString());
            var executeResult     = await _tableReference.ExecuteAsync(retrieveOperation);

            if (executeResult == null)
                return null;

            var entity = (DynamicTableEntity)executeResult.Result;

            return entity.AsPottyBreak();
        }

        public async Task SaveAsync(PottyBreak pottyBreak)
        {
            var entity          = pottyBreak.AsDynamicTableEntity();
            var insertOperation = TableOperation.InsertOrReplace(entity);

            await _tableReference.ExecuteAsync(insertOperation);
        }
    }
}
