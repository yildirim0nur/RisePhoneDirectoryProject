using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Shared
{
    public static class QueueSender
    {
        public static void Send<T>(T dto, QueueTypeEnum queueType, string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                var conn = connectionString;
                var queue = queueType.GetEnumDescription() + "Queue";
                var exchange = queueType.GetEnumDescription() + "Exchange";
                ConnectionFactory connectionFactory = new()
                {
                    Uri = new Uri(conn)
                };
                var connection = connectionFactory.CreateConnection();
                var channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange, "direct");
                channel.QueueDeclare(queue, false, false, false);
                channel.QueueBind(queue, exchange, queue);
                channel.BasicPublish(exchange, queue, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)));
            }

        }
    }
}




