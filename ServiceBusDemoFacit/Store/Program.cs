using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using MyLibrary;
using System;
using System.Threading;

namespace Store
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString =
                "Endpoint=sb://xlentdevdemo1.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=2oiWkEyXHaVepvqtOSXSnGvnEHtaASPUyyRwX0q/hto=";
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.QueueExists("First"))
            {
                namespaceManager.CreateQueue("First");
            }

            var sender = QueueClient.CreateFromConnectionString(connectionString, "First", ReceiveMode.ReceiveAndDelete);

            var storeNumber = Utils.RandomStoreNumber();
            var sequenceNumber = 1;

            while (true)
            {
                var order = new MyLibrary.Order
                {
                    StoreNumber = storeNumber,
                    SequenceNumber = sequenceNumber++,
                    Amount = Utils.RandomAmount()
                };

                var message = new BrokeredMessage(order);

                sender.Send(message);
                System.Console.WriteLine("Order sent ({0})", order);
                Thread.Sleep(3000);
            }
        }
    }
}
