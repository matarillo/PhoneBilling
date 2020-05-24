using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phone.Usages;

namespace Phone.Services
{
    /// <summary>
    /// ファミ割MAX50
    /// </summary>
    public class FamilyDiscountForTwoYears : Service
    {
        private const int PERCENTAGE = 50;

        public override string Name
        {
            get { return "ファミ割MAX50"; }
        }
        public int Months
        {
            get { return 24; }
        }
        public IList<string> FamilyPhoneNumbers { get; set; }
        public IList<string> FamilyMailAddresses { get; set; }

        public void Apply(IBasicCharge charge)
        {
            charge.DiscountRate = PERCENTAGE;
            charge.Description = string.Format("{0} ({1}適用により{2}%割引)", charge.Description, this.Name, charge.DiscountRate);
        }

        public void Visit(IUsageCharge<IUsageByTimePeriod> charge)
        {
        	var usage = charge.Usage;
            if (this.FamilyPhoneNumbers.Any(n => n == usage.Number))
            {
                switch (usage.Type)
                {
                    case "voice":
                        charge.UnitAmount = 0;
                        charge.Description = string.Format("{0} ({1}適用により無料)", charge.Description);
                        break;
                    case "digital":
                        // 単価を割引すると誤差が出るので課金単位時間を伸ばす
                        // 単価 - 60% => 単価 * 4/10 => 単位時間 * 10/4
                        charge.UnitPrice = charge.UnitAmount * 10 / 4;
                        charge.Description = string.Format("{0} ({1}適用により60%割引)", charge.Description);
                        break;
                    default:
                        break;
                }
            }
        }

        public void Visit(IUsageCharge<IUsageByDataAmount> charge)
        {
        	var usage = charge.Usage;
            if (this.FamilyMailAddresses.Any(n => n == usage.Destination))
            {
                switch (usage.Type)
                {
                    case "imode":
                        charge.UnitAmount = 0;
                        charge.Description = string.Format("{0} ({1}適用により無料)", charge.Description);
                        break;
                    default:
                        break;
                }
            }
        }

        public void Visit(IUsageCharge<IUsageByNumber> charge)
        {
            // 何もしない
        }
    }
}
