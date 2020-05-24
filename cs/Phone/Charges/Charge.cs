using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Charges
{
    class Charge : ICharge
    {
        public string Description { get; set; }

        public int Fee { get; set; }

        public void Accept(IChargeVisitor visitor)
        {
        }
    }
}
