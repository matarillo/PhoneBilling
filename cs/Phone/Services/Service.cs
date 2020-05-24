using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Services
{
    public abstract class Service : IService
    {
        public DateTime BeginningDate { get; set; }
        public DateTime EndDate { get; set; }

        public abstract string Name { get; }
        public abstract void Filter(ICharge charge);
        public abstract bool Consume(ICharge charge);
        public abstract IEnumerable<ICharge> GetCharges();
    }
}
