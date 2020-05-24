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
public interface Repository {

    public List<Usage> GetUsages(Line line, int year, int month);

    public Plan GetPlan(Line line, int year, int month);

    public List<Service> GetServices(Line line, int year, int month);
}
