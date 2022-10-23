using System.Collections;
using System.Text;
using Newtonsoft.Json;

namespace Rise.Report.Rest;

public class JsonHttpClient : IJsonHttpClient
{
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

    public async Task<T> PostAsync<T>(string url, object body)
    {
        var json = JsonConvert.SerializeObject(body);
        using (var client = GetClient(Headers))
        using (var message = new StringContent(json, Encoding.UTF8, "application/json"))
        using (var response = await client.PostAsync(url, message))
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
    public async Task<T> GetAsync<T>(string url, object parametersModel = null)
    {
        if (parametersModel != null)
        {
            url += "?";
            url += String.Join("&", ExtractParameters());
            IEnumerable ExtractParameters()
            {
                foreach (var property in parametersModel.GetType().GetProperties())
                    yield return $"{property.Name}={property.GetValue(parametersModel)}";
            }
        }

        using (var client = GetClient(Headers))
        using (var response = await client.GetAsync(url))
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
    public async Task<T> DeleteAsync<T>(string url, object parametersModel = null, object body = null)
    {
        if (parametersModel != null)
        {
            url += "?";
            url += String.Join("&", ExtractParameters());
            IEnumerable ExtractParameters()
            {
                foreach (var property in parametersModel.GetType().GetProperties())
                    yield return $"{property.Name}={property.GetValue(parametersModel)}";
            }
        }

        using (var client = GetClient(Headers))
        using (var message = new HttpRequestMessage { RequestUri = new Uri(url), Method = HttpMethod.Delete, Content = body != null ? new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json") : null })
        using (var response = await client.SendAsync(message))
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
    public async Task<T> PutAsync<T>(string url, object body)
    {
        using (var client = GetClient(Headers))
        using (var stringcontent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"))
        using (var response = await client.PutAsync(url, stringcontent))
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
    public async Task<T> PatchAsync<T>(string url, object body)
    {
        using (var client = GetClient(Headers))
        using (var stringContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"))
        using (var response = await client.PatchAsync(url, stringContent))
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }

    public static HttpClient GetClient(Dictionary<string, string> headers = default)
    {
        HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromMinutes(1);
        if (headers != null)
            foreach (var item in headers)
                client.DefaultRequestHeaders.Add(item.Key, item.Value);

        return client;
    }
}