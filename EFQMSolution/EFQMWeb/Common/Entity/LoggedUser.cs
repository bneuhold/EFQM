﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFQMWeb.Common.Entity
{
    public class LoggedUser
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// IZ - izvodac
        /// KO - konzultant
        /// IN - investor
        /// </summary>
        public string Tip { get; set; }
    }
}