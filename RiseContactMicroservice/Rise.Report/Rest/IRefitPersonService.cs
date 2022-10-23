using Microsoft.AspNetCore.Mvc;
using Refit;
using Rise.Shared.Dtos;
using System.Net;

namespace Rise.Report.Rest;

public interface IRefitPersonService
{
    [ProducesResponseType(typeof(IEnumerable<PersonDto>), (int)HttpStatusCode.OK)]
    [Get("/PersonContact/GetAllPersonsWithDetail/GetAllPersonsWithDetail")]
    Task<List<PersonDto>> GetAllPersonsWithDetail();
}