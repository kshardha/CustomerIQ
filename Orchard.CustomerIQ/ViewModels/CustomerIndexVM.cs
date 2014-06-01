using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.CustomerIQ.ViewModels
{
    public class CustomersIndexVM
    {
        
        public IList<dynamic> Customers { get; set; }      

        

        public CustomersIndexVM(IEnumerable<dynamic> customers)
        {
            Customers = customers.ToArray();            
        }        


        
    }
}