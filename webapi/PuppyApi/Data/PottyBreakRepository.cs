﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PuppyApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyApi.Data
{
    public class PottyBreakRepository : IPottyBreakRepository
    {
        private readonly CloudTable _tableReference;

        public PottyBreakRepository()
        {
            var storageConnectionString = Environment.GetEnvironmentVariable("StorageConnectionString");

            //TODO: Hack! fix later
            if(string.IsNullOrEmpty(storageConnectionString))
                storageConnectionString = GetStorageConnectionStringFromFile();
            
            var cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = cloudStorageAccount.CreateCloudTableClient();
            _tableReference = tableClient.GetTableReference("puppytrackertable");
        }

        public async Task InitializeAsync()
        {
            await _tableReference.CreateIfNotExistsAsync();
        }

        public async Task DeleteAsync(PottyBreak pottyBreak)
        {
            var tableEntity = pottyBreak.AsDynamicTableEntity();
            var deleteOperation = TableOperation.Delete(tableEntity);

            await _tableReference.ExecuteAsync(deleteOperation);
        }

        public async Task<IEnumerable<PottyBreak>> GetAllAsync()
        {
            var query        = new TableQuery<DynamicTableEntity>();
            var token        = new TableContinuationToken();
            var totalEntries = new List<DynamicTableEntity>();
            do
            {
                var page = await _tableReference.ExecuteQuerySegmentedAsync(query, token);
                token = page.ContinuationToken;
                totalEntries.AddRange(page.Results);

            } while (token != null);

            return totalEntries.Select(entity => entity.AsPottyBreak());
        }

        public async Task<PottyBreak> GetById(Guid verifiedGuid)
        {

            var retrieveOperation = TableOperation.Retrieve<DynamicTableEntity>(PottyBreakHelpers.PottyBreakPartitionKey, verifiedGuid.ToString());
            var executeResult     = await _tableReference.ExecuteAsync(retrieveOperation);
            
            if (executeResult == null)
                return null;

            if (executeResult.Result is DynamicTableEntity == false)
                return null;

            var entity = (DynamicTableEntity)executeResult.Result;

            return entity.AsPottyBreak();
        }

        public async Task SaveAsync(PottyBreak pottyBreak)
        {
            var entity = pottyBreak.AsDynamicTableEntity();
            var insertOperation = TableOperation.InsertOrReplace(entity);

            await _tableReference.ExecuteAsync(insertOperation);
        }

        private static string GetStorageConnectionStringFromFile()
        {            
            var myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fileName = Path.Combine(myDocumentsFolder, "puppyTrackerSettings.txt");
         
            if (!File.Exists(fileName))
                throw new InvalidProgramException("Unable to locate storage connection string");
            
            return File.ReadAllText(fileName);            
        }

    }
}