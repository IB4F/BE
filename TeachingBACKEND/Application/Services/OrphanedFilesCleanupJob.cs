using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;

namespace TeachingBACKEND.Application.Services;

public class OrphanedFilesCleanupJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OrphanedFilesCleanupJob> _logger;
    private static readonly TimeSpan Interval = TimeSpan.FromHours(24);

    public OrphanedFilesCleanupJob(IServiceScopeFactory scopeFactory, ILogger<OrphanedFilesCleanupJob> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(Interval, stoppingToken).ConfigureAwait(false);
            await RunCleanupAsync(stoppingToken).ConfigureAwait(false);
        }
    }

    private async Task RunCleanupAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();

        var orphans = await context.OrphanedFiles
            .OrderBy(f => f.CreatedAt)
            .ToListAsync(stoppingToken);

        if (orphans.Count == 0)
            return;

        _logger.LogInformation("OrphanedFilesCleanupJob: processing {Count} orphaned file(s)", orphans.Count);

        foreach (var orphan in orphans)
        {
            if (stoppingToken.IsCancellationRequested)
                break;

            try
            {
                // DeleteFileAsync returns false when the file record no longer exists in the DB
                // (already cleaned up by another means) — treat as success
                await fileService.DeleteFileAsync(orphan.FileId);
                context.OrphanedFiles.Remove(orphan);
                await context.SaveChangesAsync(stoppingToken);
                _logger.LogInformation("OrphanedFilesCleanupJob: deleted file {FileId}", orphan.FileId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "OrphanedFilesCleanupJob: failed to delete file {FileId}, will retry next cycle", orphan.FileId);
            }
        }
    }
}
