using System.Collections.Generic;

namespace Client.MVC.Models
{
    public class ValueViewModel
    {
        public List<string> PublicValues { get; set; }
        public List<string> SecretValues { get; set; }
        public List<string> ManagementValues { get; set; }
        public List<string> RecruitmentValues { get; set; }
        public bool PublicAuthorized { get; set; }
        public bool SecretAuthorized { get; set; }
        public bool ManagementAuthorized { get; set; }
        public bool RecruitmentAuthorized { get; set; }

        public ValueViewModel(List<string> publicValues, List<string> secretValues,
            List<string> managementValues, List<string> recruitmentValues, bool publicAuthorized, bool secretAuthorized,
            bool managementAuthorized, bool recruitmentAuthorized)
        {
            PublicValues = publicValues;
            SecretValues = secretValues;
            ManagementValues = managementValues;
            RecruitmentValues = recruitmentValues;

            PublicAuthorized = publicAuthorized;
            SecretAuthorized = secretAuthorized;
            ManagementAuthorized = managementAuthorized;
            RecruitmentAuthorized = recruitmentAuthorized;
        }
    }
}