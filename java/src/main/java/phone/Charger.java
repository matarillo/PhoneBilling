/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package phone;

import java.util.List;

/**
 *
 * @author kinomata1
 */
public interface Charger {

    public boolean Accept(ChargePerUsage charge);

    List<Charge> GetCharges();
}
