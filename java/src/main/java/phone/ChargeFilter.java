/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package phone;

/**
 *
 * @author kinomata1
 */
public interface ChargeFilter<T extends Charge> {

    public void Apply(T charge);
}
