using BotDetect;
using BotDetect.Web.Mvc;

namespace NtierMvc.Infrastructure
{
    public static class CaptchaHelper
    {
        public static MvcCaptcha GetRegistrationCaptcha()
        {
            // create the control instance
            MvcCaptcha registrationCaptcha = new MvcCaptcha("RegistrationCaptcha");

            // set up client-side processing of the Captcha code input textbox
            registrationCaptcha.UserInputID = "CaptchaCode";

            // Captcha settings
            registrationCaptcha.ImageSize = new System.Drawing.Size(255, 50);
            registrationCaptcha.ReloadEnabled = true;
            registrationCaptcha.AutoUppercaseInput = true;
            registrationCaptcha.HelpLinkEnabled = false;
            registrationCaptcha.ImageStyle = ImageStyle.Wave;
            registrationCaptcha.CodeLength = 5;

            return registrationCaptcha;
        }

        public static MvcCaptcha GetLoginCaptcha()
        {
            // create the control instance
            MvcCaptcha loginnCaptcha = new MvcCaptcha("LoginCaptcha");

            loginnCaptcha.UserInputID = "CaptchaCode";

            // Captcha settings
            loginnCaptcha.ImageSize = new System.Drawing.Size(255, 50);
            loginnCaptcha.ReloadEnabled = true;
            loginnCaptcha.AutoUppercaseInput = true;
            loginnCaptcha.HelpLinkEnabled = false;
            loginnCaptcha.ImageStyle = ImageStyle.Wave;
            loginnCaptcha.CodeLength = 5;
            return loginnCaptcha;
        }

        public static MvcCaptcha GetInstituteLoginCaptcha()
        {
            MvcCaptcha instituteLoginCaptcha = new MvcCaptcha("InstituteLoginCaptcha");

            instituteLoginCaptcha.UserInputID = "CaptchaCode";

            instituteLoginCaptcha.ImageSize = new System.Drawing.Size(255, 50);
            instituteLoginCaptcha.ReloadEnabled = true;
            instituteLoginCaptcha.AutoUppercaseInput = true;
            instituteLoginCaptcha.HelpLinkEnabled = false;
            instituteLoginCaptcha.ImageStyle = ImageStyle.Wave;
            instituteLoginCaptcha.CodeLength = 5;
            return instituteLoginCaptcha;
        }

        public static MvcCaptcha GetAdminLoginCaptcha()
        {
            MvcCaptcha adminLoginCaptcha = new MvcCaptcha("AdminLoginCaptcha");

            adminLoginCaptcha.UserInputID = "CaptchaCode";

            adminLoginCaptcha.ImageSize = new System.Drawing.Size(255, 50);
            adminLoginCaptcha.ReloadEnabled = true;
            adminLoginCaptcha.AutoUppercaseInput = true;
            adminLoginCaptcha.HelpLinkEnabled = false;
            adminLoginCaptcha.ImageStyle = ImageStyle.Wave;
            adminLoginCaptcha.CodeLength = 5;
            return adminLoginCaptcha;
        }
        public static MvcCaptcha GetOTPCaptcha()
        {
            // create the control instance
            MvcCaptcha loginnCaptcha = new MvcCaptcha("OTPCaptcha");

            loginnCaptcha.UserInputID = "CaptchaCode";

            // Captcha settings
            loginnCaptcha.ImageSize = new System.Drawing.Size(255, 50);
            loginnCaptcha.ReloadEnabled = true;
            loginnCaptcha.AutoUppercaseInput = true;
            loginnCaptcha.HelpLinkEnabled = false;
            loginnCaptcha.ImageStyle = ImageStyle.Wave;
            loginnCaptcha.CodeLength = 5;
            return loginnCaptcha;
        }
    }
}