using CineFlow.Catalog.Api.Features.Halls.Create;
using CineFlow.Catalog.Api.Features.Halls.GetAll;
using CineFlow.Catalog.Api.Features.Halls.GetById;
using Microsoft.AspNetCore.Mvc;

namespace CineFlow.Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HallsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateHallResponse>> Create(
        [FromBody] CreateHallRequest request,
        [FromServices] CreateHallUseCase useCase,
        CancellationToken ct)
    {
        var response = await useCase.ExecuteAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet]
    public async Task<ActionResult<GetAllHallsResponse>> GetAll(
        [FromServices] GetAllHallsUseCase useCase,
        CancellationToken ct)
    {
        var response = await useCase.ExecuteAsync(ct);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetHallByIdResponse>> GetById(
        Guid id,
        [FromServices] GetHallByIdUseCase useCase,
        CancellationToken ct)
    {
        var response = await useCase.ExecuteAsync(id, ct);

        return Ok(response);
    }
}
