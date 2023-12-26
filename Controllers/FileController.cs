using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using File = Entities.Models.File;

namespace questionnaire.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController:ControllerBase
{
    private IWebHostEnvironment _environment;
    private IConfiguration _config;
    private IRepositoryWrapper _repository;

    public FileController(IWebHostEnvironment environment, IConfiguration config, IRepositoryWrapper repository)
    {
        _environment = environment;
        _config = config;
        _repository = repository;
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if(file.Length == 0)
            return BadRequest("Файл пуст");

        var pathToDir = _environment.WebRootPath + $"\\{_config.GetSection("FileUpload:Path").Value}\\";
        
        if (!Directory.Exists(pathToDir))
            Directory.CreateDirectory(pathToDir);
        
        await using (var fileStream = System.IO.File.Create(pathToDir + file.FileName))
        {
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }

        var filePath = $"/{_config.GetSection("FileUpload:Path").Value}/" + file.FileName;
        var fileExtension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        
        var fileEntity = new File
        {
            FileId = Guid.NewGuid(),
            FileName = file.FileName,
            FileExtension = fileExtension,
            FilePath = filePath
        };
        _repository.File.CreateFile(fileEntity);

        return Created(nameof(UploadFile), fileEntity);
    }

    [HttpGet]
    public async Task<IActionResult> DownloadFile(string filename)
    {
        var path = Path.Combine(_environment.WebRootPath, _config.GetSection("FileUpload:Path").Value, filename);

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(path, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        var bytes = await System.IO.File.ReadAllBytesAsync(path);
        return File(bytes, contentType, Path.GetFileName(path));
    }
}