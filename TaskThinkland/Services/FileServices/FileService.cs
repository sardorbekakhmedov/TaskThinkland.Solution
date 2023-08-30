namespace TaskThinkland.Api.Services.FileServices;

public class FileService : IFileService
{
    private const string RootFolderName = "wwwroot";

    private string CheckExistsFolder(string folderName)
    {
        var rootFolderPath = Path.Combine(Environment.CurrentDirectory, RootFolderName);

        if (!Directory.Exists(rootFolderPath))
            Directory.CreateDirectory(rootFolderPath);

        var newFolderPath = Path.Combine(rootFolderPath, folderName);

        if (!Directory.Exists(newFolderPath))
            Directory.CreateDirectory(newFolderPath);

        return newFolderPath;
    }

    public async ValueTask<string> SaveFileAsync(IFormFile file, string folderName)
    {
        var folderPath = CheckExistsFolder(folderName);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        var filePath = Path.Combine(folderPath, fileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        
        return filePath;
    }

    public void DeleteFile(string filePath)
    {
        var currentFile = Path.Combine(Environment.CurrentDirectory, filePath);

        if(File.Exists(currentFile))
            File.Delete(currentFile);
    }
}