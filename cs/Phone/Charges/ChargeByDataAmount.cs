using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Charges
{
    class ChargeByDataAmount : UsageCharge<IUsageByDataAmount>
    {
        public override void Accept(IChargeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
