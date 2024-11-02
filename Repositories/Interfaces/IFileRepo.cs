namespace FINALPROJECT.Repositories.Interfaces
{
    public interface IFileRepo
    {
        Task<string> UploadAsync(IFormFile formFile);
    }
}
