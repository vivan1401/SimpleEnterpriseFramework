using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemberShip
{
    abstract public class ReadRole
    {
        abstract public List<bool> readRole(string username, string password);
    }
}