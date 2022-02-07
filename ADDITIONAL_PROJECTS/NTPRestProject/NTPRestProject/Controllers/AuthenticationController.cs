using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NTPRestProject.Controllers
{
    public class AuthenticationController : ApiController
    {
        // GET api/<controller>
        public string Get(string email, string pass)
        {
            LoginHelper login = new LoginHelper();
            return login.LoginSelectData(email, pass);
        }
    }
}