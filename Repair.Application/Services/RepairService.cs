using Repair.Application.Interfaces;
using Repair.Contracts.Repairs;
using Repair.Domain.Devices;
using Repair.Domain.Enums;
using Repair.Domain.Repairs;

namespace Repair.Application.Services;

public class RepairService : IRepairService
{
    private readonly IRepairRequestRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RepairService(
        IRepairRequestRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<RepairRequestListItemDto>> GetAllRepairRequestsAsync(CancellationToken cancellationToken)
    {
        var repairs = await _repository.GetAllAsync(cancellationToken);

        return repairs.Select(_ => new RepairRequestListItemDto
        {
            RepairRequestId = _.Id,
            DeviceModel = _.Device.DeviceModel,
            DeviceType = _.Device.GetType().Name,
            ClientContact = _.ClientContact,
            Country = _.Country,
            CurrentStatus = _.CurrentStatus.ToString(),
            StartDate = _.StartDate,
            EndDate = _.EndDate
        }).ToList();
    }


    public async Task<Guid> CreatePhoneRepairAsync(
        string deviceModel,
        string imei,
        string clientContact,
        string country,
        IssueType issueType,
        CancellationToken cancellationToken)
    {
        var phone = new Phone(model: deviceModel, imei: imei);

        var repairRequest = new RepairRequest(
            phone,
            clientContact,
            country,
            issueType);

        await _repository.AddAsync(repairRequest, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return repairRequest.Id;
    }

    public async Task UpdateRepairStatusAsync(
        Guid repairRequestId,
        RepairStatus newStatus,
        string? notes,
        CancellationToken cancellationToken)
    {
        var repairRequest =
            await _repository.GetByIdAsync(repairRequestId, cancellationToken)
            ?? throw new InvalidOperationException("Repair request not found.");

        repairRequest.UpdateStatus(newStatus, notes);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<RepairRequest?> GetRepairRequestAsync(
        Guid repairRequestId,
        CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(repairRequestId, cancellationToken);
    }
}
