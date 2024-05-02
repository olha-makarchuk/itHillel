﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Application.Services
{
    public class Consumer
    {
        public string Listening()
        {
            string messageText = null;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            var connection = factory.CreateConnection();
            using
            var channel = connection.CreateModel();
            channel.QueueDeclare("product", exclusive: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                messageText = $"Product message received: {message}";
            };
            channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);

            return messageText;
        }
    }
}
