namespace Rise.Shared.Dtos;

public class CreateReportRequestDto
{
    public string ReportRequestId { get; set; }
    public List<PersonDto> Persons { get; set; }
}