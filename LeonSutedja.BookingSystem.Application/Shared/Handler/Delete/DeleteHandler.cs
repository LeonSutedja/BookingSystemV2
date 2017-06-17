using Abp.Dependency;
using Abp.Domain.Entities;

namespace LeonSutedja.BookingSystem.Shared.Handler.Delete
{
    public interface IDeleteHandler<in TCommand, TEntity>
        where TEntity : Entity
        where TCommand : IDeleteCommand<TEntity>
    {
        HandlerResponse Delete(TCommand input);
    }

    public interface IDeleteHandlerFactory
    {
        IDeleteHandler<TCommand, TEntity> CreateHandler<TCommand, TEntity>() 
            where TEntity : Entity
            where TCommand : IDeleteCommand<TEntity>;
    }

    public class DeleteHandlerFactory : IDeleteHandlerFactory
    {
        private readonly IIocResolver _iocResolver;

        public DeleteHandlerFactory(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IDeleteHandler<TCommand, TEntity> CreateHandler<TCommand, TEntity>()
            where TEntity : Entity
            where TCommand : IDeleteCommand<TEntity>
            => _iocResolver.Resolve<IDeleteHandler<TCommand, TEntity>>();
    }
}
