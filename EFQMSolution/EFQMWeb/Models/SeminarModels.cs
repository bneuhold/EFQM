using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFQMWeb.Models
{
    public class SeminarRegistration
    {
        public string userOIB { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string academicDegree { get; set; }
        public string academicTitle { get; set; }
        public string dateOfBirth { get; set; }
        public string placeOfBirth { get; set; }
        public string address { get; set; }
        public string ZIP { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string mobile { get; set; }
        public string companyName { get; set; }
        public string companyPhone { get; set; }
        public string companyMob { get; set; }
        public string companyFax { get; set; }
        public string companyAddress { get; set; }
        public string companyZIP { get; set; }
        public string companyCity { get; set; }
        public string companyOIB { get; set; }
        public string companyWorkPosition { get; set; }
        public string seminarList { get; set; }
    }
}