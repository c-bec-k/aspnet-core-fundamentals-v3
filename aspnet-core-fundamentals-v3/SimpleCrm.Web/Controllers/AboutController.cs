﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCrm.Web.Controllers
{
    [Route("about")]
    public class AboutController
    {
        [Route("phone")]
        public string phone()
        {
            return "555-867-5309";
        }

        [Route("address")]
        public string address()
        { return "USA"; }

        [Route("")]
        public string Index()
        {
            return "Hello, there!";
        }
    }
}
