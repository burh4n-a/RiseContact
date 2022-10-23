using Rise.Shared.Dtos;

namespace Rise.Report.DataAccess.Abstract;

public interface IReportService
{
    Task CreateReport(string reportId);
    Task CreateReportWithData(string reportId,List<PersonDto> persons);
    Task<ReportDto> GetReportStatus(string reportId);
    Task<List<ReportListDto>> GetReportList();
    Task<List<ReportDetailDto>> GetReportDetails(string reportId);
}