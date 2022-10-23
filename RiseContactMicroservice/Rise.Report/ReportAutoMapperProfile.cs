using AutoMapper;
using Rise.MongoDb.Entity.Concreate;
using Rise.Shared.Dtos;

namespace Rise.Report;

public class ReportAutoMapperProfile : Profile
{
    public ReportAutoMapperProfile()
    {
        CreateMap<MongoDb.Entity.Concreate.Report, ReportDto>().ReverseMap();
        CreateMap<MongoDb.Entity.Concreate.Report, CreateReportDto>().ReverseMap();
        CreateMap<ReportDetail, ReportDetailDto>().ReverseMap();
        CreateMap<ReportDto, CustomReportResultDto>();
        CreateMap<MongoDb.Entity.Concreate.Report, ReportListDto>();
    }
}