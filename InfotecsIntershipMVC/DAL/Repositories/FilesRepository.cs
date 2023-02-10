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
        private readonly ILogger<HomeController> _logger;
        private readonly InfotecsDBContext _dbContext;

        public FilesRepository(ILogger<HomeController> logger,InfotecsDBContext dbContext)
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

        public FileEntity? FindById(Guid id)
        {
            return _dbContext.Files
                .Include(file => file.Records)
                .FirstOrDefault(f => f.FileID == id);
        }

        public async Task<FileEntity?> FindByIdAsync(Guid id)
        {
            return await _dbContext.Files
                .Include(file => file.Records)
                .FirstOrDefaultAsync(f => f.FileID == id);
        }

        public IEnumerable<FileEntity> GetAll()
        {
            return _dbContext.Files.ToList();
        }

        public async Task<IEnumerable<FileEntity>> GetAllAsync()
        {
            return await _dbContext.Files.ToListAsync();
        }

        public IEnumerable<FileEntity> GetAllImmutable()
        {
            return _dbContext.Files.ToImmutableList();
        }

        public int Update(FileEntity newEntity)
        {
            var entry = _dbContext.Entry(newEntity);
            FileEntity oldEntity = FindById(newEntity.FileID);
            if (oldEntity == null)
            {
                _logger.LogWarning($"Can't update file {newEntity}: cannot find it in db.");
                return 0;
            }

            FileEntity existedFileEntity = oldEntity.ToBuilder()
                .WithName(newEntity.Name)
                .WithRecords(newEntity.Records)
                .Build();

            entry.State = EntityState.Modified;
            _logger.LogInformation($"File {oldEntity} was updated to {existedFileEntity}.");

            _dbContext.SaveChanges();
            return 1;
        }

        public async Task<int> UpdateAsync(FileEntity newEntity)
        {
            var entry = _dbContext.Entry(newEntity);

            FileEntity? oldEntity = await FindByIdAsync(newEntity.FileID);
            if (oldEntity == default)
            {
                _logger.LogWarning($"Can't update file {newEntity}: cannot find it in db.");
                return 0;
            }

            FileEntity existedFileEntity = oldEntity.ToBuilder()
                .WithName(newEntity.Name)
                .WithRecords(newEntity.Records)
                .Build();

            entry.State = EntityState.Modified;
            _logger.LogInformation($"File {oldEntity} was updated to {existedFileEntity}.");

            await _dbContext.SaveChangesAsync();
            return 1;
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
