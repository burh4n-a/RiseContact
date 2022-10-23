using AutoMapper;
using MongoDB.Driver;
using Rise.MongoDb.Entity.Concreate;
using Rise.Report.DataAccess.Abstract;
using Rise.Shared.Abstract;
using Rise.Shared.Dtos;
using Rise.Shared.Enums;

namespace Rise.Report.DataAccess.Concreate;

public class ReportService : IReportService
{

    private readonly IMongoCollection<Person> _personCollection;
    private readonly IMongoCollection<MongoDb.Entity.Concreate.Report> _reportCollection;
    private readonly IMapper _mapper;
    public ReportService(IMongoDatabaseSettings databaseSettings, IMapper mapper)
    {
        _mapper = mapper;
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.DatabaseName);
        _personCollection = mongoDb.GetCollection<Person>(databaseSettings.PersonCollectionName);
        _reportCollection = mongoDb.GetCollection<MongoDb.Entity.Concreate.Report>(databaseSettings.ReportCollectionName);
    }

    public async Task CreateReport(string reportId)
    {
        var createReportDto = new CreateReportDto
        {
            ReportRequestId = reportId,
            ReportDate = DateTime.Now,
            ReportStatus = ReportStatus.Creating,
            ReportDetail = new List<ReportDetailDto>()

        };

        var mapReport = _mapper.Map<MongoDb.Entity.Concreate.Report>(createReportDto);
        await _reportCollection.InsertOneAsync(mapReport);

        var persons = await _personCollection.Find(x => true).ToListAsync();

        foreach (var person in persons)
        {
            var locationAwiable = person.Contacts.FirstOrDefault(x => x.ContactType == ContactType.Location);
            if (locationAwiable == null) //bölgesi yoksa rapora dahil etme
            {
                continue;
            }

            var locationName = locationAwiable.ContactData;

            var locationExists = mapReport.ReportDetail.FirstOrDefault(x => x.Location == locationName);
            var locationPhoneNumberCount = person.Contacts.Count(x => x.ContactType == ContactType.Phone);
            if (locationExists == null)
            {
                var pReport = new ReportDetail()
                {
                    Location = locationAwiable.ContactData,
                    MobilePhoneCount = locationPhoneNumberCount,
                    PersonCount = 1
                };
                mapReport.ReportDetail.Add(pReport);

            }
            else
            {
                locationExists.PersonCount++;
                locationExists.MobilePhoneCount += locationPhoneNumberCount;
            }

        }

        mapReport.ReportStatus = ReportStatus.Completed;
        await _reportCollection.ReplaceOneAsync(x => x.Id == mapReport.Id, mapReport);

    }

    public async Task CreateReportWithData(string reportId, List<PersonDto> persons)
    {
        var createReportDto = new CreateReportDto
        {
            ReportRequestId = reportId,
            ReportDate = DateTime.Now,
            ReportStatus = ReportStatus.Creating,
            ReportDetail = new List<ReportDetailDto>()

        };

        var mapReport = _mapper.Map<MongoDb.Entity.Concreate.Report>(createReportDto);
        await _reportCollection.InsertOneAsync(mapReport);


        foreach (var person in persons)
        {
            var locationAwiable = person.Contacts.FirstOrDefault(x => x.ContactType == ContactType.Location);
            if (locationAwiable == null) //bölgesi yoksa rapora dahil etme
            {
                continue;
            }

            var locationName = locationAwiable.ContactData;

            var locationExists = mapReport.ReportDetail.FirstOrDefault(x => x.Location == locationName);
            var locationPhoneNumberCount = person.Contacts.Count(x => x.ContactType == ContactType.Phone);
            if (locationExists == null)
            {
                var pReport = new ReportDetail()
                {
                    Location = locationAwiable.ContactData,
                    MobilePhoneCount = locationPhoneNumberCount,
                    PersonCount = 1
                };
                mapReport.ReportDetail.Add(pReport);

            }
            else
            {
                locationExists.PersonCount++;
                locationExists.MobilePhoneCount += locationPhoneNumberCount;
            }

        }

        mapReport.ReportStatus = ReportStatus.Completed;
        await _reportCollection.ReplaceOneAsync(x => x.Id == mapReport.Id, mapReport);

    }

    public async Task<ReportDto> GetReportStatus(string reportId)
    {
        var report = await _reportCollection.Find(x => x.ReportRequestId == reportId).FirstOrDefaultAsync();
        return _mapper.Map<ReportDto>(report);
    }

    public async Task<List<ReportListDto>> GetReportList()
    {
        var reports = await _reportCollection.Find(x => true).ToListAsync();

        return _mapper.Map<List<ReportListDto>>(reports);

    }

    public async Task<List<ReportDetailDto>> GetReportDetails(string reportId)
    {
        var report = await _reportCollection.Find(x => x.Id == reportId).FirstOrDefaultAsync();

        return _mapper.Map<List<ReportDetailDto>>(report.ReportDetail);
    }
}