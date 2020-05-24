using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface IUsageByNumber : IUsage
    {
        string Name { get; }
        int UnitPrice { get; }
    }
}
