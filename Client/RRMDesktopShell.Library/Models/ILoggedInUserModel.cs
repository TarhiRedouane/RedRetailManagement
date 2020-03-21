using System;

namespace RRMDesktopShell.Library.Models
{
    public interface ILoggedInUserModel
    {
        string Token { get; set; }
        string Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string EmailAdress { get; set; }
        DateTime CreatedDate { get; set; }
    }
}