using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;



namespace SignupsWorker1
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        private string qname = "signups";

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

        private void SaveToStorage(string email, string password)
        {
            string tableName = "Registrerade";
            //Connection till table storage account
            CloudStorageAccount account = CloudStorageAccount.Parse(tableConnectionString);

            CloudTableClient tableStorage = account.CreateCloudTableClient();

            CloudTable table = tableStorage.GetTableReference(tableName);
            table.CreateIfNotExists();

            //Skapar den entitet som ska in i storage
            Person person = new Person(email);
            person.Email = email;
            person.Password = password;

            //Sparar personen i signups table
            TableOperation insertOperation = TableOperation.Insert(person);
            table.Execute(insertOperation);
        }

        private void DeleteFromStorage(Person person)
        {
            // Retrieve storage account from connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting(connectionString));

            // Create the table client
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            //Create the CloudTable that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("signups");

            // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<Person>("signups", "chrille");

            // Execute the operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Assign the result to a CustomerEntity.
            Person deletePerson = (Person)retrievedResult.Result;

            // Create the Delete TableOperation.
            if (deletePerson != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deletePerson);

                // Execute the operation.
                table.Execute(deleteOperation);

                Console.WriteLine("user deleted.");
            }

            else
                Console.WriteLine("Could not retrieve the entity.");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                Thread.Sleep(10000);
                Trace.TraceInformation("Processing Signups..", "Information");

                QueueClient qc = QueueClient.CreateFromConnectionString(connectionString, qname);

                BrokeredMessage msg = qc.Receive();

                if (msg != null)
                {
                    try
                    {
                        Trace.WriteLine("New Signup processed: " + msg.Properties["email"] + msg.Properties["password"]);
                        msg.Complete();
                        SaveToStorage((string)msg.Properties["email"], msg.Properties["password"].ToString());
                    }
                    catch (Exception)
                    {
                        msg.Abandon();
                    }
                }

            }
        }
    }
}
