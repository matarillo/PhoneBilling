using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Charges
{
    class ChargeByTimePeriod : UsageCharge<IUsageByTimePeriod>
    {
        public override void Accept(IChargeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
