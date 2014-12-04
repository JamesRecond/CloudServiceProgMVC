using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;


namespace CloudServiceProgMVC.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        string qname = "signups";
        private string qnameLogin = "Login";

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

        [HttpGet]
        public ActionResult LoginTestResult()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginTestResult(string LoginEmail, string LoginPassword)
        {
            string tableName = "Registrerade";
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("TableStorageConnection"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference(tableName);

            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<Person>("signups", LoginEmail);

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            Person person = (Person)retrievedResult.Result;
            // Print the phone number of the result.
            if (person.Email == LoginEmail && person.Password == LoginPassword)
            {
                return RedirectToAction("LoggedIn");
            }
            else
                Console.WriteLine("You are not registred.");

            return View();
        }

        public ActionResult LoggedIn()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(string LoginEmail, string LoginPassword)
        //{
        //    var nmLogin = NamespaceManager.CreateFromConnectionString(connectionString);
        //    QueueDescription qdLogin = new QueueDescription(qnameLogin);
        //    //Ställ in Max size på queue på  2GB
        //    qdLogin.MaxSizeInMegabytes = 2048;
        //    //Max Time To Live är 5 minuter  
        //    qdLogin.DefaultMessageTimeToLive = new TimeSpan(0, 5, 0);

        //    if (!nmLogin.QueueExists(qnameLogin))
        //    {
        //        nmLogin.CreateQueue(qdLogin);
        //    }
        //    QueueClient qc = QueueClient.CreateFromConnectionString(connectionString, qnameLogin);

        //    //Skapa msg med email properaty och skicka till QueueClient
        //    var bm = new BrokeredMessage();
        //    bm.Properties["LoginEmail"] = LoginEmail;
        //    bm.Properties["LoginPassword"] = LoginPassword;
        //    qc.Send(bm);

        //    //user = email;
        //    //userPassword = password;
        //    //UserAndPassword = user + " pw: " + userPassword;
        //    //ViewBag.testaallskit = bm.Properties.Take(3).Select(c=>c.Value); 

        //    return RedirectToAction("MainPageLogged");
        //}

        public ActionResult MainPageLogged()
        {
            QueueClient qc = QueueClient.CreateFromConnectionString(connectionString, qnameLogin);

            BrokeredMessage msg = qc.Receive();


            try
            {
                if (msg != null)
                {
                    if ((bool)msg.Properties["Validated"])
                    {
                        ViewBag.testBoolean = msg.Properties["user"];
                        return View();
                    }
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
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