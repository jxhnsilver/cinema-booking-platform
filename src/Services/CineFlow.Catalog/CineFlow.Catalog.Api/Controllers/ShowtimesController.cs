using CineFlow.Catalog.Api.Features.Showtimes.CreateSingle;
using CineFlow.Catalog.Api.Features.Showtimes.Cancel;
using CineFlow.Catalog.Api.Features.Showtimes.Update;
using Microsoft.AspNetCore.Mvc;

namespace CineFlow.Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ShowtimesController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateShowtimeResponse>> Create(
        [FromBody] CreateShowtimeRequest request,
        [FromServices] CreateShowtimeUseCase useCase,
        CancellationToken ct)
    {
        var response = await useCase.ExecuteAsync(request, ct);
        return Ok(response);
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
