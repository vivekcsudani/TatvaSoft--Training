using Ciplatform.Entities.Data;
using Ciplatform.Entities.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;
using System.Web.Helpers;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Persistence.Repositories;
using User = Ciplatform.Entities.Models.User;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


using MailKit.Net.Smtp;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace CI_Platform.Controllers
{
    public class UserController : Controller
    {
        private readonly CiContext _Context;

        public UserController(CiContext context)
        {
            _Context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User u)
        {

            var Data = _Context.Users.FirstOrDefault(m => m.Email == u.Email && m.Password == u.Password);

            if (Data == null)
            {
                TempData["Error"] = "Login Failed";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public IActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Registration([Bind("FirstName,SecondName,Email,Password,PhoneNumber,CityId,CountryId")] User user)
        {
            var data = _Context.Users.Where(model => model.Email == user.Email).FirstOrDefault(); 
            if (data != null)
            {

                TempData["Enter"] = "Fill the Details or Email is alredy Exist.";
                return View();
            }
            else
            {
                user.CityId = 1;
                user.CountryId = 1;
                _Context.Users.Add(user);
                _Context.SaveChanges();
                await _Context.SaveChangesAsync();
                return RedirectToAction("Login", "User");
            }
        }


        public IActionResult ForgotPassword() { 
                
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(User model)
        {
            var user = _Context.Users.Where(x => x.Email == model.Email && x.DeletedAt == null).FirstOrDefault();
            if (user == null)
            {
                return View();
            }
            else
            {
                /*/ Generate Token /*/
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringchars = new char[16];
                var random = new Random();
                for (int i = 0; i < stringchars.Length; i++)
                {
                    stringchars[i] = chars[random.Next(chars.Length)];

                }
                var finalString = new String(stringchars);

                /*/ Update Password Reset Table /*/
                PasswordReset entry = new PasswordReset();
                entry.CreatedAt = DateTime.Now;
                entry.Email = model.Email;
                entry.Token = finalString;
                _Context.PasswordResets.Add(entry);


                _Context.SaveChanges();
                HttpContext.Session.SetString("token_session", finalString);


                /* Sending Mail */
                var mailbody = "<h1>Click link to reset password</h1><br><h2><a href='" + "https://localhost:7034/User/Forget?token=" + finalString + "'>Reset Password</a></h2>";

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("vcw159@gmail.com"));
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = "Reset Your Password";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailbody };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("vcw159@gmail.com", "dllzsofxnvgbipxf");
                smtp.Send(email);
                smtp.Disconnect(true);

                /* /#endregion Send Mail/*/
                TempData["Error"] = "Check your email to reset password";
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult Newpass()
        {

            return View();
        }
       
    }
}
