using System.Security.Claims;
using Backend.Modules.Absences.Api.Requests.Create;
using Backend.Modules.Absences.Application.UseCases.Approve;
using Backend.Modules.Absences.Application.UseCases.Cancel;
using Backend.Modules.Absences.Application.UseCases.Create;
using Backend.Modules.Absences.Application.UseCases.Reject;
using Backend.Modules.Absences.Application.UseCases.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.Absences.Api;

[Authorize]
[ApiController]
[Route("api/absences")]
public sealed class AbsencesController : ControllerBase
{
    private readonly CreateAbsenceHandler _create;
    private readonly ApproveAbsenceHandler _approve;
    private readonly RejectAbsenceHandler _reject;
    private readonly CancelAbsenceHandler _cancel;
    private readonly SearchAbsencesHandler _search;

    public AbsencesController(
        CreateAbsenceHandler create,
        ApproveAbsenceHandler approve,
        RejectAbsenceHandler reject,
        CancelAbsenceHandler cancel,
        SearchAbsencesHandler search
    )
    {
        _create = create;
        _approve = approve;
        _reject = reject;
        _cancel = cancel;
        _search = search;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
            [FromBody] CreateAbsenceRequest request,
            CancellationToken ct
    )
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var command = new CreateAbsenceCommand(
            userId,
            request.StartDate,
            request.EndDate
        );

        var result = await _create.Handle(command, ct);

        return Created($"/api/absences/{result.Id}", result);
    }

    [Authorize(Roles = "Manager")]
    [HttpPatch("{id:int}/approve")]
    public async Task<IActionResult> Approve(
            int id,
            CancellationToken ct
        )
    {
        var command = new ApproveAbsenceCommand(id);
        var result = await _approve.Handle(command, ct);

        return Ok(result);
    }

    [Authorize(Roles = "Manager")]
    [HttpPatch("{id:int}/reject")]
    public async Task<IActionResult> Reject(
        int id,
        CancellationToken ct
    )
    {
        var command = new RejectAbsenceCommand(id);
        var result = await _reject.Handle(command, ct);

        return Ok(result);
    }

    [Authorize]
    [HttpPatch("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(
        int id,
        CancellationToken ct
    )
    {
        var currentUserId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var command = new CancelAbsenceCommand(id, currentUserId);
        var result = await _cancel.Handle(command, ct);

        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Search(
    [FromQuery] SearchAbsencesQuery query,
    CancellationToken ct)
    {
        var currentUserId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var isManager = User.IsInRole("Manager");

        if (!isManager)
        {
            query = query with
            {
                UserId = currentUserId
            };
        }

        var result = await _search.Handle(query, ct);

        return Ok(result);
    }
}
