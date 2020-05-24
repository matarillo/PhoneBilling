/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package phone;

/**
 *
 * @author kinomata1
 */
public interface ChargePerUsage extends Charge {

    public Usage getUsage();

    public int getUnitAmount();

    public void setUnitAmount(int value);

    public int getUnitPrice();

    public void setUnitPrice(int value);

    public void Callback(ChargePerUsageHandler handler);

    public void Visit(UsageByTimePeriod usage);

    public void Visit(UsageByDataAmount usage);

    public void Visit(UsageByNumber usage);
}
