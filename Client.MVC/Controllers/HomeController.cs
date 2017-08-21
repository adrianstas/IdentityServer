using Client.MVC.Code;
using Client.MVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Client.MVC.Code.Constants;

namespace Client.MVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> GetApiValues()
        {
            ViewBag.Title = "Values";

            var regularClient = RegularClient.GetClient(IdentityConstants.APIRegular);
            var secretClient = RegularClient.GetClient(IdentityConstants.APISecret);

            var publicValues = await regularClient.GetAsync("values/PublicValues")
                .ConfigureAwait(false);
            var managementValues = await regularClient.GetAsync("values/ManagementValues")
                .ConfigureAwait(false);
            var recruitmentValues = await regularClient.GetAsync("values/RecruitmentValues")
                .ConfigureAwait(false);
            var secretValues = await secretClient.GetAsync("values/SecretValues")
                .ConfigureAwait(false);

            var valuesAsString = await publicValues.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
            var secretAsString = await secretValues.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
            var managementAsString = await managementValues.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
            var recruitmentAsString = await recruitmentValues.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            var publicValuesDeserialized = publicValues.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<string>>(valuesAsString)
                : null;

            var secretValuesDeserialized = secretValues.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<string>>(secretAsString)
                : null;

            var managementValuesDeserialized = managementValues.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<string>>(managementAsString)
                : null;

            var recruitmentValuesDeserialized = recruitmentValues.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<string>>(recruitmentAsString)
                : null;

            var vm = new ValueViewModel(publicValuesDeserialized,
                secretValuesDeserialized,
                managementValuesDeserialized,
                recruitmentValuesDeserialized,
                secretValues.IsSuccessStatusCode,
                managementValues.IsSuccessStatusCode,
                recruitmentValues.IsSuccessStatusCode);

            return View("Index", vm);
        }
    }
}