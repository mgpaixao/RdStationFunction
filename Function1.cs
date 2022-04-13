using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RdStationFunction.Models;
using RdStationFunction.Services;

namespace RdStationFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string email = data?.email;
            string tag = data?.tag;

            if (email == null || tag == null)
            {
                return new BadRequestObjectResult("Parametros inválido");
            }

            var result = await RdStationService.UpdateTags(email, tag, GetCredential());

            if (!result)
            {
                return new BadRequestObjectResult("Falha ao tentar atualizar a Tag");
            }

            return new OkObjectResult("Tags atualizado com sucesso");
        }

        private static ClientCredential GetCredential()
        {
            return new ClientCredential
            {
                ClientId = Environment.GetEnvironmentVariable("ClientId"),
                ClientSecret = Environment.GetEnvironmentVariable("ClientSecret"),
                RefreshToken = Environment.GetEnvironmentVariable("RefreshToken")
            };
        }
    }
}
