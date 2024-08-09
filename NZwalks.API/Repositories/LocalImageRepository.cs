using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class LocalImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly NZWalksDbContext _dbContext;

    public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }
    public async Task<Image> Upload(Image image)
    {
        var localFilePath = Path.Combine(
            _webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtention}");

        // Upload image to local path
        await using var stream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(stream);

        var http = _httpContextAccessor.HttpContext.Request;
        var urlFilePath =
            $"{http.Scheme}://{http.Host}{http.PathBase}/Images/{image.FileName}{image.FileExtention}";

        image.FilePath = urlFilePath;

        // Add Image to the Images database
        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();

        return image;
    }
}