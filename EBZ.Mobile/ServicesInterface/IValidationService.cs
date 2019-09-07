using System;
using System.Collections.Generic;
using System.Text;

namespace EBZ.Mobile.ServicesInterface
{
    public interface IValidationService
    {
        bool IsEmailValid(string email);
        bool IsPasswordValid(string password);
    }
}
