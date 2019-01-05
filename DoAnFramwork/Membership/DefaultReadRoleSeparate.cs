using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MemberShip
{
    public class DefaultReadRoleSeparate : ReadRole
    {
        public override List<bool> readRole(string username, string password)
        {
            string pathJsonUserFile = "users.json";
            string pathJsonRoleFile = "roles.json";
            string jsonUser = "";
            string jsonRole = "";
            try
            {
                jsonUser = System.IO.File.ReadAllText(pathJsonUserFile);
            }
            catch (FileNotFoundException e)
            {
                jsonUser = "[]";
            }

            try
            {
                jsonRole = System.IO.File.ReadAllText(pathJsonRoleFile);
            }
            catch (FileNotFoundException e)
            {
                jsonRole = "[]";
            }

            JArray dataUser = JArray.Parse(jsonUser);
            JArray dataRole = JArray.Parse(jsonRole);

            foreach (dynamic user in dataUser)
            {
                string curUsername = user.username;
                string curPassword = user.password;
                if (username == curUsername && password == curPassword)
                {
                    List<bool> roles = new List<bool> { };
                    foreach (dynamic role in dataRole)
                    {
                        // Trả về role
                        if(role.name == user.role)
                        {
                            foreach (dynamic temp in role.roles)
                            {
                                roles.Add((bool)temp);
                            }
                            return roles;
                        }
                    }
                    // k tìm thấy role, trả ra role mặc định
                    return new List<bool> { false, false, false, false };
                }
            }
            return new List<bool> { };
        }
    }
}