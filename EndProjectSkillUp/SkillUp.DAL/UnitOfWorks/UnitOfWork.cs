using SkillUp.Core.Entities;
using SkillUp.DAL.Context;
using SkillUp.DAL.Repositories.Abstractions;
using SkillUp.DAL.Repositories.Concretes.GenericRepository;

namespace SkillUp.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }


        //Dispose
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }


        //Get Repository
        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }


        //Save
        public int Save()
        {
            return _context.SaveChanges();
        }


        //SaveChanges
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();   
        }
    }
}
