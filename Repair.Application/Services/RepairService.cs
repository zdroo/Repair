using Repair.Application.Persistence;
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

    public async Task<Guid> CreatePhoneRepairAsync(
        string phoneModel,
        string imei,
        string clientContact,
        string country,
        IssueType issueType,
        CancellationToken cancellationToken)
    {
        var phone = new Phone(model:phoneModel, imei:imei);

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
