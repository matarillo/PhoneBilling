using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface IUsageByDataAmount : IUsage
    {
        string Destination { get; }
    }
}
