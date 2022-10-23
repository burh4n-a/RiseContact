using Rise.Shared.Abstract;
using Rise.Shared.Enums;

namespace Rise.Shared.Dtos;

public class ReportListDto : EntityBaseDto
{
    public string ReportRequestId { get; set; }
    public DateTime ReportDate { get; set; }
    public ReportStatus ReportStatus { get; set; }
    public string ReportStatusString
    {
        get { return this.ReportStatus.ToString(); }
    }
}