using CleanLeave.Domain.Enums;

namespace CleanLeave.Domain.Entities;

using CleanLeave.Domain.ValueObjects;

public sealed class WorkRecord
{
    public Guid Id { get; }
    public Guid EmployeeId { get; }
    public TimePeriod Period { get; private set; }
    public WorkRecordCategory Category { get; }
    public WorkRecordStatus Status { get; private set; }

    private WorkRecord(
        Guid id,
        Guid employeeId,
        TimePeriod period,
        WorkRecordCategory category)
    {
        Id = id;
        EmployeeId = employeeId;
        Period = period;
        Category = category;
        Status = WorkRecordStatus.Pending;
    }

    public static WorkRecord Create(
        Guid employeeId,
        TimePeriod period,
        WorkRecordCategory category)
    {
        if (employeeId == Guid.Empty)
            throw new ArgumentException("EmployeeId is required");

        return new WorkRecord(
            Guid.NewGuid(),
            employeeId,
            period,
            category);
    }

    public void Approve()
    {
        if (Status != WorkRecordStatus.Pending)
            throw new InvalidOperationException(
                $"Cannot approve record in {Status} state");

        Status = WorkRecordStatus.Approved;
    }

    public void Reject()
    {
        if (Status != WorkRecordStatus.Pending)
            throw new InvalidOperationException(
                $"Cannot reject record in {Status} state");

        Status = WorkRecordStatus.Rejected;
    }
}