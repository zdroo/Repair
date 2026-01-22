using Repair.Domain.Common;
using Repair.Domain.Devices;
using Repair.Domain.Enums;
using Repair.Domain.Events;

namespace Repair.Domain.Repairs;

public class RepairRequest : BaseEntity
{
    public Guid DeviceId { get; private set; }
    public Device Device { get; private set; } = null!;

    public string ClientContact { get; private set; } = null!;
    public string Country { get; private set; } = null!;

    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public IssueType IssueType { get; private set; }
    public RepairStatus CurrentStatus { get; private set; }

    private readonly List<RepairPhaseHistory> _history = new();
    public IReadOnlyCollection<RepairPhaseHistory> PhaseHistory => _history;

    private RepairRequest() { }

    public RepairRequest(
        Device device,
        string clientContact,
        string country,
        IssueType issueType)
    {
        Device = device;
        ClientContact = clientContact;
        Country = country;
        IssueType = issueType;

        StartDate = DateTime.UtcNow;
        CurrentStatus = RepairStatus.Reception;

        AddHistory(CurrentStatus);
    }

    public void UpdateStatus(RepairStatus newStatus, string? notes = null)
    {
        if (newStatus < CurrentStatus)
            throw new InvalidOperationException("Cannot move repair status backwards.");

        var oldStatus = CurrentStatus;

        CurrentStatus = newStatus;
        AddHistory(newStatus, notes);

        if (newStatus == RepairStatus.Return)
            EndDate = DateTime.UtcNow;

        var domainEvent = new RepairStatusChangedDomainEvent(
            Id,
            oldStatus,
            newStatus,
            DateTime.UtcNow);

        AddDomainEvent(domainEvent);
    }

    private void AddHistory(RepairStatus status, string? notes = null)
    {
        _history.Add(new RepairPhaseHistory(status, notes));
    }
}
