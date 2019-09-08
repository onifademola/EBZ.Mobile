using EBZ.Mobile.Models;
using EBZ.Mobile.ServicesInterface;
using System.Threading.Tasks;

namespace EBZ.Mobile.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        //private readonly IGenericService _genericRepository;
        //private readonly ISettingsService _settingsService;
        //public AuthenticationService(IGenericService genericRepository, ISettingsService settingsService)
        //{
        //    _settingsService = settingsService;
        //    _genericRepository = genericRepository;

        //}

        GenericService _genericRepository = new GenericService();
        SettingsService _settingsService = new SettingsService();

        public AuthenticationService()
        {

        }

        public async Task<AuthenticationResponse> Register(string email, string password, string phone, string birthDay, string birthMonth, int categoryId)
        {
            string uri = Constants.BaseApiUrl + Constants.RegisterEndpoint + email + "/" + password + "/" + phone + "/" + birthDay + "/" + birthMonth + "/" + categoryId;
            var response = await _genericRepository.DoAuthAsync<AuthenticationResponse>(uri.ToString());
            return response;
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_settingsService.UserIdSetting);
        }

        public async Task<AuthenticationResponse> Authenticate(string userName, string password)
        {
            string uri = Constants.BaseApiUrl + Constants.LoginEndpoint + userName + "/" + password;
            var response = await _genericRepository.DoAuthAsync<AuthenticationResponse>(uri.ToString());
            return response;
        }

        public async Task ResetPassword(string email)
        {
            string uri = Constants.BaseApiUrl + Constants.ResetPasswordEndpoint + email;
            await _genericRepository.DoAuthAsync(uri.ToString());
        }
    }
}
