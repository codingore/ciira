using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciira.Models
{
    public enum UserKind
    {
        Developer = 0,
        TeamLeader = 1,
        ProjectManager = 2,
        Customer = 3,
        Administrator = 4
    }
}