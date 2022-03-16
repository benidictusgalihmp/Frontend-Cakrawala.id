using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cakrawala.Data
{
    public class AuthService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;

        public AuthService() 
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            
        }

        public async Task RegisterAsync(string username, string email, string password)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl+"users/register", string.Empty));
            try
            {
                string json = JsonSerializer.Serialize<RegisterFormat>(new RegisterFormat(username, email, password), serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success Register");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "users/login", string.Empty));
            LoginResponse logResp = new LoginResponse();

            Debug.WriteLine("Start Logging in...");

            try
            {
                LoginFormat item = new LoginFormat(email, password);

                Debug.WriteLine("Start Make kontent");
                string json = JsonSerializer.Serialize<LoginFormat>(item, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8 , "application/json");

                Debug.WriteLine("Start Send Post Asnyc");
                Debug.WriteLine(item.email);
                Debug.WriteLine(json);
                Debug.WriteLine(content);
                HttpResponseMessage response = await client.PostAsync(uri , content);

                Debug.WriteLine("End Send Post Asnyc");
                Debug.WriteLine(response.Content);
                if (response.IsSuccessStatusCode)
                {
                    string rawLogResp = await response.Content.ReadAsStringAsync();
                    logResp = JsonSerializer.Deserialize<LoginResponse>(rawLogResp, serializerOptions);

                    Debug.WriteLine(logResp.username);
                    Debug.WriteLine("Login Successful");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
            }

            return logResp;
        }



        private class LoginFormat
        {
            public string email { get; set; }
            public string password { get; set; }

            public LoginFormat(string email, string password)
            {
                this.email = email;
                this.password = password;
            }
        }

        public class LoginResponse
        {
            public string id { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string jwt_token { get; set; }

            public LoginResponse()
            {
                this.id = null;
                this.username = null;
                this.email = null;
                this.jwt_token = null;
            }

            public LoginResponse(string id, string username, string email, string jwtToken)
            {
                this.id = id;
                this.username = username;
                this.email=email;
                this.jwt_token = jwtToken;
            }
        }

        private class RegisterFormat
        {
            public string username;
            public string email;
            public string password;

            public RegisterFormat(string username, string email, string password)
            {
                this.username = username;
                this.email = email;
                this.password = password;
            }
        }
    }
}
