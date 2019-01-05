using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemberShip
{
    public class Role
    {
        protected List<bool> listRole;

        public Role(List<bool> role)
        {
            this.listRole = role;
        }

        public bool allowRead()
        {
            if(listRole.Count() == 0)
            {
                return false;
            }
            return listRole[0];
        }
        public bool allowCreate()
        {
            if (listRole.Count() == 0)
            {
                return false;
            }
            return listRole[1];
        }
        public bool allowUpdate()
        {
            if (listRole.Count() == 0)
            {
                return false;
            }
            return listRole[2];
        }
        public bool allowDelete()
        {
            if (listRole.Count() == 0)
            {
                return false;
            }
            return listRole[3];
        }
    }
}