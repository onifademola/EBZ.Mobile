using System.Threading.Tasks;

namespace EBZ.Mobile.ServicesInterface
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);
        void ShowLoading(string message);
        void ShowToast(string message);
        void HideLoading();
    }
}
