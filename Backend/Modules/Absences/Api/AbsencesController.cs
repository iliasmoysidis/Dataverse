using System.Security.Claims;
using Backend.Modules.Absences.Api.Requests.Create;
using Backend.Modules.Absences.Application.UseCases.Approve;
using Backend.Modules.Absences.Application.UseCases.Create;
using Backend.Modules.Absences.Application.UseCases.GetByUser;
using Backend.Modules.Absences.Application.UseCases.GetPending;
using Backend.Modules.Absences.Application.UseCases.Reject;
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
    private readonly GetAbsencesHandler _getAbsences;
    private readonly GetAbsencesByUserHandler _getByUser;

    public AbsencesController(
        CreateAbsenceHandler create,
        ApproveAbsenceHandler approve,
        RejectAbsenceHandler reject,
        GetAbsencesHandler getAbsences,
        GetAbsencesByUserHandler getByUser
    )
    {
        _create = create;
        _approve = approve;
        _reject = reject;
        _getAbsences = getAbsences;
        _getByUser = getByUser;
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

    [Authorize(Roles = "Manager")]
    [HttpGet]
    public async Task<IActionResult> GetAbsences(
        CancellationToken ct)
    {
        var result = await _getAbsences.Handle(
            new GetAbsencesQuery(), ct);

        return Ok(result);
    }

    [HttpGet("users/{userId:int}")]
    public async Task<IActionResult> GetByUser(
        int userId,
        CancellationToken ct)
    {
        if (!CanAccessUser(userId))
            return Forbid();

        var result = await _getByUser.Handle(
            new GetAbsencesByUserQuery(userId), ct);

        return Ok(result);
    }

    private bool CanAccessUser(int userId)
    {
        var currentUserId =
            int.Parse(User.FindFirstValue(
                ClaimTypes.NameIdentifier)!);

        return User.IsInRole("Manager")
            || currentUserId == userId;
    }
}
