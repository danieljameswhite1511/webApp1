using System.Web;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Urls;

public class UriBuilderService : IUriBuilderService
{
    private readonly IConfiguration _configuration;
    private readonly string? _host;
    private readonly string? _scheme;
    private readonly string? _port;
    
    public UriBuilderService(IConfiguration configuration) {
        _configuration = configuration;
        _host = _configuration["AppSettings:Host"];
        _scheme = _configuration["AppSettings:Scheme"];
        _port = _configuration["AppSettings:Port"];
    }
    public string CreateConfiguredUri(string path, string query)
    {
        var uriBuilder = new UriBuilder {
            Host = _host,
            Scheme = _scheme,
            Path = path,
            Query = query,
        };

        if (_port != null) {
            uriBuilder.Port = int.Parse(_port);
        }
        
        return uriBuilder.ToString();
    }

    public string CreateConfiguredUri(string path, Dictionary<string, string> queryParams)
    {
        var uriBuilder = new UriBuilder {
            Host = _host,
            Scheme = _scheme,
            Path = path,
        };

        if (_port != null && int.TryParse(_port, out int port)) {
            uriBuilder.Port = port;
        }

        var i = 0;
        foreach (var keyValuePair in queryParams)
        {
            var encodedValue = Uri.EscapeDataString(keyValuePair.Value);
            if (i == 0) {
                uriBuilder.Query = $"?{keyValuePair.Key}={encodedValue}";
            }
            else {
                uriBuilder.Query += $"&{keyValuePair.Key}={encodedValue}";    
            }
            i++;
        }
        
        
        return uriBuilder.ToString();
    }
}