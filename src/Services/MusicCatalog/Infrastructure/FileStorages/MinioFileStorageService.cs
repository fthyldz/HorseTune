using Application.Abstractions.Infrastructure;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.FileStorages;

public class MinioFileStorageService(IMinioClient minioClient, IOptions<MinioSetting> minioSetting)
    : IFileStorageService
{
    private readonly MinioSetting _minioSetting = minioSetting.Value;

    public async Task UploadFileAsync(IFormFile file, string fileName, CancellationToken cancellationToken = default)
    {
        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();
            
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_minioSetting.BucketName)
                .WithObject(fileName)
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType);
            
            await minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
        }
    }

    public async Task<string?> GetFileUrlAsync(string fileName)
    {
        var presignedGetObjectArgs = new PresignedGetObjectArgs()
            .WithBucket(_minioSetting.BucketName)
            .WithObject(fileName)
            .WithExpiry(Convert.ToInt32((new TimeSpan(0, 10, 0).TotalSeconds)));
        
        return await minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
    }
}