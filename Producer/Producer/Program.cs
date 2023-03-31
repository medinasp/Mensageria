﻿using RabbitMQ.Client;
using System.Text;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            //var factory = new ConnectionFactory()
            //{
            //    HostName = "localhost",
            //    UserName = "guest",
            //    Password = "guest",
            //    Port = 5672
            //};

            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "webQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            const string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "webQueue",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent {message}");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}