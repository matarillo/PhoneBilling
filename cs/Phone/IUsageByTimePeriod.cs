using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface IUsageByTimePeriod : IUsage
    {
        string Number { get; }
        MessageAreaType TargetMessageArea { get; }
    }
}
