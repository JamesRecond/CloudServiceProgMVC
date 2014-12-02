using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage.Table;


namespace CloudServiceProgMVC.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        string qname = "signups";

        private string user;
        private string userPassword;
        private string UserAndPassword;

        private string tableConnectionString = CloudConfigurationManager.GetSetting("TableStorageConnection");

        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult DeleteUser()
        {
            return View();
        }

        public ActionResult Login(string email, string password)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registry(string email, string password)
        {
            ViewBag.email = email;

            var nm = NamespaceManager.CreateFromConnectionString(connectionString);
            QueueDescription qd = new QueueDescription(qname);
            //Ställ in Max size på queue på  2GB
            qd.MaxSizeInMegabytes = 2048;
            //Max Time To Live är 5 minuter  
            qd.DefaultMessageTimeToLive = new TimeSpan(0, 5, 0);

            if (!nm.QueueExists(qname))
            {
                nm.CreateQueue(qd);
            }
            QueueClient qc = QueueClient.CreateFromConnectionString(connectionString, qname);

            //Skapa msg med email properaty och skicka till QueueClient
            var bm = new BrokeredMessage();
            bm.Properties["email"] = email;
            bm.Properties["password"] = password;
            qc.Send(bm);
            
            //user = email;
            //userPassword = password;
            //UserAndPassword = user + " pw: " + userPassword;
            //ViewBag.testaallskit = bm.Properties.Take(3).Select(c=>c.Value); 

            return View();
        }

        [HttpGet]
        public ActionResult ShowDataFromTable()
        {    
            
            return View();
        }
     
        [HttpGet]
        public ActionResult RegistryDisplay()
        {
            
            return View();
        }
        //[HttpPost]
        //public ActionResult RegistryDisplay()
        //{

        //    return View();
        //}
        public ActionResult WhatHappened()
        {
            ViewBag.email = user;
            ViewBag.userAndPassword = UserAndPassword;
            var bm = new BrokeredMessage();
            var userTest = bm.Properties.GetType().GetProperties();
            ViewBag.test = userTest;

            return View();
        }

        public ActionResult Sample()
        {
            return View();
        }

        //public ActionResult SignUp()
        //{
        //    //ViewBag.Message = "Newsletter Signups";
        //    ret
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Knas()
        {
            return View();
        }

        //public ActionResult Kaos()
        //{
        //    return View();
        //}
    }
}