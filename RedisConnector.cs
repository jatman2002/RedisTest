using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisTest
{
    internal class RedisConnector
    {

        public static RedisConnector redis = new RedisConnector();
        private const string REDIS_NAME = "localhost";
        private const int PORT = 6379;
        private ConfigurationOptions configurationOptions = new ConfigurationOptions {
            EndPoints = { { REDIS_NAME, PORT } },
            //ConnectRetry = 1, // The number of retries when connecting to Redis, default is 3
            ConnectTimeout = 1000, // How long to wait per retry in ms, default is 5000ms
            SyncTimeout = 1000, // How long to wait for synchronous operations in ms, default is 5000ms
            //AsyncTimeout = 1000
        };

        private RedisConnector() { }
        public string readFromRedis(string key)
        {
            try
            {
                Console.WriteLine("Connecting to Redis");
                var redisConnection = ConnectionMultiplexer.Connect(configurationOptions.ToString());
                Console.WriteLine("Redis connected");

                IDatabase db = redisConnection.GetDatabase();
                Console.WriteLine("DB connected");
                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();

                Console.WriteLine($"Reading key={key}");
                string value = db.StringGet(key);
                Console.WriteLine($"Obtained:\n{value}");

                Console.WriteLine("Closing connection\n");
                redisConnection.Close();

                return value;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public  void writeToRedis(string key, string value, int timeOut=0)
        {
            try
            {
                Console.WriteLine("\nConnecting to Redis");
                var redisConnection = ConnectionMultiplexer.Connect(configurationOptions.ToString());
                Console.WriteLine("Redis connected");

                IDatabase db = redisConnection.GetDatabase();
                Console.WriteLine("DB connected");

                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();
                Console.WriteLine($"Writing\nkey={key}\nvalue={value}");

                if (timeOut > 0)
                {
                    db.StringSet(key, value, TimeSpan.FromSeconds(10)); // Timespan part is the TTL for the entry in redis
                }
                else
                {
                    db.StringSet(key, value);
                }

                Console.WriteLine("Closing connection\n");
                redisConnection.Close();
            }
            catch (Exception ex)
            {
              Console.WriteLine(ex.Message);
            }
        }

        // delete
        public void deleteFromRedis(string key)
        {
            try
            {
                Console.WriteLine("\nConnecting to Redis");
                var redisConnection = ConnectionMultiplexer.Connect(configurationOptions.ToString());
                Console.WriteLine("Redis connected");

                IDatabase db = redisConnection.GetDatabase();
                Console.WriteLine("DB connected");

                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();

                Console.WriteLine($"Deleting key={key}");
                db.KeyDelete(key);

                Console.WriteLine("Closing connection\n");
                redisConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // update
        //"[{\"Id\":1,\"Name\":\"Alice Johnson\",\"Age\":27,\"Position\":\"Manager\"},{\"Id\":2,\"Name\":\"Bob Smith\",\"Age\":21,\"Position\":\"Developer\"}]"
        //"[{\"Id\":1,\"Name\":\"Alice Johnson\",\"Age\":27,\"Position\":\"Manager\"}]"
        //"[{\"Id\":1,\"Name\":\"Alice Johnson\",\"Age\":27,\"Position\":\"Manager\"},{\"Id\":2,\"Name\":\"Bob Smith\",\"Age\":21,\"Position\":\"Developer\"},{\"Id\":3,\"Name\":\"John Smith\",\"Age\":55,\"Position\":\"Senior Developer\"}]"
        public void updateRedis(string key, string value)
        {
            try
            {
                Console.WriteLine("\nConnecting to Redis");
                var redisConnection = ConnectionMultiplexer.Connect(configurationOptions.ToString());
                Console.WriteLine("Redis connected");

                IDatabase db = redisConnection.GetDatabase();
                Console.WriteLine("DB connected");

                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();

                db.StringSet(key, value);

                Console.WriteLine("Closing connection\n");
                redisConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
