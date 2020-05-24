using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phone;
using Phone.Charges;
using Phone.Usages;

namespace Phone.Services
{
    /// <summary>
    /// パケットパック
    /// </summary>
    public class PacketPack : Service
    {
        public enum PacketPackType
        {
            PacketPack60,
            PacketPack90
        }

        private const int PACKETUNIT = 1000;

        private PacketPackType type;
        private readonly string name;
        private readonly int baseFee;
        private readonly int allowance;
        private readonly int packetFee;

        private List<IChargePerUsage> packetList = new List<IChargePerUsage>();

        public PacketPackType Type
        {
            get
            {
                return type;
            }
        }

        public override string Name
        {
            get { return name; }
        }

        public PacketPack(PacketPackType type)
        {
            this.type = type;
            switch (type)
            {
                case PacketPackType.PacketPack60:
                    this.name = "パケットパック 60";
                    this.baseFee = 6000;
                    this.allowance = this.baseFee;
                    this.packetFee = 20;
                    break;
                case PacketPackType.PacketPack90:
                    this.name = "パケットパック 90";
                    this.baseFee = 9000;
                    this.allowance = this.baseFee;
                    this.packetFee = 15;
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public bool Accept(UsageCharge charge)
        {
            if (!(charge.Usage is ByDataAmount))
            {
                return false;
            }
            charge.UnitPrice = packetFee;
            charge.UnitAmount = PACKETUNIT;
            packetList.Add(charge);
            return true;
        }

        public IEnumerable<ICharge> GetCharges()
        {
            Charge baseCharge = new Charge();
            baseCharge.Fee = baseFee;
            baseCharge.Description = string.Format("{0} 定額料", name);
            yield return baseCharge;

            foreach (IChargePerUsage packet in packetList)
            {
                yield return packet;
            }

            Charge reducedCharge = new Charge();
            int totalPacketFee = packetList.Sum(c => c.Fee);
            reducedCharge.Fee = (totalPacketFee < allowance) ? -totalPacketFee : -allowance;
            reducedCharge.Description = string.Format("{0} 無料通信分を割引します。", name);
            yield return reducedCharge;
        }
    }
}
