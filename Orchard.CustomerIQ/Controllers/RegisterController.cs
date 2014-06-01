using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Themes;
using System.Web.Mvc;
using Orchard.Security;
using Orchard.CustomerIQ.ViewModels;
using Orchard.CustomerIQ.Services;
using Orchard.CustomerIQ.Models;
using Orchard.Data;
using Orchard.Users.Models;
using Orchard.DisplayManagement.Shapes;
using Orchard.UI.Navigation;
using Orchard.DisplayManagement;
using Orchard.Settings;

namespace Orchard.CustomerIQ.Controllers
{
    public class RegisterController : Controller
    {
        private Localizer T { get; set; }
        private readonly IAuthenticationService _authenticationService;
        private readonly IOrchardServices _services;
        private readonly ICustomerService _customerService;
        private readonly IMembershipService _membershipService;
        private dynamic Shape { get; set; }
        private readonly ISiteService _siteService;

        //private readonly IRepository<CustomerPartRecord> _customerPartRepository;

        /**
        public ActionResult Index()
        {
            return View("CustomerIQ");
        }
        

        public RegisterController(IOrchardServices services)
        {
            _services = services;
        }
        
        
        [Themed]
        public ActionResult SignupOrLogin() {
 
            return new ShapeResult(this, _services.New.Register_SignupOrLogin());
        }
        **/

        public RegisterController(IOrchardServices services, IAuthenticationService authenticationService,
            ICustomerService customerService, IMembershipService membershipService, IShapeFactory shapeFactory,
            ISiteService siteService)
        {
            _authenticationService = authenticationService;
            _services = services;
            _customerService = customerService;
            _membershipService = membershipService;
            //_customerPartRepository = customerPartRepository;
            Shape = shapeFactory;
            _siteService = siteService;
        }

        [Themed]
        public ActionResult SignupOrLogin()
        {
            //if (_authenticationService.GetAuthenticatedUser() != null)
              //  return RedirectToAction("SelectAddress");

            return new ShapeResult(this, _services.New.Register_SignupOrLogin());
        }

        [Themed]
        public ActionResult Signup()
        {
            var shape = _services.New.Register_Signup();
            return new ShapeResult(this, shape);
        }

        [HttpPost]
        public ActionResult Signup(SignupViewModel signup)
        {
            if (!ModelState.IsValid)
                return new ShapeResult(this, _services.New.Register_Signup(Signup: signup));

            var customer = _customerService.CreateCustomer(signup.Email, signup.Password);
            customer.FirstName = signup.FirstName;
            customer.LastName = signup.LastName;
            customer.EmailAddress = signup.Email;

            _authenticationService.SignIn(customer.User, true);

            return RedirectToAction("CustomerList");
        }

        [Themed]
        public ActionResult Login()
        {
            var shape = _services.New.Register_Login();
            return new ShapeResult(this, shape);
        }

        [Themed, HttpPost]
        public ActionResult Login(LoginViewModel login)
        {

            // Validate the specified credentials
            var user = _membershipService.ValidateUser(login.Email, login.Password);

            // If no user was found, add a model error
            if (user == null)
            {
                ModelState.AddModelError("Email", "Incorrect username/password combination");
            }

            // If there are any model errors, redisplay the login form
            if (!ModelState.IsValid)
            {
                var shape = _services.New.Register_Login(Login: login);
                return new ShapeResult(this, shape);
            }

            // Create a forms ticket for the user
            //_authenticationService.SignIn(user, login.CreatePersistentCookie);

            // Redirect to the next step
            return RedirectToAction("CustomerList");
        }

        
        [Themed]
        public ActionResult CustomerList()
        {
           // var customerQuery = _customerService.GetCustomers().Join<UserPartRecord>().List();
           var customerQuery = _customerService.GetCustomers().List();
            
            // Project the query into a list of customer shapes
            var customersProjection = from customer in customerQuery
                                      select Shape.Customer
                                      (
                                        Id: customer.Id,
                                        FirstName: customer.FirstName,
                                        LastName: customer.LastName,
                                        Email: customer.EmailAddress
                                      );

            var model = new CustomersIndexVM(customersProjection);

            return View("CustomerList",model);
        } 
       
        
    }
}