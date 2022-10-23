namespace Rise.Report.Rest;

public interface IJsonHttpClient
{
    Dictionary<string, string> Headers { get; set; }

    Task<T> DeleteAsync<T>(string url, object parametersModel = null, object body = null);
    Task<T> GetAsync<T>(string url, object parametersModel = null);
    Task<T> PatchAsync<T>(string url, object body);
    Task<T> PostAsync<T>(string url, object body);
    Task<T> PutAsync<T>(string url, object body);
}