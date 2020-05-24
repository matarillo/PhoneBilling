using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public class Line
    {
        private IRepository repository;

        public Line(IRepository repository)
        {
            this.repository = repository;
        }

        public IList<ICharge> Calculate(int year, int month)
        {
            this.Plan = repository.GetPlan(this, year, month);
            this.Services = repository.GetServices(this, year, month);
            this.Usages = repository.GetUsages(this, year, month);

            List<ICharge> charges = new List<ICharge>();

            // 基本料
            charges.Add(this.Plan.CreateBasicCharge(year, month));
            // 使用料
            charges.AddRange(this.Usages.Select(u => u.CreateCharge()));
            // 調整
            charges.ForEach(c => this.PlanAndServices.ForEach(f => f.Filter(c)));

            // 定額料＋使用料一括割引
            var leftCharges = charges.Where(c => this.PlanAndServices.All(f => !f.Consume(c)));
            var adjustedCharges = this.PlanAndServices.SelectMany(f => f.GetCharges());

            List<ICharge> totalCharges = new List<ICharge>();
            totalCharges.AddRange(adjustedCharges);
            totalCharges.AddRange(leftCharges);
            return totalCharges;
        }

        public IPlan Plan { get; set; }

        public IList<IService> Services { get; set; }

        public IList<IUsage> Usages { get; set; }

        public IEnumerable<IService> PlanAndServices
        {
            get
            {
                yield return this.Plan;
                foreach (IService service in this.Services)
                {
                    yield return service;
                }
            }
        }
    }
}
