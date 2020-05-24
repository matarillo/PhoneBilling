/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package phone;

/**
 *
 * @author kinomata1
 */
public interface Charge {

    public String getDescription();

    public void setDescription(String value);

    public int getFee();

    public void setFee(int value);
}
