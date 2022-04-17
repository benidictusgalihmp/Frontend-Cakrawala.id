using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cakrawala.Data
{
    public class AuthService
    {
        HttpClient client;
        //JsonSerializerOptions serializerOptions;

        public AuthService() 
        {
            client = new HttpClient();
            /*
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            */
            
        }

        public async Task<bool> RegisterAsync(string username, string name, string email, string password)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl+"users/register", string.Empty));
            bool output = false;
            try
            {
                string json = JsonConvert.SerializeObject(new RegisterFormat(username, name, email, password));
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success Register");
                    output = true;
                }

                Debug.WriteLine(response.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return output;
        }

        public void Logout()
        {
            Application.Current.Properties["token"] = null;
            Application.Current.Properties["userId"] = null;
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
                string json = JsonConvert.SerializeObject(item);
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
                    logResp = JsonConvert.DeserializeObject<ResponseWrapper<LoginResponse>>(rawLogResp).data;

                    Debug.WriteLine(logResp.username);
                    Debug.WriteLine("Login Successful");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
                logResp.jwtToken = "error";
            }

            return logResp;
        }



        private class LoginFormat
        {
            [JsonProperty(PropertyName = "email")]
            public string email { get; set; }

            [JsonProperty(PropertyName = "password")]
            public string password { get; set; }

            public LoginFormat(string email, string password)
            {
                this.email = email;
                this.password = password;
            }
        }

        public class LoginResponse
        {
            [JsonProperty(PropertyName = "id")]
            public string id { get; set; }

            [JsonProperty(PropertyName = "username")]
            public string username { get; set; }

            [JsonProperty(PropertyName = "email")]
            public string email { get; set; }

            [JsonProperty(PropertyName = "jwt_token")]
            public string jwtToken { get; set; }

            public LoginResponse()
            {
                this.id = null;
                this.username = null;
                this.email = null;
                this.jwtToken = null;
            }

            public LoginResponse(string id, string username, string email, string jwtToken)
            {
                this.id = id;
                this.username = username;
                this.email=email;
                this.jwtToken = jwtToken;
            }
        }

        private class RegisterFormat
        {
            [JsonProperty(PropertyName = "username")]
            public string username { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string name { get; set; }

            [JsonProperty(PropertyName = "email")]
            public string email { get; set; }

            [JsonProperty(PropertyName = "password")]
            public string password { get; set; }

            public RegisterFormat(string username, string name, string email, string password)
            {
                this.username = username;
                this.name = name;
                this.email = email;
                this.password = password;
            }
        }
    }
}
