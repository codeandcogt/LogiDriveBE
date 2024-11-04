namespace LogiDriveBE.SAL.Sao
{
    public interface IS3Service
    {
        Task<string> UploadImageAsync(IFormFile file, string folder);
        Task DeleteImageAsync(string imageUrl);
    }
}
