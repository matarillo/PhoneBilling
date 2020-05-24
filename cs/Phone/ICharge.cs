using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface ICharge
    {
        string Description { get; set; }
        int Fee { get; set; }
        void Accept(IChargeVisitor visitor);
    }
}
