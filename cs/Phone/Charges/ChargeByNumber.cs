using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Charges
{
    class ChargeByNumber : UsageCharge<IUsageByNumber>
    {
        public override void Accept(IChargeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
