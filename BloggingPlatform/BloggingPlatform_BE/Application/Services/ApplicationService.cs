using BloggingPlatform_BE.Domain.Interfaces;
using Microsoft.Data.Sqlite;

namespace BloggingPlatform_BE.Application.Services;

public class ApplicationService
{
    private string _filePath;
    private IRepositoryService _repositoryService;

    public ApplicationService(string filePath, IRepositoryService repositoryService)
    {
        _filePath = filePath;
        _repositoryService = repositoryService;
        Initialize();
    }

    private void CreateDataBase()
    {
        string dbPath = "";
        string connectionString = $"Data Source={dbPath}";
        
        if(!File.Exists(_filePath)) 
            _repositoryService.InitializeTables();
    }


    private void Initialize() => CreateDataBase();
}
