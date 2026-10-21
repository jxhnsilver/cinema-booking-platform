using CineFlow.Catalog.Api.Entities.Showtimes;
using CineFlow.Catalog.Api.Features.Showtimes.Cancel;
using CineFlow.Catalog.Api.Features.Showtimes.Create;
using CineFlow.Catalog.Api.Features.Showtimes.GetAll;
using CineFlow.Catalog.Api.Features.Showtimes.GetById;
using CineFlow.Catalog.Api.Features.Showtimes.Update;
using Microsoft.AspNetCore.Mvc;

namespace CineFlow.Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ShowtimesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GetAllShowtimesResponse>> GetAll(
        [FromQuery] DateTime? fromDate,
        [FromQuery] ShowtimeStatus? status,
        [FromServices] GetAllShowtimesUseCase useCase,
        CancellationToken ct)
    {
        return Ok(await useCase.ExecuteAsync(fromDate, status, ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetShowtimeByIdResponse>> GetById(
        Guid id,
        [FromServices] GetShowtimeByIdUseCase useCase,
        CancellationToken ct)
    {
        return Ok(await useCase.ExecuteAsync(id, ct));
    }

    [HttpPost]
    public async Task<ActionResult<CreateShowtimeResponse>> Create(
        [FromBody] CreateShowtimeRequest request,
        [FromServices] CreateShowtimeUseCase useCase,
        CancellationToken ct)
    {
        var response = await useCase.ExecuteAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdateShowtimeResponse>> Update(
        Guid id,
        [FromBody] UpdateShowtimeRequest request,
        [FromServices] UpdateShowtimeUseCase useCase,
        CancellationToken ct)
    {
        return Ok(await useCase.ExecuteAsync(id, request, ct));
    }

    [HttpPatch("{id:guid}/cancel")]
    public async Task<ActionResult<CancelShowtimeResponse>> Cancel(
        Guid id,
        [FromServices] CancelShowtimeUseCase useCase,
        CancellationToken ct)
    {
        return Ok(await useCase.ExecuteAsync(id, ct));
    }
}
