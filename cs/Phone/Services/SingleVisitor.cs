
namespace Phone.Services
{
    abstract class SingleVisitor<T> : IChargeVisitor where T : ICharge
    {

        public void Visit(IBasicCharge charge)
        {
            if (charge is T)
            {
                Dispatch((T)charge);
            }
        }

        public void Visit(IUsageCharge<IUsageByTimePeriod> charge)
        {
            if (charge is T)
            {
                Dispatch((T)charge);
            }
        }

        public void Visit(IUsageCharge<IUsageByDataAmount> charge)
        {
            if (charge is T)
            {
                Dispatch((T)charge);
            }
        }

        public void Visit(IUsageCharge<IUsageByNumber> charge)
        {
            if (charge is T)
            {
                Dispatch((T)charge);
            }
        }

        public abstract void Dispatch(T charge);
    }
}
