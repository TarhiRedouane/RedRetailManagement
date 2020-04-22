using System.Collections.Generic;
using System.Linq;

namespace RRMDesktopShell.Library.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Roles { get; set; }
        public string RolesList => Roles.Select(pair => pair.Value)
                                        .Aggregate((s, s1) => $"{s},{s1}");
    }
}
