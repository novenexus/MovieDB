using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MDB.UserLikes.Service
{
    public class KafkaProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<KafkaProducer> _logger;

        public KafkaProducer(IConfiguration config, ILogger<KafkaProducer> logger)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"]
            };

            _producer = new ProducerBuilder<string, string>(producerConfig).Build();
            _logger = logger;
        }

        public async Task ProduceMessageAsync(string topic, string key, string message)
        {
            try
            {
                var dr = await _producer.ProduceAsync(topic, new Message<string, string>
                {
                    Key = key,
                    Value = message
                });

                _logger.LogInformation($"Message '{dr.Value}' successfully sent to Kafka topic {topic}.");
            }
            catch (ProduceException<string, string> e)
            {
                _logger.LogError($"Failed to deliver message: {e.Message}");
            }
        }
    }
}
