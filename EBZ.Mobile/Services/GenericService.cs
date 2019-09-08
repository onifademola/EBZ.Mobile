using Acr.UserDialogs;
using EBZ.Mobile.Exceptions;
using EBZ.Mobile.Models;
using EBZ.Mobile.ServicesInterface;
using Newtonsoft.Json;
using Polly;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EBZ.Mobile.Services
{
    public class GenericService : IGenericService
    {
        //private readonly SettingsService _settingsService;
        //private readonly ConnectionService _connectionService;

        //public GenericService(SettingsService settingsService, ConnectionService connectionService)
        //{
        //    _settingsService = settingsService;
        //    _connectionService = connectionService;
        //}


        SettingsService _settingsService = new SettingsService();
        ConnectionService _connectionService = new ConnectionService();
        public GenericService()
        {
        }

        public async Task DoAuthAsync(string uri)
        {
            if (_connectionService.IsConnected)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    string jsonResult = string.Empty;

                    var responseMessage = await Policy
                        .Handle<WebException>(ex =>
                        {
                            Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                            return true;
                        })
                        .WaitAndRetryAsync
                        (
                            5,
                            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                        )
                        .ExecuteAsync(async () => await httpClient.GetAsync(uri));
                    //jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    //var json = JsonConvert.DeserializeObject<AuthenticationResponse>(jsonResult);
                    //return json;
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                    throw;
                }
            }
            else
            {
                //UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync(
                    "Please check your network service",
                    "Service Unavailable",
                    "OK");
            }            
        }

        public async Task<AuthenticationResponse> DoAuthAsync<T>(string uri)
        {
            if (_connectionService.IsConnected)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    string jsonResult = string.Empty;

                    var responseMessage = await Policy
                        .Handle<WebException>(ex =>
                        {
                            Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                            return true;
                        })
                        .WaitAndRetryAsync
                        (
                            5,
                            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                        )
                        .ExecuteAsync(async () => await httpClient.GetAsync(uri));
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<AuthenticationResponse>(jsonResult);
                    return json;
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                    throw;
                    //var jsonR = JsonConvert.DeserializeObject<AuthenticationResponse>(string.Empty);
                    //return jsonR;
                }
            }
            else
            {
                var jsonR = JsonConvert.DeserializeObject<AuthenticationResponse>(string.Empty);
                
                //UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync(
                    "Please check your network service",
                    "Service Unavailable",
                    "OK");
                return jsonR;
            }
            
        }

        public async Task<T> GetBasicAsync<T>(string uri)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.GetAsync(uri));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }
                var jsonR = JsonConvert.DeserializeObject<T>(null);
                return jsonR;
            }
            catch (Exception e)
            {
                //Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                //throw;
                var jsonR = JsonConvert.DeserializeObject<T>(null);
                return jsonR;
            }
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            if (_connectionService.IsConnected)
            {
                try
                {
                    string token = _settingsService.TokenSetting;
                    HttpClient httpClient = new HttpClient();
                    string jsonResult = string.Empty;
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    var responseMessage = await Policy
                        .Handle<WebException>(ex =>
                        {
                            Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                            return true;
                        })
                        .WaitAndRetryAsync
                        (
                            5,
                            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                        )
                        .ExecuteAsync(async () => await httpClient.GetAsync(uri));

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var json = JsonConvert.DeserializeObject<T>(jsonResult);
                        return json;
                    }
                    var jsonR = JsonConvert.DeserializeObject<T>(string.Empty);
                    return jsonR;
                }
                catch (Exception e)
                {
                    //Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                    //throw;
                    var jsonR = JsonConvert.DeserializeObject<T>(string.Empty);
                    return jsonR;
                }
            }
            else
            {
                //UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync(
                    "Please check your network service",
                    "Service Unavailable",
                    "OK");
                var jsonR = JsonConvert.DeserializeObject<T>(string.Empty);
                return jsonR;
            }            
        }

        public async Task<T> PostAsync<T>(string uri, T data)
        {
            try
            {
                string token = _settingsService.TokenSetting;
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }
                var jsonR = JsonConvert.DeserializeObject<T>(null);
                return jsonR;
            }
            catch (Exception e)
            {
                //Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                //throw;
                var jsonR = JsonConvert.DeserializeObject<T>(null);
                return jsonR;
            }
        }

        //public async Task<TR> PostAsync<T, TR>(string uri, T data)
        //{
        //    try
        //    {
        //        string token = _settingsService.TokenSetting;
        //        HttpClient httpClient = CreateHttpClient(uri);
        //        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        //        var content = new StringContent(JsonConvert.SerializeObject(data));
        //        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //        string jsonResult = string.Empty;

        //        var responseMessage = await Policy
        //            .Handle<WebException>(ex =>
        //            {
        //                Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
        //                return true;
        //            })
        //            .WaitAndRetryAsync
        //            (
        //                5,
        //                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
        //            )
        //            .ExecuteAsync(async () => await httpClient.PostAsync(uri, content));

        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            var json = JsonConvert.DeserializeObject<TR>(jsonResult);
        //            return json;
        //        }

        //        if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
        //            responseMessage.StatusCode == HttpStatusCode.Unauthorized)
        //        {
        //            throw new ServiceAuthenticationException(jsonResult);
        //        }

        //        throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
        //        throw;
        //    }
        //}
        
        public async Task<T> PutAsync<T>(string uri, T data)
        {
            try
            {
                string token = _settingsService.TokenSetting;
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient(authToken);
            await httpClient.DeleteAsync(uri);
        }

        private HttpClient CreateHttpClient(string authToken)
        {
            string token = _settingsService.TokenSetting;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return httpClient;
        }
    }
}
