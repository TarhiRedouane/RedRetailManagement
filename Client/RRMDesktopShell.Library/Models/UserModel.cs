using System.Collections.Generic;
using System.Linq;
using RRMDesktopShell.Library.Base;

namespace RRMDesktopShell.Library.Models
{
    public class UserModel : PropertyChangedRedBase
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public Dictionary<string, string> Roles { get; set; }

        public string RolesList => Roles.Select(pair => pair.Value)
            .Aggregate((s, s1) => $"{s},{s1}");
    }
}
