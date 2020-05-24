using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface IUsage
    {
        DateTime DateTime { get; }
        int Amount { get; }
        string Type { get; }
        MessageAreaType MessageArea { get; }
        ICharge CreateCharge();
    }
}
