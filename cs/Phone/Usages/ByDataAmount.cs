using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Usages
{
    class ByDataAmount : IUsageByDataAmount
    {
        public DateTime DateTime { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public MessageAreaType MessageArea { get; set; }
        public string Destination { get; set; }

        public ICharge CreateCharge()
        {
            return new Charges.ChargeByDataAmount { Usage = this };
        }
    }
}
