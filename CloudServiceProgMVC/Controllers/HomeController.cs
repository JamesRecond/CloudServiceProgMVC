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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string LoginEmail, string LoginPassword)
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
            if (person != null)
            {
                if (person.Email == LoginEmail && person.Password == LoginPassword)
                {
                    return RedirectToAction("Registry");
                }
            }
            else
                Console.WriteLine("You are not registred.");

            return View();
        }

        public ActionResult LoggedIn()
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

            return View();
        }

        [HttpGet]
        public ActionResult RegistryDisplay()
        {
            return View();
        }

        public ActionResult Sample()
        {
            return View();
        }

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
    }
}