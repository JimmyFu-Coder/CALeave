using CleanLeave.Domain.Entities;
using CleanLeave.Domain.Enums;
using CleanLeave.Domain.ValueObjects;
using Xunit;

namespace CleanLeave.Domain.Tests.Entities;

public class WorkRecordTests
{
    [Fact]
    public void Create_Should_Create_WorkRecord_With_Pending_Status()
    {
        var employeeId = Guid.NewGuid();
        var period = TimePeriod.Create(
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 2));

        var record = WorkRecord.Create(
            employeeId,
            period,
            WorkRecordCategory.Leave);

        Assert.Equal(employeeId, record.EmployeeId);
        Assert.Equal(WorkRecordStatus.Pending, record.Status);
        Assert.Equal(WorkRecordCategory.Leave, record.Category);
        Assert.NotEqual(Guid.Empty, record.Id);
    }

    [Fact]
    public void Create_Should_Throw_When_EmployeeId_Is_Empty()
    {
        var period = TimePeriod.Create(
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 2));

        Assert.Throws<ArgumentException>(() =>
            WorkRecord.Create(
                Guid.Empty,
                period,
                WorkRecordCategory.Absence));
    }

    [Fact]
    public void Approve_Should_Change_Status_To_Approved()
    {
        var record = CreateValidRecord();

        record.Approve();

        Assert.Equal(WorkRecordStatus.Approved, record.Status);
    }

    [Fact]
    public void Approve_Should_Throw_When_Not_Pending()
    {
        var record = CreateValidRecord();
        record.Approve();

        Assert.Throws<InvalidOperationException>(() =>
            record.Approve());
    }

    private static WorkRecord CreateValidRecord()
    {
        return WorkRecord.Create(
            Guid.NewGuid(),
            TimePeriod.Create(
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 2)),
            WorkRecordCategory.Overtime);
    }
}