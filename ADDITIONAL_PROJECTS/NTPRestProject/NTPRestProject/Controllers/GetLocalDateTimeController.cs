using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NTPRestProject.Controllers
{
    public class GetLocalDateTimeController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            DateTime localDateTime = DateTime.Now;
            return localDateTime.ToString();
        }

    }
}