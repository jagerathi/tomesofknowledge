using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DeltekVantageInterface
{
    class Program
    {
        static void Mainxxx(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            var settings = new AppSettings();
            configuration.GetSection("AppSettings").Bind(settings);

            var client = new RestClient($"{settings.BaseUrl}/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer {{oauth_token}}");
            request.AddParameter("Username", settings.UserId);
            request.AddParameter("Password", settings.Password);
            request.AddParameter("grant_type", "password");
            request.AddParameter("Integrated", "N");
            request.AddParameter("database", settings.Database);
            request.AddParameter("Client_Id", settings.ClientId);
            request.AddParameter("client_secret", settings.ClientSecret);
            var response = client.Execute(request);

            var authentication = JsonConvert.DeserializeObject<Authentication>(response.Content);

            var activeProjects = new List<Project>();

            var fieldFilter = "fieldFilter=WBSNumber,Name,LongName,ChargeType,SubLevel,WBS1,WBS2,WBS3,Status,ProjMgrName";

            var page = 1;
            var pageSize = 500;

            var startTime = DateTime.Now;

            while (true)
            {
                client = new RestClient($"{settings.BaseUrl}/project?pageSize={pageSize}&page={page}&{fieldFilter}");
                client.Timeout = -1;
                request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"bearer {authentication.access_token}");
                request.AlwaysMultipartFormData = true;
                response = client.Execute(request);

                var list = JsonConvert.DeserializeObject<List<Project>>(response.Content);

                if (settings.CustomerIsBBB)
                {
                    activeProjects.AddRange(list.Where(x => x.Status == "A" && x.SubLevel == "N").ToList());
                }
                else
                {
                    activeProjects.AddRange(list.Where(x => x.Status == "A" && string.IsNullOrWhiteSpace(x.WBS2)).ToList());
                }

                if (list.Count < pageSize)
                    break;

                var elapsed = DateTime.Now - startTime;
                Console.WriteLine($"{DateTime.Now} - Pages processed: {page}, Active Projects: {activeProjects.Count} of {page*pageSize} - Elapsed {elapsed.TotalSeconds}");

                page++;
            }

            activeProjects = activeProjects.OrderBy(x => x.ProjectNumber).ToList();

            if (settings.CustomerIsBBB)
            {
                var counter = 0;
                foreach (var project in activeProjects)
                {
                    client = new RestClient($"{settings.BaseUrl}/project/{project.ProjectNumber}/customTable/ProjectCustomTabFields");
                    client.Timeout = -1;
                    request = new RestRequest(Method.GET);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", $"bearer {authentication.access_token}");
                    request.AlwaysMultipartFormData = true;
                    response = client.Execute(request);

                    var data = JsonConvert.DeserializeObject<List<ProjectCustomTabFields>>(response.Content);

                    if (data.Count > 0)
                    {
                        var customData = data[0];
                        if (!string.IsNullOrEmpty(customData.Select4))
                        {
                            project.ChargeType = customData.Select4;
                        }
                    }

                    project.LongName = project.LongName.Replace("\"", "");

                    if (project.ChargeType == "R" || project.ChargeType == "NR")
                    {
                        project.ContractNo = settings.LocationCode + project.ChargeType;
                    }
                    else if (project.ChargeType.ToUpperInvariant() == "R EXEMPT" || project.ChargeType.ToUpperInvariant() == "R- EXEMPT")
                    {
                        project.ContractNo = "R-Exempt";
                    }
                    else if (project.ChargeType.ToUpperInvariant() == "NR EXEMPT" || project.ChargeType.ToUpperInvariant() == "NR- EXEMPT")
                    {
                        project.ContractNo = "NR-Exempt";
                    }
                    if (++counter % 50 == 0)
                    {
                        var elapsed = DateTime.Now - startTime;
                        Console.WriteLine($"Processed {counter} of {activeProjects.Count} - Elapsed {elapsed.TotalSeconds}");
                    }
                }
            }

            if(!string.IsNullOrEmpty(settings.ACRProjectOutputFile))
                CreateAcrOutputFile(activeProjects, settings.ACRProjectOutputFile);

            if(!string.IsNullOrEmpty(settings.HBProjectOutputFile))
                CreateHbOutputFile(activeProjects, settings.HBProjectOutputFile);


            var activeEmployees = new List<User>();

            fieldFilter = "fieldFilter=Employee,Status,FirstName,LastName,EMail";

            page = 1;
            pageSize = 500;

            while (true)
            {
                client = new RestClient($"{settings.BaseUrl}/employee?pageSize={pageSize}&page={page}&{fieldFilter}");
                client.Timeout = -1;
                request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"bearer {authentication.access_token}");
                request.AlwaysMultipartFormData = true;
                response = client.Execute(request);

                var list = JsonConvert.DeserializeObject<List<User>>(response.Content);

                if (list != null)
                {
                    activeEmployees.AddRange(list.Where(x => x.Status == "A").ToList());
                }

                if (list == null || list.Count < pageSize)
                    break;

                Console.WriteLine($"{DateTime.Now} - Pages processed: {page}, Active Employees: {activeEmployees.Count} of {page * pageSize}");

                page++;
            }

            if (!string.IsNullOrEmpty(settings.ACRUserOutputFile))
                CreateAcrUserOutputFile(activeEmployees, settings.ACRUserOutputFile);

            if (!string.IsNullOrEmpty(settings.HBUserOutputFile))
                CreateHbUserOutputFile(activeEmployees, settings.HBUserOutputFile);

        }

        private class RestRequest
        {
            public RestRequest(object get)
            {
                throw new NotImplementedException();
            }

            public bool AlwaysMultipartFormData { get; set; }

            public void AddHeader(string contentType, string applicationJson)
            {
                throw new NotImplementedException();
            }

            public void AddParameter(string username, string settingsUserId)
            {
                throw new NotImplementedException();
            }
        }

        private class RestResponse
        {
            public string Content;
        }

        private class RestClient
        {
            public RestClient(string s)
            {
                throw new NotImplementedException();
            }

            public int Timeout { get; set; }

            public RestResponse Execute(RestRequest request)
            {
                throw new NotImplementedException();
            }
        }

        private static void CreateAcrOutputFile(IEnumerable<Project> projects, string filename)
        {
            using (TextWriter writer = new StreamWriter(filename, false))
            {
                foreach (var project in projects)
                {
                    writer.WriteLine("\"{0}\",\"{1}\"", project.ProjectNumber, project.ProjectName);
                }
            }
        }

        private static void CreateHbOutputFile(IEnumerable<Project> projects, string filename)
        {
            using (TextWriter writer = new StreamWriter(filename, false))
            {
                foreach (var project in projects)
                {
                    writer.WriteLine(
                        "\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\"",
                        project.ProjectNumber, project.ProjectName, project.ContractNo, string.Empty, project.WBS1,
                        project.WBS2, project.WBS3, string.Empty, project.ProjMgrName, project.ChargeType, string.Empty);
                }
            }
        }

        private static void CreateAcrUserOutputFile(IEnumerable<User> users, string filename)
        {
            using (TextWriter writer = new StreamWriter(filename, false))
            {
                foreach (var user in users)
                {
                    writer.WriteLine("\"{0}\",,\"{1}\",\"{2}\",\"{3}\",,,,,\"{4}\",",
                        user.UserId, user.NetworkLogin, user.FirstName, user.LastName, user.Email);
                }
            }
        }

        private static void CreateHbUserOutputFile(IEnumerable<User> users, string filename)
        {
            using (TextWriter writer = new StreamWriter(filename, false))
            {
                foreach (var user in users)
                {
                    writer.WriteLine("\"{0}\",\"{1}\",\"{2} {3}\"", user.UserId, user.NetworkLogin, user.FirstName, user.LastName);
                }
            }
        }
    }

    internal class Method
    {
        public static object GET;
        public static object POST;
    }
}
