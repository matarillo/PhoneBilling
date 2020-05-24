using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface IService
    {
        DateTime BeginningDate { get; }
        DateTime EndDate { get; }
        string Name { get; }

        void Filter(ICharge charge);
        bool Consume(ICharge charge);
        IEnumerable<ICharge> GetCharges();
    }
}
