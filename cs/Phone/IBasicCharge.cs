using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface IBasicCharge : ICharge
    {
        DateTime YearMonth { get; set; }
        int DiscountRate { get; set; }
        int UnitPrice { get; set; }
    }
}
