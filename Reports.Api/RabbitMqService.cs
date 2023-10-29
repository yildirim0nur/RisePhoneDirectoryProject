using Contacts.Application.Service.Contact.Interface;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using Shared.Dto.Response;
using System.ComponentModel;
using System.Text;

namespace Reports.Api
{
    public static class RabbitMqService
    {
        public static IApplicationBuilder UseRabbitMq(this IApplicationBuilder app)
        {
            //var _reportSettings = app.ApplicationServices.GetRequiredService<IOptions<ReportSettings>>().Value;
            var contanctService = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IContactReportSvc>();

            CreateConsumer<Shared.Dto.Response.ContactResDto>(QueueTypeEnum.ContactCreated, app);
            CreateConsumer<Guid>(QueueTypeEnum.ContactDeleted, app);
            CreateConsumer<ContactDetailResDto>(QueueTypeEnum.DetailCreated, app);
            CreateConsumer<Guid>(QueueTypeEnum.DetailDeleted, app);
            CreateConsumer<object>(QueueTypeEnum.ReportRequested, app);


            return app;
        }

        public static void CreateConsumer<T>(QueueTypeEnum queueType, IApplicationBuilder app)
        {
            var contanctService = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IContactReportSvc>();
            var conn = "amqp://guest:guest@localhost:5672";

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

            var consumerEvent = new EventingBasicConsumer(channel);

            consumerEvent.Received += (ch, ea) =>
            {
                var reportService = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IContactReportSvc>();
                var incomingModel = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(ea.Body.ToArray()));

                switch (queueType)
                {
                    case QueueTypeEnum.ContactCreated:
                        reportService.CreateContact(incomingModel as Shared.Dto.Response.ContactResDto);
                        break;
                    case QueueTypeEnum.ContactDeleted:
                        reportService.DeleteContact(Guid.Parse(incomingModel.ToString()));
                        break;
                    case QueueTypeEnum.DetailCreated:
                        reportService.AddContactDetail(incomingModel as ContactDetailResDto);
                        break;
                    case QueueTypeEnum.DetailDeleted:
                        reportService.RemoveContactDetail(Guid.Parse(incomingModel.ToString()));
                        break;
                    case QueueTypeEnum.ReportRequested:
                        reportService.GenerateExcel(Guid.Parse(incomingModel.ToString()));
                        break;
                }

                Console.WriteLine("Data received");
                Console.WriteLine(Encoding.UTF8.GetString(ea.Body.ToArray()));
            };

            channel.BasicConsume(queue, true, consumerEvent);
        }
    }



    public class ContactResDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
    }



    public static class EnumExtensionMethods
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}
