
namespace Lands.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;

    public class ApiService
    {

        // CheckConnection nos va a garantizar si hay coneccion de internet.
        public async Task<Response> CheckConnection()
        {
            // libreria CrossConnectivity sabemos si esta prendido el internet verifica que los settings se encuenrtre en off
            // valida si esta en modo avion y notifica no esta conectado
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Plase turn on your internet settings.",
                };
            }

            // realiza un ping a Google
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
            "google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check you internet connection.",
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }
        /*

        */
        public async Task<Response> PasswordRecovery(
            string urlBase,
            string servicePrefix,
            string controller,
            string email)
        {
            try
            {
                var useRequest = new UseRequest { email = email };
                var request = JsonConvert.SerializeObject(useRequest);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<TokenResponse> LoginFacebook(
            string urlBase,
            string servicePrefix,
            string controller,
            FaceBookResponse profile)

        {
            try
            {
                var request = JsonConvert.SerializeObject(profile);
                var content = new StringContent(
                request,
                Encoding.UTF8,
                "application/json");
                var client = new HttpClient();
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return null
                }

                var tokenResponse = await GetToken(urlBase, profile.Id, profile.Id);
                return tokenResponse;
            }
            catch
            {
                return null;
            }

        }

        public async Task<TokenResponse> GetToken(
            string urlBase,
            string username,
            string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token",
                new StringContent(string.Format(
                username, password),
                Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStreamAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(
                    resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }


    }
}
