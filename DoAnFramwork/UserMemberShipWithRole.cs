using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemberShip
{
    internal class UserMemberShipWithRole
    {
        private MemberShip member;
        private Role role;
        private bool isValidUser;

        public UserMemberShipWithRole(string username, string password, ReadRole readRole)
        {
            this.member = new MemberShip(username, password);
            List<bool> temp = readRole.readRole(username, password);
            this.isValidUser = temp.Count() != 0;
            this.role = new Role(temp);
        }

        public bool validUser()
        {
            return this.isValidUser;
        }

        public string getUsername()
        {
            return member.Username;
        }

        public string getPassword()
        {
            return member.Password;
        }

        public bool allowRead()
        {
            return role.allowRead();
        }
        public bool allowCreate()
        {
            return role.allowCreate();
        }
        public bool allowUpdate()
        {
            return role.allowUpdate();
        }
        public bool allowDelete()
        {
            return role.allowDelete();
        }
    }
}