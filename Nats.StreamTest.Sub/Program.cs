using System.Text;

using STAN.Client;

namespace Nats.StreamTest.Sub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //if (args.Length != 1)
            //{
            //    Console.WriteLine("Usage: consumer <clientid>");
            //    return;
            //}

            string clientId = "ClientID" + Guid.NewGuid().ToString().Replace("-", "");

            var cf = new StanConnectionFactory();

            StanOptions options = StanOptions.GetDefaultOptions();
            options.NatsURL = "nats://localhost:4222";

            using (var c = cf.CreateConnection("test-cluster", clientId, options))
            {
                var opts = StanSubscriptionOptions.GetDefaultOptions();

                //opts.DeliverAllAvailable();
                //opts.StartAt(15);
                //opts.StartAt(TimeSpan.FromSeconds(10));
                //opts.StartAt(new DateTime(2019, 9, 18, 9, 0, 0));
                //opts.StartWithLastReceived();
                //opts.DurableName = "durable";

                var s = c.Subscribe("nats.streaming.channel", opts, (obj, args) =>
                {
                    string message = Encoding.UTF8.GetString(args.Message.Data);
                    Console.WriteLine($"[#{args.Message.Sequence}] {message}");
                });

                Console.WriteLine($"Subcribe - client id '{clientId}'");
                Console.ReadKey(true);

                //s.Unsubscribe();
                c.Close();
            }
        }
    }
}