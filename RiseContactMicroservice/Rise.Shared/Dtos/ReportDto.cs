using Rise.Shared.Abstract;
using Rise.Shared.Enums;

namespace Rise.Shared.Dtos;

public class ReportDto : EntityBaseDto
{
    public string ReportRequestId { get; set; }
    public DateTime ReportDate { get; set; }
    public ReportStatus ReportStatus { get; set; }
    public IList<ReportDetailDto> ReportDetail { get; set; }
}