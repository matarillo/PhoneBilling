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
public interface BasicMonthlyCharge extends Charge {

    public Date getYearMonth();

    public void setYearMonth(Date value);

    public int getDiscountRate();

    public void setDiscountRate(int value);

    public int getUnitPrice();

    public void setUnitPrice(int value);
}
