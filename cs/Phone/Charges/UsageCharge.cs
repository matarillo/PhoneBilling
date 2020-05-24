using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Charges
{
    abstract class UsageCharge<T> : IUsageCharge<T> where T : IUsage
    {
        public T Usage { get; set; }
        public int UnitAmount { get; set; }
        public int UnitPrice { get; set; }
        public string Description { get; set; }
        public int Fee { get; set; }
        public abstract void Accept(IChargeVisitor visitor);
    }
}
