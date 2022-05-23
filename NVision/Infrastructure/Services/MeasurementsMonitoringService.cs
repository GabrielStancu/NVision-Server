using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MeasurementsMonitoringService : BackgroundService
    {
        private readonly string _queueUri = "amqps://jchjxvxx:ZM0XdOye65LJHaVRPg-xA_o_mlexMRxP@cow.rmq2.cloudamqp.com/jchjxvxx";
        private readonly IServiceScopeFactory _factory;

        public MeasurementsMonitoringService(IServiceScopeFactory factory)
        {
            _factory = factory;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_queueUri) };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "measurements",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) => await OnMessageReceived(model, ea);
            channel.BasicConsume(queue: "measurements", autoAck: true, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() => { }, stoppingToken);
            }
        }

        private async Task OnMessageReceived(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var sensorReading = JsonConvert.DeserializeObject<SensorReadingDto>(message);

            using var scope = _factory.CreateScope();
            var sensorMeasurementRepository = scope.ServiceProvider.GetRequiredService<ISensorMeasurementRepository>();
            var readingToMeasurementConverter = scope.ServiceProvider.GetRequiredService<IReadingToMeasurementConverter>();
            var sensorMeasurement = await readingToMeasurementConverter.ConvertAsync(sensorReading);

            await sensorMeasurementRepository.InsertAsync(sensorMeasurement);
        }
    }
}
