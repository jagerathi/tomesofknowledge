using System;

namespace DeltekVantageInterface
{
    public class AppSettings
    {
        public string BaseUrl { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ACRProjectOutputFile { get; set; }
        public string HBProjectOutputFile { get; set; }
        public string ACRUserOutputFile { get; set; }
        public string HBUserOutputFile { get; set; }
        public bool CustomerIsBBB { get; set; }
        public string LocationCode { get; set; }
        public bool CustomerIsMorphosis { get; set; }


    }
    public class Authentication
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    public class Project
    {
        public string ProjectNumber
        {
            get
            {
                var x = WBS1;
                if (!string.IsNullOrWhiteSpace(WBS2))
                    x += "." + WBS2;
                if (!string.IsNullOrWhiteSpace(WBS3))
                    x += "." + WBS3;
                return x;

            }
        }

        public string ProjectName => LongName;

        public string WBS1 { get; set; }
        public string WBS2 { get; set; }
        public string WBS3 { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string ChargeType { get; set; }
        public string SubLevel { get; set; }
        public string Status { get; set; }
        public string ProjMgrName { get; set; }
        public string ContractNo { get; set; }

    }

    public class User
    {
        public string Employee { get; set; }
        public string UserId => Employee;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        public string NetworkLogin
        {
            get
            {
                if (string.IsNullOrEmpty(Email)) return UserId;

                var index = Email.IndexOf("@", StringComparison.Ordinal);
                if (index > 0)
                {
                    return Email.Substring(0, index);
                }
                return UserId;
            }
        }
    }

    public class ProjectCustomTabFields
    {
        public string Select1 { get; set; }
        public string Select2 { get; set; }
        public string Select3 { get; set; }
        public string Select4 { get; set; }
        public string Select5 { get; set; }
    }
}