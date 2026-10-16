using CineFlow.Catalog.Api.Features.Movies.Create;
using CineFlow.Catalog.Api.Features.Movies.Delete;
using CineFlow.Catalog.Api.Features.Movies.GetAll;
using CineFlow.Catalog.Api.Features.Movies.GetById;
using Microsoft.AspNetCore.Mvc;

namespace CineFlow.Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateMovieResponse>> Create(
        [FromBody] CreateMovieRequest request,
        [FromServices] CreateMovieUseCase useCase,
        CancellationToken ct)
    {
        var response = await useCase.ExecuteAsync(request, ct);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetMovieByIdResponse>> GetById(
        Guid id,
        [FromServices] GetMovieByIdUseCase useCase,
        CancellationToken ct)
    {
        var response = await useCase.ExecuteAsync(id, ct);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetAllMoviesResponse>> GetAll(
        [FromServices] GetAllMoviesUseCase useCase,
        CancellationToken ct)
    {
        var response = await useCase.ExecuteAsync(ct);

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        [FromServices] DeleteMovieUseCase useCase,
        CancellationToken ct
        )
    {
        await useCase.ExecuteAsync(id, ct);

        return NoContent();
    }
}
