/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package phone;

/**
 *
 * @author kinomata1
 */
public interface Plan {

    public BasicMonthlyCharge getBasicMonthlyCharge(int year, int month);

    public ChargePerUsage getDefaultCharge(Usage usage);
}
