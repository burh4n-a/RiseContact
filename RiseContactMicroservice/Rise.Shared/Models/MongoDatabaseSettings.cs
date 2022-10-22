using Rise.Shared.Abstract;

namespace Rise.Shared.Models;

public class MongoDatabaseSettings : IMongoDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string PersonCollectionName { get; set; }
    public string ContactCollectionName { get; set; }
    public string ReportCollectionName { get; set; }
}