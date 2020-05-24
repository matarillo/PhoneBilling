using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Charges
{
    class BasicCharge : IBasicCharge
    {
        public string Description { get; set; }
        public DateTime YearMonth { get; set; }
        public int DiscountRate { get; set; }
        public int UnitPrice { get; set; }
        public int Fee
        {
            get
            {
                return (this.UnitPrice * (100 - this.DiscountRate)) / 100;
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        public void Accept(IChargeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
