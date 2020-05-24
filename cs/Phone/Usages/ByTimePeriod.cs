using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Usages
{
    class ByTimePeriod : IUsageByTimePeriod
    {
        public DateTime DateTime { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public MessageAreaType MessageArea { get; set; }
        public MessageAreaType TargetMessageArea { get; set; }
        public string Number { get; set; }

        public ICharge CreateCharge()
        {
            return new Charges.ChargeByTimePeriod { Usage = this };
        }
    }
}
