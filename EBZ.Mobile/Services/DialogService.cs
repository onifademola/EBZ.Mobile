using Acr.UserDialogs;
using System.Threading.Tasks;

namespace EBZ.Mobile.Services
{
    public class DialogService
    {
        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public void ShowToast(string message)
        {
            UserDialogs.Instance.Toast(message, System.TimeSpan.FromSeconds(4.0));
        }

        public void ShowDialog(string message)
        {
            UserDialogs.Instance.ShowLoading(message, MaskType.Gradient);
        }

        public void HideDialog()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}
