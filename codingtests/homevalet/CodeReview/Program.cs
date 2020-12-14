using System.IO;
using Microsoft.Extensions.Configuration;

namespace CodeReview
{
    class Program
    {
        static void Main(string[] args)
        {
            WrapItUp._connectionString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                   .AddJsonFile("appsettings.json", true, true)
                                                                   .AddEnvironmentVariables()
                                                                   .Build()
                                                                   .GetConnectionString("default");

            var configRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var config = new Config();
            configRoot.GetSection("AppSettings").Bind(config);


            WrapItUp.WrapIt(args[0]);
        }
    }
}