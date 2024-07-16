using G_APIs.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace G_APIs.Common;

public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);

        return value == null ? default(T) :
            JsonConvert.DeserializeObject<T>(value);
    }
}
public class ExternalDataService
{
    private readonly HttpClient _httpClient;

    public ExternalDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Login>> GetExternalDataAsync()
    {
        var response = await _httpClient.GetAsync("https://api.example.com/data");
        response.EnsureSuccessStatusCode();
        using var responseStream = await response.Content.ReadAsStreamAsync();
        return await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<Login>>(responseStream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true  
        });
    }
}

public  static class ExternalApi
{
    public async static Task<T> Call<T>()
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://api.example.com/");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "your_access_token");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = new { Name = "John Doe", Age = 30 };
            string requestUri = "endpoint"; // Relative URI

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, data);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (HttpRequestException e)
            {
                return JsonConvert.DeserializeObject<T>(e.Message);
            }
        }
    }
}