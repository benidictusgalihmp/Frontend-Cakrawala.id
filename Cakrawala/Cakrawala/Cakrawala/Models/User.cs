using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakrawala.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string userId { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string userName { get; set; }
        public string password { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string displayName { get; set; }
        public string email { get; set; }
        public int balance { get; set; }
        public int exp { get; set; }
        public string level { get; set; }
        public LoginType loginType { get; set; }

        public User(string userId, string userName, string password, string displayName, string email, int balance, int exp, string level, LoginType loginType)
        {
            this.userId = userId;
            this.userName = userName;
            this.password = password;
            this.displayName = displayName;
            this.email = email;
            this.balance = balance;
            this.exp = exp;
            this.level = level;
            this.loginType = loginType;
        }

        public User(User u)
        {
            userId = u.userId;
            userName = u.userName;
            password = u.password;
            displayName = u.displayName;
            email = u.email;
            balance = u.balance;
            exp = u.exp;
            level = u.level;
            loginType = u.loginType;

        }

        public User()
        {
            userId = "";
            userName = "";
            password = "";
            displayName = "";
            email = "";
            balance = 0;
            exp = 0;
            level = "";
            loginType = 0;
        }
    }
}
