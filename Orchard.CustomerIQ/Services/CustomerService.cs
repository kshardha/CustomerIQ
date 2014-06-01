using Orchard;
using Orchard.ContentManagement;
using Orchard.Security;
using Orchard.Services;
using Orchard.Users.Models;
using Orchard.CustomerIQ.Models;
using System.Collections.Generic;
using Orchard.Data;
using NHibernate;
using NHibernate.Linq;
using Orchard.CustomerIQ.ViewModels;

namespace Orchard.CustomerIQ.Services
{
     public class CustomerService : ICustomerService
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IMembershipService _membershipService;
        private readonly IClock _clock;
        private readonly ISessionLocator _sessionLocator;
 
        public CustomerService(IOrchardServices orchardServices, IMembershipService membershipService, IClock clock,
            ISessionLocator sessionLocator)
        {
            _orchardServices = orchardServices;
            _membershipService = membershipService;
            _clock = clock;
            _sessionLocator = sessionLocator;
        }
 
        public CustomerPart CreateCustomer(string email, string password)
        {
            // New up a new content item of type "Customer"
            var customer = _orchardServices.ContentManager.New("Customer");
 
            // Cast the customer to a UserPart
            var userPart = customer.As<UserPart>();
 
            // Cast the customer to a CustomerPart
            var customerPart = customer.As<CustomerPart>();
 
            // Set some properties of the customer content item (via UserPart and CustomerPart)
            userPart.UserName                  = email;
            userPart.Email                     = email;
            userPart.NormalizedUserName        = email.ToLowerInvariant();
            userPart.Record.HashAlgorithm      = "SHA1";
            userPart.Record.RegistrationStatus = UserStatus.Approved;
            userPart.Record.EmailStatus        = UserStatus.Approved;
 
            //customerPart.CreatedUtc = _clock.UtcNow;
 
            // Use Ochard's MembershipService to set the password of our new user
            _membershipService.SetPassword(userPart, password);
 
            // Store the new user into the database
            _orchardServices.ContentManager.Create(customer);
 
            return customerPart;
        }

        public IContentQuery<CustomerPart> GetCustomers()
        {
            return _orchardServices.ContentManager.Query<CustomerPart>();
        }
    }
}
