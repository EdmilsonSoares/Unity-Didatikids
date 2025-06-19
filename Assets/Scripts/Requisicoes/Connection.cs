using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor.ShaderGraph.Serialization;

public class Connection
{
    protected readonly HttpClient _httpClient;
    //protected readonly string _baseUrl = "http://127.0.0.1:8000";
    protected readonly string _baseUrl = "https://didatikidsapi.onrender.com";
    public Connection()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_baseUrl)
        };
    }

    protected async Task<HttpResponseMessage> Post(string endpoint, string json)
    {
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            string errorMessage = await response.Content.ReadAsStringAsync();
            UnityEngine.Debug.LogError("Mensagem da API: " + errorMessage);
            throw new HttpRequestException($"Erro: {(int)response.StatusCode} - {response.ReasonPhrase}");
        }

        return response;
    }

    protected async Task<HttpResponseMessage> Get(string endpoint, string accessToken, string refreshToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync(endpoint);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var newAccessToken = await RefreshAccessToken(refreshToken);
            if (newAccessToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                response = await _httpClient.GetAsync(endpoint);
            }
            else
            {
                throw new Exception("Não foi possível renovar o token.");
            }
        }

        if (!response.IsSuccessStatusCode)
        {
            string errorMessage = await response.Content.ReadAsStringAsync();
            UnityEngine.Debug.LogError($"Mensagem da API: {errorMessage}");
            throw new HttpRequestException($"Erro: {(int)response.StatusCode} - {response.ReasonPhrase}");
        }

        response.EnsureSuccessStatusCode();
        return response;
    }

    private class RefreshRequest
    {
        public string refresh { get; set; }
    }
    private class RefreshResponse
    {
        public string access { get; set; }
    }
    internal async Task<string> RefreshAccessToken(string refreshToken)
    {
        var request = new RefreshRequest { refresh = refreshToken };
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}/refresh/", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            UnityEngine.Debug.LogError("Erro ao renovar token: " + responseBody);
            UnityEngine.Debug.LogError("Tente fazer login novamente.");
            throw new HttpRequestException($"Erro {response.StatusCode}");
        }

        var tokenObj = JsonConvert.DeserializeObject<RefreshResponse>(responseBody);
        return tokenObj.access!;
    }
}

