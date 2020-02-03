using System;

namespace PuppyApi.Models
{
    public class FeedingEvent
    {
        public DateTime DateTime { get; set; }

        public bool IsBreakfast { get; set; }

        public bool IsDinner { get; set; }

        public bool IsOther { get; set; }

        public string Comment { get; set; }
    }
}
