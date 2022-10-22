using Rise.Report.DataAccess.Abstract;
using Rise.Shared.Enums;

namespace Rise.Report.Entities;

public class Report : MongoEntityBase
{
    public DateTime ReportDate { get; set; }
    public ReportStatus ReportStatus { get; set; }
    public ReportDetail ReportDetail { get; set; }

}