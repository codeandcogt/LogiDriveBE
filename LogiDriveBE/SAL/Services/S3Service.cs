using LogiDriveBE.SAL.Sao;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace LogiDriveBE.SAL.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:BucketName"];
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file provided");

            try
            {
                var fileName = $"{folder}/{Guid.NewGuid()}-{file.FileName}";

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileName,
                    InputStream = memoryStream,
                    ContentType = file.ContentType
                };

                await _s3Client.PutObjectAsync(request);
                return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading file to S3: {ex.Message}");
            }
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;

            try
            {
                var key = imageUrl.Split($"{_bucketName}.s3.amazonaws.com/")[1];
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key
                };

                await _s3Client.DeleteObjectAsync(deleteRequest);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting file from S3: {ex.Message}");
            }
        }
    }
}
