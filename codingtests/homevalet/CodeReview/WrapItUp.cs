using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CodeReview
{
    public class WrapItUp
    {
        public static string _connectionString;

        private static IConfigurationRoot _configuration = new ConfigurationBuilder()
                                                     .SetBasePath(Directory.GetCurrentDirectory())
                                                     .AddJsonFile("appsettings.json", true, true)
                                                     .AddEnvironmentVariables()
                                                     .Build();
        
        public static async void WrapIt(string key)
        {
            var files = await GetFiles(key);

            using (MemoryStream zipToOpen = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    foreach (var file in files)
                    {
                        ZipArchiveEntry readmeEntry = archive.CreateEntry(file);

                        using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                        {
                            writer.WriteLine(new StreamReader(File.OpenRead(file)).ReadToEnd());
                        }
                    }
                }


                await SendNotification(_configuration.GetSection("email").Get<string>(), zipToOpen.ToString());
            }
        }

        public static async Task<string[]> GetFiles(string key)
        {
            var sqlClient = new SqlConnection(_connectionString);
            await sqlClient.OpenAsync();
            var cmd = "select name from files where key = ''" + key + "'";
            var files = await sqlClient.QueryAsync<string>(cmd);

            return files.ToArray();
        }

        private static async Task SendNotification(string email, string stuff)
        {
            var sender = new SmtpClient();
            sender.Host = "smtp.example.com";
            sender.Port = 25;
            
            MailMessage msg = new MailMessage(email, email, "Wrapped it up", stuff);
            await sender.SendMailAsync(msg);
        }
    }
}