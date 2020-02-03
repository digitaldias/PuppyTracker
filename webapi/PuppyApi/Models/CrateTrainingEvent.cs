using System;

namespace PuppyApi.Models
{
    public class CrateTrainingEvent
    {
        public DateTime WentIn { get; set; }

        public DateTime WentOut { get; set; }

        public string Comment { get; set; }
    }
}
