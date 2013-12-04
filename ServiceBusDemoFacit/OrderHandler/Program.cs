using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;

namespace OrderHandler
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
            if (!namespaceManager.TopicExists("Second"))
            {
                namespaceManager.CreateTopic("Second");
            }

            var receiver = QueueClient.CreateFromConnectionString(connectionString, "First", ReceiveMode.ReceiveAndDelete);
            var sender = TopicClient.CreateFromConnectionString(connectionString, "Second");

            while (true)
            {
                BrokeredMessage message;
                do
                {
                    message = receiver.Receive();
                } while (message == null);

                var order = message.GetBody<MyLibrary.Order>();
                var newMessage = new BrokeredMessage(order);
                newMessage.Properties.Add("Amount", order.Amount);
                sender.Send(newMessage);
                Console.WriteLine("Order received ({0}) and sent.", order);
            }
        }
    }
}
