namespace Infrastructure.Urls;

public interface IUriBuilderService {
    string CreateConfiguredUri(string path, string query);
    string CreateConfiguredUri(string path, Dictionary<string, string> queryParams);
    
}