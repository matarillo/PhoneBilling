using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Services
{
    /// <summary>
    /// ひとりでも割50
    /// </summary>
    public class DiscountForTwoYears : Service
    {
        private const int PERCENTAGE = 50;

        public override string Name
        {
            get { return "ひとりでも割50"; }
        }
        public int Months
        {
            get { return 24; }
        }

        public void Apply(IBasicCharge charge)
        {
            charge.DiscountRate = PERCENTAGE;
            charge.Description = string.Format("{0} ({1}適用により{2}%割引)", charge.Description, this.Name, charge.DiscountRate);
        }
    }
}
