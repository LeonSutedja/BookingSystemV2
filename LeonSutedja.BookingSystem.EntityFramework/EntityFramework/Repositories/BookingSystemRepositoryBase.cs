using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace LeonSutedja.BookingSystem.EntityFramework.Repositories
{
    public abstract class BookingSystemRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<BookingSystemDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected BookingSystemRepositoryBase(IDbContextProvider<BookingSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class BookingSystemRepositoryBase<TEntity> : BookingSystemRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected BookingSystemRepositoryBase(IDbContextProvider<BookingSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
