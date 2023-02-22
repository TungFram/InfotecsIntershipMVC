using InfotecsIntershipMVC.Controllers;
using InfotecsIntershipMVC.DAL.DbContexts;
using InfotecsIntershipMVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;

namespace InfotecsIntershipMVC.DAL.Repositories
{
    public class FilesRepository : IGenericRepository<FileEntity>, IGenericAsyncRepository<FileEntity>
    {
        private readonly ILogger<FilesRepository> _logger;
        private readonly InfotecsDBContext _dbContext;

        public FilesRepository(ILogger<FilesRepository> logger,InfotecsDBContext dbContext)
        {
            _dbContext = dbContext != null ? dbContext : 
                throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(FileEntity fileEntity)
        {
            await _dbContext.Files.AddAsync(fileEntity);
            await _dbContext.SaveChangesAsync();
            
            return fileEntity.FileID;
        }

        public Guid Create(FileEntity fileEntity)
        {
            _dbContext.Files.Add(fileEntity);
            _dbContext.SaveChanges();

            return fileEntity.FileID;
        }

        public FileEntity? FindByName(string name)
        {
            return _dbContext.Files
                .Include(file => file.Records)
                .Include(file => file.Result)
                .FirstOrDefault(file => file.Name == name);
        }

        public async Task<FileEntity?> FindByNameAsync(string name)
        {
            return await _dbContext.Files
                .Include(file => file.Records)
                .Include(file => file.Result)
                .FirstOrDefaultAsync(file => file.Name == name);
        }

        public FileEntity? FindById(Guid id)
        {
            return _dbContext.Files
                .Include(file => file.Records)
                .Include(file => file.Result)
                .FirstOrDefault(file => file.FileID == id);
        }

        public async Task<FileEntity?> FindByIdAsync(Guid id)
        {
            return await _dbContext.Files
                .Include(file => file.Records)
                .Include(file => file.Result)
                .FirstOrDefaultAsync(file => file.FileID == id);
        }

        public IEnumerable<FileEntity> GetAll()
        {
            var files = new List<FileEntity>();

            // Get files with records and result.
            foreach (var file in _dbContext.Files.ToList())
            {
                files.Add(FindById(file.FileID));
            }

            return files;
        }

        public async Task<IEnumerable<FileEntity>> GetAllAsync()
        {
            var files = new List<FileEntity>();

            // Get files with records and result.
            foreach (var file in _dbContext.Files.ToList())
            {
                files.Add(await FindByIdAsync(file.FileID));
            }

            return files;
        }

        public ImmutableList<FileEntity> GetAllImmutable()
        {
            var files = new List<FileEntity>();

            // Get files with records and result.
            foreach (var file in _dbContext.Files.ToList())
            {
                files.Add(FindById(file.FileID));
            }

            return files.ToImmutableList();
        }

        public int Update(FileEntity newEntity)
        {
            FileEntity oldEntity = FindById(newEntity.FileID);
            if (oldEntity == null)  
            {
                _logger.LogWarning($"Can't update file {newEntity}: cannot find it in db.");
                return 0;
            }

            var entry = _dbContext.Entry(oldEntity);
            _logger.LogInformation($"File {oldEntity} was updated:" +
                $"name from {oldEntity.Name} to {newEntity.Name}," +
                $"records from {oldEntity.Records} to {newEntity.Records}.");

            oldEntity = oldEntity.ToBuilder()
                .WithName(newEntity.Name)
                .WithRecords(newEntity.Records)
                .Build();

            entry.State = EntityState.Modified;
            return _dbContext.SaveChanges();
        }

        public async Task<int> UpdateAsync(FileEntity newEntity)
        {
            FileEntity? oldEntity = await FindByIdAsync(newEntity.FileID);
            if (oldEntity == default)
            {
                _logger.LogWarning($"Can't update file {newEntity}: cannot find it in db.");
                return 0;
            }

            var entry = _dbContext.Entry(newEntity);
            _logger.LogInformation($"File {oldEntity} was updated to {newEntity}.");

            oldEntity = oldEntity.ToBuilder()
                .WithName(newEntity.Name)
                .WithRecords(newEntity.Records)
                .Build();

            entry.State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public void DeleteById(Guid id)
        {
            FileEntity? existedEntity = FindById(id);
            if (existedEntity == null)
            {
                _logger.LogWarning($"Can't find file with id {id}. File automaticly deleted.");
                return;
            }

            _dbContext.Remove(existedEntity);
            _dbContext.SaveChanges();
        }

        public async void DeleteByIdAsync(Guid id)
        {
            FileEntity? existedEntity = await FindByIdAsync(id);
            if (existedEntity == null)
            {
                _logger.LogWarning($"Can't find file with id {id}. File automaticly deleted.");
                return;
            }

            _dbContext.Remove(existedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public void DeleteAll()
        {
            IEnumerable<FileEntity> fileEntities = GetAll();
            foreach (FileEntity fileEntity in fileEntities)
            {
                _dbContext.Remove(fileEntity);
            }

            _dbContext.SaveChanges();
        }

        public async void DeleteAllAsync()
        {
            var fileEntities = await GetAllAsync();
            foreach(FileEntity fileEntity in fileEntities)
            {
                _dbContext.Remove(fileEntity);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
