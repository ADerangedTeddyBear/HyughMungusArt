using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArtPortfolioASPEdition.Models;
using ArtPortfolioASPEdition.DAL;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using MySql.Data.MySqlClient.Memcached;

namespace ArtPortfolioASPEdition.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Commissions()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Page2(CommissionDetails commission)
        {
            DALCommission dp = new DALCommission(configuration);
            int uID = dp.addCommission(commission);
            this.UserConfirmationMail(commission);
            this.SendEmail(commission);
            return View(commission);
        }

        public IActionResult RandomKittens()
        {
            return View();
        }
        public IActionResult CommissionWorks()
        {
            return View();
        }
        public IActionResult EtcWorks()
        {
            return View();
        }

        public IActionResult MessageReceived(ContactDetails contactMessage)
        {
            DALMessages dm = new DALMessages(configuration);
            int uID = dm.addMessage(contactMessage);
            this.SendMessage(contactMessage);
            this.ReceiveMessage(contactMessage);
            return View(contactMessage);
        }

        //General Message to me
        public void ReceiveMessage(ContactDetails contactMessage)
        {
            string to = "jokernaik69@gmail.com";
            string body = $"{contactMessage.message}";

            string subject = $"Message from {contactMessage.name} at {contactMessage.email}";
            MailMessage mail = new MailMessage();
            string senderEmail = EmailCredentials.EmailAddress;
            string senderPassword = EmailCredentials.Password;

            mail.From = new MailAddress(senderEmail);
            mail.To.Add(to);
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Subject = subject;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        //General Message to sender. 
        public void SendMessage(ContactDetails contactMessage)
        {
            string to = contactMessage.email;
            string body = $"Hello there {contactMessage.name}! Your message has been sent. The following is the message that you have sent: \" {contactMessage.message} \". Thank you for sending the message and I will get back to you ASAP. ";

            string subject = $"Message Sent";
            MailMessage mail = new MailMessage();
            string senderEmail = EmailCredentials.EmailAddress;
            string senderPassword = EmailCredentials.Password;


            mail.From = new MailAddress(senderEmail);
            mail.To.Add(to);
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Subject = subject;


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        //Commission Message to me
        public void SendEmail(CommissionDetails commission)
        {
            string to = "jokernaik69@gmail.com";
            string body = $"{commission.description} \n. Send results to {commission.email} upon completion. ";

            string subject = $"Commission from {commission.email}";
            MailMessage mail = new MailMessage();
            string senderEmail = EmailCredentials.EmailAddress;
            string senderPassword = EmailCredentials.Password;

            mail.From = new MailAddress(senderEmail);
            mail.To.Add(to);
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Subject = subject;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        //Email sent to requester so that the requester can verify that they have sent a request. 
        public void UserConfirmationMail(CommissionDetails commission)
        {
            string to = commission.email;
            string body = $"Your request has been received. The following is the description of your request: \n {commission.description}. I will look over your commission request and see what I can do. ";

            string subject = $"Commission confirmation";
            MailMessage mail = new MailMessage();
            string senderEmail = EmailCredentials.EmailAddress;
            string senderPassword = EmailCredentials.Password;


            mail.From = new MailAddress(senderEmail);
            mail.To.Add(to);
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Subject = subject;


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
    }
}
