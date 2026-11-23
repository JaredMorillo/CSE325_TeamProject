using CSE325TeamProject.Data;
using CSE325TeamProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CSE325TeamProject.Services;

public sealed class JobApplicationService(ApplicationDbContext dbContext)
{
    public Task<List<JobApplication>> GetForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        return dbContext.JobApplications
            .AsNoTracking()
            .Where(application => application.UserId == userId)
            .OrderByDescending(application => application.AppliedOn)
            .ToListAsync(cancellationToken);
    }

    public Task<JobApplication?> FindForUserAsync(
        string userId,
        int applicationId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        return dbContext.JobApplications
            .AsNoTracking()
            .SingleOrDefaultAsync(
                application => application.Id == applicationId && application.UserId == userId,
                cancellationToken);
    }

    public async Task<JobApplication> CreateAsync(
        string userId,
        JobApplicationInput input,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);
        ArgumentNullException.ThrowIfNull(input);

        var application = new JobApplication
        {
            Company = input.Company,
            JobTitle = input.JobTitle,
            Status = input.Status,
            AppliedOn = input.AppliedOn,
            Notes = input.Notes,
            UserId = userId
        };

        dbContext.JobApplications.Add(application);
        await dbContext.SaveChangesAsync(cancellationToken);
        return application;
    }

    public async Task<bool> UpdateAsync(
        string userId,
        int applicationId,
        JobApplicationInput input,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);
        ArgumentNullException.ThrowIfNull(input);

        var application = await dbContext.JobApplications.SingleOrDefaultAsync(
            item => item.Id == applicationId && item.UserId == userId,
            cancellationToken);

        if (application is null)
        {
            return false;
        }

        application.Company = input.Company;
        application.JobTitle = input.JobTitle;
        application.Status = input.Status;
        application.AppliedOn = input.AppliedOn;
        application.Notes = input.Notes;

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(
        string userId,
        int applicationId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        var application = await dbContext.JobApplications.SingleOrDefaultAsync(
            item => item.Id == applicationId && item.UserId == userId,
            cancellationToken);

        if (application is null)
        {
            return false;
        }

        dbContext.JobApplications.Remove(application);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
