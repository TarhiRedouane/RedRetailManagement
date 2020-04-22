using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RRMDataManager.Models
{
    public class ApplicationUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Dictionary<string,string> Roles { get; set; }
    }
}