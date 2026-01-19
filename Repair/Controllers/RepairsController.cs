using Microsoft.AspNetCore.Mvc;
using Repair.Application.Interfaces;
using Repair.Contracts.Repairs;
using Repair.Contracts.Repairs.CreateRepairRequest;
using Repair.Contracts.Repairs.RepairDetails;
using Repair.Contracts.Repairs.UpdateRepairStatus;

namespace Repair.Controllers;

[ApiController]
[Route("api/repairs")]
public class RepairsController : ControllerBase
{
    private readonly IRepairService _repairService;

    public RepairsController(IRepairService repairService)
    {
        _repairService = repairService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<RepairRequestListItemDto>>> GetAll(
    CancellationToken cancellationToken)
    {
        var list = await _repairService.GetAllRepairRequestsAsync(cancellationToken);
        return Ok(list);
    }


    [HttpPost]
    public async Task<ActionResult<CreateRepairRequestResponse>> Create(
        CreateRepairRequestRequest request,
        CancellationToken cancellationToken)
    {
        var id = await _repairService.CreatePhoneRepairAsync(
            request.DeviceModel,
            request.IMEI,
            request.ClientContact,
            request.Country,
            request.IssueType,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new CreateRepairRequestResponse { RepairRequestId = id });
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        UpdateRepairStatusRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _repairService.UpdateRepairStatusAsync(
            id,
            request.NewStatus,
            request.Notes,
            cancellationToken);

            return Ok();
        } 
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RepairDetailsResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var repair = await _repairService.GetRepairRequestAsync(id, cancellationToken);

            if (repair is null)
                return NotFound();

            var response = new RepairDetailsResponse
            {
                RepairRequestId = repair.Id,
                DeviceModel = repair.Device.DeviceModel,
                DeviceType = repair.Device.GetType().Name,
                ClientContact = repair.ClientContact,
                Country = repair.Country,
                IssueType = repair.IssueType,
                CurrentStatus = repair.CurrentStatus,
                StartDate = repair.StartDate,
                EndDate = repair.EndDate,
                PhaseHistory = repair.PhaseHistory
                    .Select(h => new RepairPhaseHistoryDto
                    {
                        Status = h.Status,
                        ChangedAt = h.ChangedAt,
                        Notes = h.Notes
                    })
                    .ToList()
            };

            return Ok(response);
        }
        catch(Exception ex) 
        {
            return BadRequest();
        }
    }
}
