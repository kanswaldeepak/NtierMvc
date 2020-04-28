using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NtierMvc.Model.Account;
using NtierMvc.Model;

namespace NtierMvc.Infrastructure
{
    public class ERPContext
    {
        public static UserEntity UserContext
        {
            get
            {
                return (UserEntity)HttpContext.Current.Session["UserContext"];

            }
            set
            {
                HttpContext.Current.Session["UserContext"] = value;

            }
        }
        public static UserEntity AdminContext
        {
            get
            {
                return (UserEntity)HttpContext.Current.Session["AdminContext"];

            }
            set
            {
                HttpContext.Current.Session["AdminContext"] = value;

            }
        }
        
        public static ApplicationFromEntity ApplicationFormContext
        {
            get
            {
                return (ApplicationFromEntity)HttpContext.Current.Session["ApplicationFormContext"];

            }
            set
            {
                HttpContext.Current.Session["ApplicationFormContext"] = value;

            }
        }
    }

    
    public class ApplicationFromEntity
    {
        public int RegistrationId { get; set; }
        public int InstituteId { get; set; }
        public int WorkitemId { get; set; }
        public int WorkflowInstanceId { get; set; }
        
    }
}