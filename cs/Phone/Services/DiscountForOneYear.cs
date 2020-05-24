using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Services
{
    /// <summary>
    /// （新）いちねん割引
    /// </summary>
    public class DiscountForOneYear : Service, IChargeVisitor
    {
        private static readonly int[] PERCENTAGES = { 10, 12, 14, 16, 18, 20, 21, 22, 23, 24, 25 };

        public override string Name
        {
            get { return "（新）いちねん割引"; }
        }
        public int Months
        {
            get { return 12; }
        }

        public override void Filter(ICharge charge)
        {
            charge.Accept(this);
        }

        public override bool Consume(ICharge charge)
        {
            return false;
        }

        public override IEnumerable<ICharge> GetCharges()
        {
            yield break;
        }

        public void Visit(IBasicCharge charge)
        {
            int months = (charge.YearMonth.Year * 12 + charge.YearMonth.Month) - (this.BeginningDate.Year * 12 + this.BeginningDate.Month);
            int years = months / 12;
            if (years >= PERCENTAGES.Length)
            {
                years = PERCENTAGES.Length - 1;
            }
            charge.DiscountRate = PERCENTAGES[years];
            charge.Description = string.Format("{0} ({1}適用により{2}%割引)", charge.Description, this.Name, charge.DiscountRate);
        }

        public void Visit(IUsageCharge<IUsageByTimePeriod> charge)
        {
        }

        public void Visit(IUsageCharge<IUsageByDataAmount> charge)
        {
        }

        public void Visit(IUsageCharge<IUsageByNumber> charge)
        {
        }
    }
}
