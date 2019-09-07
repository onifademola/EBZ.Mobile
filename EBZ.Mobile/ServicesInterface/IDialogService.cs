﻿using System.Threading.Tasks;

namespace EBZ.Mobile.ServicesInterface
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);
        void ShowDialog(string message);
        void ShowToast(string message);
        void HideDialog();
    }
}
