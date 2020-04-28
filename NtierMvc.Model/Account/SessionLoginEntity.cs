using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.Account
{
    public class SessionLoginEntity
    {
        public int UserId { get; set; }
        public string IPV4 { get; set; }
        public string IPV6 { get; set; }
        public string UserAgent { get; set; }

        public string SessionId { get; set; }

        public LogoutOption.LogoutCode logout { get; set; }

    }
    public class ChangePasswodEntity
    {
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string OTP { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Require Old Password")]
        [MinLength(8, ErrorMessage = "Minimum Password Length is 8")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Require New Password")]
        [MinLength(8, ErrorMessage = "Minimum Password Length is 8")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\\da-zA-Z])(.{8,15})$", ErrorMessage = "Password should have minimum 1 Capital Alphabet, 1 Number,1 Special Character and without space, e.g. Password@123")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Require Confirm Password")]
        [Compare("Password", ErrorMessage = "New Password should no match with Confirm Password")]
        [MinLength(8, ErrorMessage = "Minimum Password Length is 8")]
        public string ConfirmPassword { get; set; }

        public string RegNumber { get; set; }
        public string passPhrase { get; set; }
    }
}
