/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package phone;

import java.util.Date;

/**
 *
 * @author kinomata1
 */
public interface Usage {

    public Date getDateTime();

    public int getAmount();

    public String getType();

    public MessageAreaType getMessageArea();

    // public void Accept(ChargePerUsage visitor);
}
