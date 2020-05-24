using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phone;
using Phone.Charges;

namespace Phone.Services
{
    /// <summary>
    /// 継続利用割引
    /// </summary>
    public class LongTermDiscount : Service, IChargeVisitor
    {
        private static readonly int[] PERCENTAGES = { 0, 7, 8, 10, 12, 15 };

        public override string Name
        {
            get { return "継続利用割引"; }
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
            charge.Description = string.Format("{0} (継続利用割引 {1}%)", charge.Description, PERCENTAGES[years]);
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
