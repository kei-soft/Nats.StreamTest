using System.Text;

using STAN.Client;

namespace Nats.StreamTest.Pub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string clientId = $"pub-{Guid.NewGuid().ToString()}";

            var cf = new StanConnectionFactory();
            StanOptions options = StanOptions.GetDefaultOptions();
            options.NatsURL = "nats://localhost:4222";

            using (var c = cf.CreateConnection("test-cluster", clientId, options))
            {
                for (int i = 1; i <= 25; i++)
                {
                    string message = $"[{DateTime.Now.ToString("hh:mm:ss:fffffff")}] Message {i}";
                    Console.WriteLine($"Pubish - Sending {message}");

                    c.Publish("nats.streaming.channel", Encoding.UTF8.GetBytes(message));
                }
            }

            Console.ReadKey(true);
        }
    }
}