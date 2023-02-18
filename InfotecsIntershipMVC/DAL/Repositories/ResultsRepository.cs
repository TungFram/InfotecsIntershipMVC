using InfotecsIntershipMVC.DAL.DbContexts;
using InfotecsIntershipMVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Security.Principal;

namespace InfotecsIntershipMVC.DAL.Repositories
{
    public class ResultsRepository :
        IGenericRepository<ResultEntity>,
        IGenericAsyncRepository<ResultEntity>
    {

        private readonly ILogger<FilesRepository> _logger;
        private readonly InfotecsDBContext _dbContext;

        public ResultsRepository(ILogger<FilesRepository> logger, InfotecsDBContext dbContext)
        {
            _dbContext = dbContext != null ? dbContext :
                throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
        }

        public Guid Create(ResultEntity entity)
        {
            _dbContext.Results.Add(entity);

            _dbContext.SaveChanges();
            return entity.ResultID;
        }

        public async Task<Guid> CreateAsync(ResultEntity entity)
        {
            await _dbContext.Results.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity.ResultID;
        }

        public void DeleteAll()
        {
            _dbContext.Results.RemoveRange(_dbContext.Results);

            _dbContext.SaveChanges();
        }

        public async void DeleteAllAsync()
        {
            _dbContext.Results.RemoveRange(await GetAllAsync());

            await _dbContext.SaveChangesAsync(true);
        }

        public void DeleteById(Guid id)
        {
            var entity = FindById(id);
            if (entity == null)
            {
                _logger.LogWarning($"Can't find file with id {id}. File automaticly deleted.");
                return;
            }

            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async void DeleteByIdAsync(Guid id)
        {
            var entity = await FindByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning($"Can't find file with id {id}. File automaticly deleted.");
                return;
            }

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public ResultEntity? FindById(Guid id)
        {
            return _dbContext.Results
                .FirstOrDefault(result => result.ResultID == id);
        }

        public async Task<ResultEntity?> FindByIdAsync(Guid id)
        {
            return await _dbContext.Results
                .FirstOrDefaultAsync(result => result.ResultID == id);
        }

        public IEnumerable<ResultEntity> GetAll()
        {
            return _dbContext.Results.ToList();
        }

        public async Task<IEnumerable<ResultEntity>> GetAllAsync()
        {
            return await _dbContext.Results.ToListAsync();
        }

        public IEnumerable<ResultEntity> GetAllImmutable()
        {
            return _dbContext.Results.ToImmutableList();
        }

        public int Update(ResultEntity entity)
        {
            ResultEntity existedEntity = FindById(entity.ResultID);
            if (existedEntity == null)
            {
                _logger.LogWarning($"Can't update file {entity}: cannot find it in db.");
                return 0;
            }

            var entry = _dbContext.Entry(existedEntity);

            _logger.LogInformation($"Result {existedEntity} was updated to {entity}." +
                $"For file {existedEntity.FileName} ({entity.FileName}).");

            existedEntity = new ResultEntity(entity);
            entry.State = EntityState.Modified;
            return _dbContext.SaveChanges();
        }

        public async Task<int> UpdateAsync(ResultEntity entity)
        {
            ResultEntity existedEntity = await FindByIdAsync(entity.ResultID);
            if (existedEntity == null)
            {
                _logger.LogWarning($"Can't update file {entity}: cannot find it in db.");
                return 0;
            }

            var entry = _dbContext.Entry(existedEntity);

            _logger.LogInformation($"Result {existedEntity} was updated to {entity}." +
                $"For file {existedEntity.FileName} ({entity.FileName}).");

            existedEntity = new ResultEntity(entity);
            entry.State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
    }
}
