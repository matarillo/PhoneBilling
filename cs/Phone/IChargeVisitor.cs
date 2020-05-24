
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface IChargeVisitor
    {
        void Visit(IBasicCharge charge);
        void Visit(IUsageCharge<IUsageByTimePeriod> charge);
        void Visit(IUsageCharge<IUsageByDataAmount> charge);
        void Visit(IUsageCharge<IUsageByNumber> charge);
    }
}
