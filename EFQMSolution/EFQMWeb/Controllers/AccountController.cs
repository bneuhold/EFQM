using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using EFQMWeb.Models;
using EFQMWeb.Common.Base;
using EFQMWeb.Common.Util;
using MRI.Tools;
using EFQMWeb.Common;

namespace EFQMWeb.Controllers
{
    public class AccountController : BaseController
    {

        //
        // GET: /Account/LogOn

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            PureJson result = new PureJson();
            if (ModelState.IsValid)
            {
                MySession.CurrentUser = Database.Login(model.Email, model.Password);
                if (MySession.CurrentUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
                    {
                        jRoot.Add("Status", 0);
                    }
                    return new SimpleJsonResult(result);
                }
            }
            using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
            {
                jRoot.Add("Status", 1);
            }
            return new SimpleJsonResult(result);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Account");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(LoggedUser model)
        {
            PureJson result = new PureJson();
            if (ModelState.IsValid)
            {
                LoggedUser user = Database.Login(model.Email, model.Password);
                if (user != null)
                {
                    using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
                    {
                        jRoot.Add("Status", 2);
                    }
                    return new SimpleJsonResult(result);
                }
                user = Database.Register(model);
                if (user != null)
                {
                    SendRegistrationMail(model);
                    //FormsAuthentication.SetAuthCookie(model.Email, false);
                    //MySession.CurrentUser = user;
                    using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
                    {
                        jRoot.Add("Status", 0);
                    }
                    return new SimpleJsonResult(result);
                }
            }
            using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
            {
                jRoot.Add("Status", 1);
            }
            return new SimpleJsonResult(result);
        }

        public ActionResult Activate(string key)
        {
            LoggedUser user = ActivateAccount(key, "USER");

            return View(user);
        }

        public ActionResult SuperActivate(string key)
        {

            LoggedUser user = ActivateAccount(key, "SUPERUSER");

            return View(user);
        }

        private LoggedUser ActivateAccount(string key, string UserType)
        {
            int? id=null;
            string type = null;
            string val = Encryption64Util.DecryptStringUInt64(key);
            string[] keys = val.Split(new char[] { '&' });
            string[] keyValue = keys[0].Split(new char[] { ':' });
            if (keyValue[0] == "ID")
            {
                id = Utils.ParseInt(keyValue[1]);
            }

            keyValue = keys[1].Split(new char[] { ':' });
            if (keyValue[1] == "Type")
            {
                type = keyValue[1];
            }
            if (id.HasValue && type == UserType)
            {
                return Database.ActivateUser(id.Value, type);
            }
            return null;
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult TestEmail(string to, string content)
        {
            EmailUtil email = new EmailUtil(false);
            ViewBag.Status = email.SendHtmlEmail(content, "Test", "info@huog.hr", to, null, null);
            return View();
        }

        #region email
        private int SendRegistrationMail(LoggedUser model)
        {
            string key = "ID=" + model.Id + "&Type=USER";
            EmailUtil email = new EmailUtil(false);

            string content = "Kako biste potvrdili registraciju na portalu huog.hr kliknite na sljedeći link:" + "<br /><br />"
                                + Url.AbsoluteAction("Activate", "Account", new { key = Encryption64Util.CryptStringUInt64(key) })
                                + "<br /><br />"
                                + "Ako ne možete kliknuti na link, akcijom copy-paste kopirajte ga u vaš preglednik." + "<br /><br />";
            if (email.SendHtmlEmail(content, "Potvrda registracije", "info@huog.hr", model.Email, null, null) == 0)
            {
                key = "ID=" + model.Id + "&Type=SUPERUSER";
                content = "Prijavljeni korisnik:<br/>"
                            + "Email: " + model.Email + " <br/>"
                            + "Ime: " + model.Name + " <br/>"
                            + "Tvrtka: " + model.CompanyName + " <br/>"
                            + "Grad: " + model.City + " <br/>"
                            + "Tip: " + model.Type + " <br/>"
                            + "Broj Zaposlenih: " + model.Employees + " <br/>"
                            + "Prihod: " + model.Income + " <br/>"
                            +"<br /><br />Kako biste ga potvrdili kliknite na sljedeci link: <br/>"
                            + Url.AbsoluteAction("SuperActivate", "Account", new { key = Encryption64Util.CryptStringUInt64(key) })
                            + "<br /><br />"
                            + "Ako ne možete kliknuti na link, akcijom copy-paste kopirajte ga u vaš preglednik." + "<br /><br />";

                email.SendHtmlEmail(content, "Potvrda korisničke registracije", "info@huog.hr", MyConfig.GetSetting("EmailConfirmUserReg"), null, null);
            }
            else
            {
                return -1;
            }

            return 0;
        }

        #endregion

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
