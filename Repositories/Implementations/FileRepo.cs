using FINALPROJECT.Configurations;
using FINALPROJECT.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace FINALPROJECT.Repositories.Implementations
{
    public class FileRepo : IFileRepo
    {
        private readonly FileConfiguration _configuration;
        public FileRepo(IOptions<FileConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public async Task<string> UploadAsync(IFormFile? file)
        {
            if (file == null)
            {
                return null;
            }

            var a = file.ContentType.Split('/');
            var newName = $"IMG{a[0]}{Guid.NewGuid().ToString().Substring(6, 5)}.{a[1]}";

            var b = _configuration.Path;
            if (!Directory.Exists(b))
            {
                Directory.CreateDirectory(b);
            }

            var c = Path.Combine(b, newName);

            using (var d = new FileStream(c, FileMode.Create))
            {
                await file.CopyToAsync(d);
            }

            return newName;
        }
    }
}
