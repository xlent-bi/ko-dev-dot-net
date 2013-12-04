using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;

namespace AmountsAll
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString =
                  "Endpoint=sb://xlentdevdemo1.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=2oiWkEyXHaVepvqtOSXSnGvnEHtaASPUyyRwX0q/hto=";
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.TopicExists("Second"))
            {
                namespaceManager.CreateTopic("Second");
            }
            var topic = TopicClient.CreateFromConnectionString(connectionString, "Second");
            if (!namespaceManager.SubscriptionExists(topic.Path, "All"))
            {
                namespaceManager.CreateSubscription(topic.Path, "All");
            }

            var receiver = SubscriptionClient.CreateFromConnectionString(
                connectionString, topic.Path, "All", ReceiveMode.ReceiveAndDelete);

            while (true)
            {
                BrokeredMessage message;
                do
                {
                    message = receiver.Receive();
                } while (message == null);

                var order = message.GetBody<MyLibrary.Order>();
                Console.WriteLine("Order noted ({0}).", order);
            }
        }
    }
}
