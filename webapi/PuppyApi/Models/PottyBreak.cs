using System;

namespace PuppyApi.Models
{
    public class PottyBreak
    {
        public DateTime DateTime { get; set; }

        public bool Peed { get; set; }

        public bool Pooed { get; set; }

        public string Comment { get; set; }
    }
}
