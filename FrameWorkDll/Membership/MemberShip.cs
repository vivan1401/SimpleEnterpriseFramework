using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemberShip
{
    public class MemberShip
    {
        public MemberShip(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        protected string username;
        protected string password;

        public string Username { get => username; }
        public string Password { get => password; }
    }
}