using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface IPlan : IService
    {
        IBasicCharge CreateBasicCharge(int year, int month);
    }
}
