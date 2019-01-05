using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemberShip
{
    abstract internal class ReadRole
    {
        abstract public List<bool> readRole(string username, string password);
    }
}