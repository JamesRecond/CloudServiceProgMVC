using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace LoginWorker
{
    public class WorkerRole : RoleEntryPoint
    {

        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        string tableConnectionString = CloudConfigurationManager.GetSetting("TableStorageConnection");

        private string qname = "Login";

        private Person user;

        public override void Run()
        {
            Trace.TraceInformation("LoginWorker is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("loginWorker has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("loginWorker is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("loginWorker has stopped");
        }

        private Person CheckStorage(string email, string password)
        {
            string tableName = "Registrerade";

            try
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(tableConnectionString);

                CloudTableClient client = account.CreateCloudTableClient();
                CloudTable table = client.GetTableReference(tableName);

                TableOperation retrieveOperation = TableOperation.Retrieve<Person>("signups", email);

                TableResult user = table.Execute(retrieveOperation);

                Person person = new Person();
                person = (Person)user.Result;

                if (person.Email == email)
                {
                    Trace.WriteLine(person.Email);

                    Console.WriteLine(person.Email);
                    return person;
                }
                else
                {
                    Trace.WriteLine("The fag was not found.");
                    Console.WriteLine("FAGGITY");
                    return null;
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                Console.WriteLine(ex);

            }
            return null;

        }

        private void UpdatePerson(string currentPage, string email)
        {
            string tableName = "Registrerade";
            //Connection till table storage account
            CloudStorageAccount account = CloudStorageAccount.Parse(tableConnectionString);

            CloudTableClient tableStorage = account.CreateCloudTableClient();

            CloudTable table = tableStorage.GetTableReference(tableName);
            table.CreateIfNotExists();

            TableOperation retrieveOperation = TableOperation.Retrieve<Person>("signups", email);
            TableResult retrievedResult = table.Execute(retrieveOperation);
            //Skapar den entitet som ska in i storage
            Person person = (Person)retrievedResult.Result;
            if (person != null)
            {
                person.CurrentPage = currentPage;
                TableOperation updateOperation = TableOperation.Replace(person);
                table.Execute(updateOperation);
            }

            //Sparar personen i signups table
            TableOperation insertOperation = TableOperation.InsertOrReplace(person);
            table.Execute(insertOperation);
        }

        private async Task<Person> RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                Thread.Sleep(00);
                Trace.TraceInformation("Processing check..", "Information");

                QueueClient qc = QueueClient.CreateFromConnectionString(connectionString, qname);

                BrokeredMessage msg = qc.Receive();

                if (msg.Properties["action"].ToString() == "Update")
                {
                    UpdatePerson(msg.Properties["currentPage"].ToString(), msg.Properties["email"].ToString());
                }

                if (msg != null)
                {
                    try
                    {
                        Trace.WriteLine("New login processed: " + msg.Properties["LoginEmail"] + msg.Properties["LoginPassword"]);
                        msg.Complete();
                        user = CheckStorage((string)msg.Properties["LoginEmail"], msg.Properties["LoginPassword"].ToString());

                        if (user != null)
                        {
                            var bm = new BrokeredMessage();
                            bm.Properties["user"] = user.Email;
                            bm.Properties["Validated"] = true;
                            qc.Send(bm);
                        }
                        else
                        {
                            var bm = new BrokeredMessage();
                            bm.Properties["Validated"] = false;
                            qc.Send(bm);
                        }
                        return user;
                    }
                    catch (Exception)
                    {
                        msg.Abandon();
                    }
                }
                return null;

            }
        }
    }
}
