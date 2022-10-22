using Rise.Shared.Abstract;

namespace Rise.Shared.Dtos;

public class PersonWithoutDetailDto: EntityBaseDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Company { get; set; }
}