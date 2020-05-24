using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    public interface IRepository
    {
        IList<IUsage> GetUsages(Line line, int year, int month);
        IPlan GetPlan(Line line, int year, int month);
        IList<IService> GetServices(Line line, int year, int month);
    }
}
