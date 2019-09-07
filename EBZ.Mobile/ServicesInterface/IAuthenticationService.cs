using EBZ.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBZ.Mobile.ServicesInterface
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Register(string email, string password, string phone, string birthDay, string birthMonth, int categoryId);
        bool IsUserAuthenticated();
        Task<AuthenticationResponse> Authenticate(string userName, string password);
        Task ResetPassword(string email);
    }
}
