using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MemberShip
{
    public class DefaultReadRole : ReadRole
    {
        public override List<bool> readRole(string username, string password)
        {
            string pathJsonFile = "users.json";
            string json = "";
            try
            {
                json = System.IO.File.ReadAllText(pathJsonFile);
            } catch(FileNotFoundException e)
            {
                json = "[]";
            }
            JArray data = JArray.Parse(json);
            foreach (dynamic user in data)
            {
                string curUsername = user.username;
                string curPassword = user.password;
                if(username == curUsername && password == curPassword)
                {
                    List<bool> roles = new List<bool> { };
                    foreach (dynamic role in user.roles)
                    {
                        roles.Add((bool)role);
                    }

                    return roles;
                }
            }
            return new List<bool> { };
        }
    }
}