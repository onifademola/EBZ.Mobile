
namespace EBZ.Mobile.ServicesInterface
{
    public interface IUserService
    {
        bool IsAuthenticated();
        bool IsTokenExpired();
        string LoggedInUser();
        string LoggedInUserToken();
        string LoggedInUserRole();
    }
}
