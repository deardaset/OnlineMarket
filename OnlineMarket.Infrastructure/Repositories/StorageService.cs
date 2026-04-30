using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Repositories
{
    public class StorageService(IOptions<YandexStorageSettings> options) : IStorageService
    {
        private readonly AmazonS3Client _s3 = new(
                options.Value.AccessKey,
                options.Value.SecretKey,
                    new AmazonS3Config
                    {
                        ServiceURL = options.Value.Endpoint,
                        ForcePathStyle = true
                    });
        public async Task<string> UploadAsync(Stream stream, string fileName, string contentType)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            var key = $"{Guid.NewGuid()}{ext}";

            await _s3.PutObjectAsync(new PutObjectRequest
            {
                BucketName = options.Value.Bucket,
                Key = key,
                InputStream = stream,
                ContentType = contentType,
                CannedACL = S3CannedACL.PublicRead
            });

            return $"{options.Value.Endpoint}/{options.Value.Bucket}/{key}";
        }

        public async Task DeleteAsync(string fileUrl)
        {
            var key = fileUrl.Replace($"{options.Value.Endpoint}/{options.Value.Bucket}/", "");

            await _s3.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = options.Value.Bucket,
                Key = key
            });
        }
    }
}
