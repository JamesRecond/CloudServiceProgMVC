using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;


namespace CloudServiceProgMVC.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        string qname = "signups";

        private string user;

        private string tableConnectionString = CloudConfigurationManager.GetSetting("TableStorageConnection");

        public ActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Registry(string email)
        {
            ViewBag.email = email;

            var nm = NamespaceManager.CreateFromConnectionString(connectionString);
            QueueDescription qd = new QueueDescription(qname);
            //St�ll in Max size p� queue p�  2GB
            qd.MaxSizeInMegabytes = 2048;
            //Max Time To Live �r 5 minuter  
            qd.DefaultMessageTimeToLive = new TimeSpan(0, 5, 0);

            if (!nm.QueueExists(qname))
            {
                nm.CreateQueue(qd);
            }
            QueueClient qc = QueueClient.CreateFromConnectionString(connectionString, qname);

            //Skapa msg med email properaty och skicka till QueueClient
            var bm = new BrokeredMessage();
            bm.Properties["email"] = email;
            qc.Send(bm);
            user = email;

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
    }
}