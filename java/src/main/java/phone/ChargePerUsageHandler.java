/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package phone;

/**
 *
 * @author kinomata1
 */
public interface ChargePerUsageHandler {

    public void Handle(ChargePerUsage charge, UsageByTimePeriod usage);

    public void Handle(ChargePerUsage charge, UsageByDataAmount usage);

    public void Handle(ChargePerUsage charge, UsageByNumber usage);
}
