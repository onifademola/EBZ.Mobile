using EBZ.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBZ.Mobile.ServicesInterface
{
    public interface IGenericService
    {
        Task DoAuthAsync(string uri);
        Task<AuthenticationResponse> DoAuthAsync<T>(string uri);
        Task<T> GetBasicAsync<T>(string uri);
        Task<T> GetAsync<T>(string uri);
        Task<T> PostAsync<T>(string uri, T data);
        Task<T> PutAsync<T>(string uri, T data);
        Task DeleteAsync(string uri, string authToken = "");
    }
}
