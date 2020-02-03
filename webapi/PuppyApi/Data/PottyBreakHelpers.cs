using Microsoft.WindowsAzure.Storage.Table;
using PuppyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyApi.Data
{
    public static class PottyBreakHelpers
    {
        private const string POTTYBREAK_PARTITIONKEY = "potty";

        public static string PottyBreakPartitionKey => PottyBreakPartitionKey;

        public static PottyBreak AsPottyBreak(this DynamicTableEntity entity)
        {
            return new PottyBreak
            {
                Id = Guid.Parse(entity.RowKey),
                DateTime = entity.Properties["datetime"].DateTime.Value,
                Peed = entity.Properties["peed"].BooleanValue.Value,
                Pooed = entity.Properties["pooed"].BooleanValue.Value,
                Comment = entity.Properties["comment"].StringValue
            };
        }

        public static DynamicTableEntity AsDynamicTableEntity(this PottyBreak pottyBreak)
        {
            var entity = new DynamicTableEntity
            {
                ETag = "*",
                PartitionKey = POTTYBREAK_PARTITIONKEY,
                RowKey = pottyBreak.Id.ToString(),
            };

            entity.Properties.Add("datetime", new EntityProperty(pottyBreak.DateTime));
            entity.Properties.Add("peed", new EntityProperty(pottyBreak.Peed));
            entity.Properties.Add("pooed", new EntityProperty(pottyBreak.Pooed));
            entity.Properties.Add("comment", new EntityProperty(pottyBreak.Comment));

            return entity;
        }
    }
}
