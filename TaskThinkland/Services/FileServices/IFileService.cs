namespace TaskThinkland.Api.Services.FileServices;

public interface IFileService
{
    ValueTask<string> SaveFileAsync(IFormFile file, string folderName);
    void DeleteFile(string filePath);
}