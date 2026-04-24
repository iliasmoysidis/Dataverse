using Backend.Modules.Absences.Api.Requests;
using Backend.Modules.Absences.Application.UseCases.Approve;
using Backend.Modules.Absences.Application.UseCases.Create;
using Backend.Modules.Absences.Application.UseCases.GetByUser;
using Backend.Modules.Absences.Application.UseCases.GetPending;
using Backend.Modules.Absences.Application.UseCases.GetPendingByUser;
using Backend.Modules.Absences.Application.UseCases.Reject;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.Absences.Api;

[ApiController]
[Route("api/absences")]
public sealed class AbsencesController : ControllerBase
{
    private readonly CreateAbsenceHandler _create;
    private readonly ApproveAbsenceHandler _approve;
    private readonly RejectAbsenceHandler _reject;
    private readonly GetPendingAbsencesHandler _getPending;
    private readonly GetPendingAbsencesByUserHandler _getPendingByUser;
    private readonly GetAbsencesByUserHandler _getByUser;

    public AbsencesController(
        CreateAbsenceHandler create,
        ApproveAbsenceHandler approve,
        RejectAbsenceHandler reject,
        GetPendingAbsencesHandler getPending,
        GetPendingAbsencesByUserHandler getPendingByUser,
        GetAbsencesByUserHandler getByUser
    )
    {
        _create = create;
        _approve = approve;
        _reject = reject;
        _getPending = getPending;
        _getPendingByUser = getPendingByUser;
        _getByUser = getByUser;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateAbsenceRequest request,
        CancellationToken ct
    )
    {
        var command = new CreateAbsenceCommand(
            request.UserId,
            request.StartDate,
            request.EndDate
        );

        var result = await _create.Handle(command, ct);

        return Created($"/api/absences/{result.Id}", result);
    }

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

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending(
        CancellationToken ct)
    {
        var result = await _getPending.Handle(
            new GetPendingAbsencesQuery(), ct);

        return Ok(result);
    }

    [HttpGet("users/{userId:int}/pending")]
    public async Task<IActionResult> GetPendingByUser(
        int userId,
        CancellationToken ct)
    {
        var result = await _getPendingByUser.Handle(
            new GetPendingAbsencesByUserQuery(userId), ct);

        return Ok(result);
    }

    [HttpGet("users/{userId:int}")]
    public async Task<IActionResult> GetByUser(
        int userId,
        CancellationToken ct)
    {
        var result = await _getByUser.Handle(
            new GetAbsencesByUserQuery(userId), ct);

        return Ok(result);
    }
}
