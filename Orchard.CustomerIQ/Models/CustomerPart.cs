using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Security;
using Orchard.Users.Models;

namespace Orchard.CustomerIQ.Models
{
    public class CustomerPart : ContentPart<CustomerPartRecord>
    {

        public string FirstName
        {
            get { return Record.FirstName; }
            set { Record.FirstName = value; }
        }

        public string LastName
        {
            get { return Record.LastName; }
            set { Record.LastName = value; }
        }

        public string EmailAddress
        {
            get { return Record.EmailAddress; }
            set { Record.EmailAddress = value; }
        }

        public IUser User
        {
            get { return this.As<UserPart>(); }
        }

        public CustomerPart Customer
        {
            get { return this.As<CustomerPart>(); }
        }
    }
}