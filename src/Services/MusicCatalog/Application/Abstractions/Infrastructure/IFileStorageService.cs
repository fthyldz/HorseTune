using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Infrastructure;

public interface IFileStorageService
{
    Task UploadFileAsync(IFormFile file, string fileName, CancellationToken cancellationToken = default);
    Task<string?> GetFileUrlAsync(string fileName);
}