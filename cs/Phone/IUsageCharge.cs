using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface IUsageCharge<T> : ICharge
        where T : IUsage
    {
        T Usage { get; }
        int UnitAmount { get; set; }
        int UnitPrice { get; set; }
    }
}
