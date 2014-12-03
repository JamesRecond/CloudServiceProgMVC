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

        private string qname = "Login"; 

        string tableConnectionString = CloudConfigurationManager.GetSetting("TableStorageConnection");
        

        public override void Run()
        {
            Trace.TraceInformation("SignupsWorker1 is running");

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

            Trace.TraceInformation("SignupsWorker1 has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("SignupsWorker1 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("SignupsWorker1 has stopped");
        }

        private Person CheckStorage(string email, string password)
        {
            try
            {
                //StorageCredentials creds = new StorageCredentials(person.Email);
                CloudStorageAccount account = CloudStorageAccount.Parse(connectionString);

                CloudTableClient client = account.CreateCloudTableClient();
                CloudTable table = client.GetTableReference("users");

                TableOperation retrieveOperation = TableOperation.Retrieve<Person>(email, password);

                TableResult user = table.Execute(retrieveOperation);

                Person person = new Person();
                person = (Person) user.Result;
            //    person.Email = account;

                if (person != null)
                {
                    //Console.WriteLine("Product: {0}", ((Person)query.Result).Email);
                    return person;
                }
                else
                {
                    Console.WriteLine("The fag was not found.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        
      

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                Thread.Sleep(10000);
                Trace.TraceInformation("Processing Signups..", "Information");

                QueueClient qc = QueueClient.CreateFromConnectionString(connectionString, qname);

                BrokeredMessage msg = qc.Receive();

                //QueueClient qcDelete = QueueClient.CreateFromConnectionString(connectionString, qnameDelete);
                //BrokeredMessage msgDelete = qc.Receive();

                if (msg != null)
                {
                    try
                    {
                        Trace.WriteLine("New login processed: " + msg.Properties["email"] + msg.Properties["password"]);
                        msg.Complete();
                        CheckStorage((string) msg.Properties["email"], msg.Properties["password"].ToString());
                    }
                    catch (Exception)
                    {
                        msg.Abandon();
                    }
                }
                //if (msgDelete != null)
                //{
                //    try
                //    {
                //        Trace.WriteLine("New delete is processed: " + msgDelete.Properties["email"] + msgDelete.Properties["password"]);
                //        msgDelete.Complete();
                //        DeleteFromStorageTest(msgDelete.Properties["email"].ToString());
                //    }
                //    catch (Exception)
                //    {
                //        msgDelete.Abandon();
                //    }

                //}
            }
        }
    }
}
