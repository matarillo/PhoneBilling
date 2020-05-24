using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phone;
using Phone.Charges;

namespace Phone.Services
{
    /// <summary>
    /// パケ・ホーダイ ダブル
    /// </summary>
    public class PacketFlatRate : Service
    {
        private Charger charger;

        public override string Name
        {
            get { return "パケ・ホーダイ ダブル"; }
        }

        public PacketFlatRate()
        {
            charger = new Charger
            {
                Name = this.Name,
                FlatRate = 980,
                Limit = 4200
            };
        }

        public override void Filter(ICharge charge)
        {
        }

        public override bool Consume(ICharge charge)
        {
            charger.Consumed = false;
            charge.Accept(charger);
            return charger.Consumed;
        }

        public override IEnumerable<ICharge> GetCharges()
        {
            return charger.GetCharges();
        }

        private class Charger : SingleVisitor<IUsageCharge<IUsageByDataAmount>>
        {
            public int FlatRate;
            public int Limit;
            public string Name;
            public bool Consumed;
            private List<IUsageCharge<IUsageByDataAmount>> imodeList;

            public Charger()
            {
                imodeList = new List<IUsageCharge<IUsageByDataAmount>>();
                Consumed = false;
            }

            public override void Dispatch(IUsageCharge<IUsageByDataAmount> charge)
            {
                IUsage usage = charge.Usage;
                if (usage.Type != "imode")
                {
                    Consumed = false;
                    return;
                }
                charge.UnitPrice = 8;
                charge.UnitAmount = 10;
                imodeList.Add(charge);
                Consumed = true;
            }

            public IEnumerable<ICharge> GetCharges()
            {
                int totalImodeFee = imodeList.Sum(c => c.Fee);
                if (Limit < totalImodeFee)
                {
                    Charge limitCharge = new Charge();
                    limitCharge.Fee = Limit;
                    limitCharge.Description = string.Format("{0} 上限額", Name);
                    yield return limitCharge;
                    yield break;
                }

                Charge baseCharge = new Charge();
                baseCharge.Fee = FlatRate;
                baseCharge.Description = string.Format("{0} 定額料", Name);
                yield return baseCharge;

                foreach (IUsageCharge<IUsageByDataAmount> imode in imodeList)
                {
                    yield return imode;
                }

                Charge reducedCharge = new Charge();
                reducedCharge.Fee = (totalImodeFee < FlatRate) ? -totalImodeFee : -FlatRate;
                reducedCharge.Description = string.Format("{0} 無料通信分を割引します。", Name);
                yield return reducedCharge;
            }
        }
    }
}
