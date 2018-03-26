using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Text;

namespace ZalidaneSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(FormCollection form)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(form["comment"]))
                {
                    CreateAndSendEmail(form["Name"], form["Email"], form["Comment"]);
                }
                else
                {
                    ShowPopup("Comment Required");
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            
            return View("Index");
        }

        private void ShowPopup(string v)
        {
            Response.Write("<script type='text/javascript'>alert('TEST');</script>");
        }

        private void CreateAndSendEmail(string name, string email, string comment)
        {

            SmtpClient server = new SmtpClient("mail.zalidane.com", 26);
            server.Credentials = new System.Net.NetworkCredential("admin@zalidane.com", "H3c24w&");

            MailMessage message = new MailMessage()
            {
                From = new MailAddress(email, name),
                Subject = "Contact Message",
                Body = comment
            };
            
            message.To.Add(new MailAddress("admin@zalidane.com"));
            message.BodyEncoding = Encoding.UTF8;
            
            server.Send(message);
        }
    }
}