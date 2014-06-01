using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.CustomerIQ.Models;
using Orchard.CustomerIQ.ViewModels;
using Orchard.ContentManagement;

namespace Orchard.CustomerIQ.Services
{
    public interface ICustomerService : IDependency
    {
        CustomerPart CreateCustomer(string email, string password);

        IContentQuery<CustomerPart> GetCustomers();
    }
}