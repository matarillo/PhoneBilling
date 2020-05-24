using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Usages
{
    class ByNumber : IUsageByNumber
    {
        public DateTime DateTime { get; set; }
        public int Amount
        {
            get
            {
                // 1インスタンス = 1回の利用
                return 1;
            }
        }
        public string Type { get; set; }
        public MessageAreaType MessageArea { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }

        public ICharge CreateCharge()
        {
            return new Charges.ChargeByNumber { Usage = this };
        }
    }
}
