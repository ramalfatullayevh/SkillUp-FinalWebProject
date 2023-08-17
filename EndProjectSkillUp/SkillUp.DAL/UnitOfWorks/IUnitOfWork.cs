using SkillUp.Core.Entities;
using SkillUp.DAL.Repositories.Abstractions;

namespace SkillUp.DAL.UnitOfWorks
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        //Get Repository
        IRepository<T> GetRepository<T>() where T : BaseEntity;

        //Save
        Task<int> SaveAsync();

        int Save();
    }
}
