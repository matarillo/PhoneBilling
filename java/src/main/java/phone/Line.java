/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package phone;

import java.util.Vector;
import java.util.List;

/**
 *
 * @author kinomata1
 */
public class Line {

    private Repository repository;
    private Plan plan;
    private List<Service> services;
    private List<Usage> usages;

    public Line(Repository repository) {
        this.repository = repository;
    }

    /**
     * @return the Plan
     */
    public Plan getPlan() {
        return plan;
    }

    /**
     * @return the Services
     */
    public List<Service> getServices() {
        return services;
    }

    /**
     * @return the Usages
     */
    public List<Usage> getUsages() {
        return usages;
    }

    /**
     * @return the BasicDiscounts
     */
    public List<ChargeFilter<BasicMonthlyCharge>> getBasicDiscounts() {
        return null;
    }

    /**
     * @return the UsageDiscounts
     */
    public List<ChargeFilter<ChargePerUsage>> getUsageDiscounts() {
        return null;
    }

    /**
     * @return the Chargers
     */
    public List<Charger> getChargers() {
        return null;
    }

    public List<Charge> Calculate(int year, int month) {
        plan = repository.GetPlan(this, year, month);
        services = repository.GetServices(this, year, month);
        usages = repository.GetUsages(this, year, month);

        BasicMonthlyCharge basicCharge = plan.getBasicMonthlyCharge(year, month);
        List<ChargeFilter<BasicMonthlyCharge>> basicDiscounts = this.getBasicDiscounts();
        for (ChargeFilter<BasicMonthlyCharge> f : basicDiscounts) {
            f.Apply(basicCharge);
        }

        List<ChargePerUsage> chargesPerUsage = new Vector<ChargePerUsage>();
        for (Usage u : this.getUsages()) {
            chargesPerUsage.add(plan.getDefaultCharge(u));
        }

        List<ChargeFilter<ChargePerUsage>> usageDiscounts = this.getUsageDiscounts();
        for (ChargePerUsage c : chargesPerUsage) {
            for (ChargeFilter<ChargePerUsage> f : usageDiscounts) {
                f.Apply(c);
            }
        }

        List<Charger> chargers = this.getChargers();
        List<ChargePerUsage> leftCharges = new Vector<ChargePerUsage>();
        for (ChargePerUsage c : chargesPerUsage) {
            for (Charger f : chargers) {
                if (!(f.Accept(c))) {
                    leftCharges.add(c);
                    break;
                }
            }
        }

        List<Charge> charges = new Vector<Charge>();
        charges.add(basicCharge);
        for (Charger f : chargers) {
            charges.addAll(f.GetCharges());
        }
        charges.addAll(leftCharges);
        return charges;
    }
}
