using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phone.Charges;
using Phone.Usages;

namespace Phone.Services
{
    public class NewPlan : Service, IPlan
    {
        public enum NewPlanType
        {
            BasicPlanS,
            BasicPlanM,
            BasicPlanL,
            ValuePlanS,
            ValuePlanM,
            ValuePlanL
        }

        private readonly string name;
        private readonly ChargeFilter filter;
        private readonly Charger charger;

        public NewPlan(NewPlanType type)
        {
            switch (type)
            {
                case NewPlanType.BasicPlanS:
                    this.name = "ベーシックプランS";
                    this.filter = new ChargeFilter
                    {
                        BasicFee = 4600,
                        VoiceFee = 18,
                        DigitalFee = 32,
                        PacketFee = 2
                    };
                    this.charger = new Charger
                    {
                        Allowance = 2000
                    };
                    break;

                case NewPlanType.BasicPlanM:
                    this.name = "ベーシックプランM";
                    this.filter = new ChargeFilter
                    {
                        BasicFee = 6600,
                        VoiceFee = 14,
                        DigitalFee = 25,
                        PacketFee = 2
                    };
                    this.charger = new Charger
                    {
                        Allowance = 4000
                    };
                    break;

                case NewPlanType.BasicPlanL:
                    this.name = "ベーシックプランL";
                    this.filter = new ChargeFilter
                    {
                        BasicFee = 9600,
                        VoiceFee = 10,
                        DigitalFee = 18,
                        PacketFee = 2
                    };
                    this.charger = new Charger
                    {
                        Allowance = 6000
                    };
                    break;

                case NewPlanType.ValuePlanS:
                    this.name = "バリュープランS";
                    this.filter = new ChargeFilter
                    {
                        BasicFee = 3000,
                        VoiceFee = 18,
                        DigitalFee = 32,
                        PacketFee = 2
                    };
                    this.charger = new Charger
                    {
                        Allowance = 2000
                    };
                    break;

                case NewPlanType.ValuePlanM:
                    this.name = "バリュープランM";
                    this.filter = new ChargeFilter
                    {
                        BasicFee = 5000,
                        VoiceFee = 14,
                        DigitalFee = 25,
                        PacketFee = 2
                    };
                    this.charger = new Charger
                    {
                        Allowance = 4000
                    };
                    break;

                case NewPlanType.ValuePlanL:
                    this.name = "バリュープランL";
                    this.filter = new ChargeFilter
                    {
                        BasicFee = 8000,
                        VoiceFee = 10,
                        DigitalFee = 18,
                        PacketFee = 2
                    };
                    this.charger = new Charger
                    {
                        Allowance = 6000
                    };
                    break;

                default:
                    throw new ArgumentException();
            }
        }

        public override string Name
        {
            get { return name; }
        }

        public IBasicCharge CreateBasicCharge(int year, int month)
        {
            return new BasicCharge { YearMonth = new DateTime(year, month, 1) };
        }

        public override void Filter(ICharge charge)
        {
            charge.Accept(filter);
        }

        public override bool Consume(ICharge charge)
        {
            charge.Accept(charger);
            return charger.Consumed;
        }

        public override IEnumerable<ICharge> GetCharges()
        {
            return charger.GetCharges();
        }

        private class ChargeFilter : IChargeVisitor
        {
            public const int CALLUNIT = 30;
            public const int PACKETUNIT = 10;

            public int BasicFee;
            public int VoiceFee;
            public int DigitalFee;
            public int PacketFee;

            public void Visit(IBasicCharge charge)
            {
                charge.Description = "基本使用料";
                charge.UnitPrice = BasicFee;
            }

            public void Visit(IUsageCharge<IUsageByTimePeriod> charge)
            {
                switch (charge.Usage.Type)
                {
                    case "voice":
                        charge.Description = "通話料";
                        charge.UnitPrice = VoiceFee;
                        charge.UnitAmount = 30;
                        break;
                    case "digital":
                        charge.Description = "64kデジタル通信料";
                        charge.UnitPrice = DigitalFee;
                        charge.UnitAmount = 30;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            public void Visit(IUsageCharge<IUsageByDataAmount> charge)
            {
                switch (charge.Usage.Type)
                {
                    case "imode":
                        charge.Description = "iモード通信料";
                        break;
                    case "data":
                        charge.Description = "データ通信料";
                        break;
                    default:
                        charge.Description = "パケット通信料";
                        break;
                }
                charge.UnitPrice = PacketFee;
                charge.UnitAmount = 10;
            }

            public void Visit(IUsageCharge<IUsageByNumber> charge)
            {
                charge.Description = charge.Usage.Name;
                charge.UnitPrice = charge.Usage.UnitPrice;
                charge.UnitAmount = 1;
            }
        }

        private class Charger : IChargeVisitor
        {
            public int Allowance;
            public bool Consumed;

            private IBasicCharge basicCharge;
            private IList<IUsageCharge<IUsageByTimePeriod>> callList;

            public Charger()
            {
                basicCharge = null;
                callList = new List<IUsageCharge<IUsageByTimePeriod>>();
                Consumed = false;
            }

            public void Visit(IBasicCharge charge)
            {
                // 出力順を入れ替えるために保持する
                basicCharge = charge;
                Consumed = true;
            }

            public void Visit(IUsageCharge<IUsageByTimePeriod> charge)
            {
                // 無料割引の計算のために保持する
                callList.Add(charge);
                Consumed = true;
            }

            public void Visit(IUsageCharge<IUsageByDataAmount> charge)
            {
                Consumed = false;
            }

            public void Visit(IUsageCharge<IUsageByNumber> charge)
            {
                Consumed = false;
            }

            public IEnumerable<ICharge> GetCharges()
            {
                if (basicCharge != null)
                {
                    yield return basicCharge;
                }
                foreach (IUsageCharge<IUsageByTimePeriod> call in callList)
                {
                    yield return call;
                }
                Charge reducedCharge = new Charge();
                reducedCharge.Description = "無料通話分を割引します。";
                int totalCallFee = callList.Sum(c => c.Fee);
                reducedCharge.Fee = (totalCallFee < Allowance) ? -totalCallFee : -Allowance;
                yield return reducedCharge;
            }
        }
    }
}
