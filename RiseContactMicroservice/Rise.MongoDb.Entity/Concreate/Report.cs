using Rise.MongoDb.Entity.Abstract;
using Rise.Shared.Enums;

namespace Rise.MongoDb.Entity.Concreate;

public class Report : MongoEntityBase
{
    public string ReportRequestId { get; set; }
    public DateTime ReportDate { get; set; }
    public ReportStatus ReportStatus { get; set; }
    public IList<ReportDetail> ReportDetail { get; set; }

}