using Rise.Shared.Enums;

namespace Rise.Shared.Dtos;

public class CreateReportDto
{
    public string ReportRequestId { get; set; }
    public DateTime ReportDate { get; set; }
    public ReportStatus ReportStatus { get; set; }
    public List<ReportDetailDto> ReportDetail { get; set; }
}