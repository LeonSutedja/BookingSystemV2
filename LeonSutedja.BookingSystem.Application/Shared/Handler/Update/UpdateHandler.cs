using Abp.Dependency;
using Abp.Domain.Entities;

namespace LeonSutedja.BookingSystem.Shared.Handler.Update
{
    public interface IUpdateHandler<in TCommand, TEntity>
        where TEntity : Entity
        where TCommand : IUpdateCommand<TEntity>
    {
        HandlerResponse Update(TCommand input);
    }

    public interface IUpdateHandlerFactory
    {
        IUpdateHandler<TCommand, TEntity> CreateHandler<TCommand, TEntity>() 
            where TEntity : Entity
            where TCommand : IUpdateCommand<TEntity>;
    }

    public class UpdateHandlerFactory : IUpdateHandlerFactory
    {
        private readonly IIocResolver _iocResolver;

        public UpdateHandlerFactory(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IUpdateHandler<TCommand, TEntity> CreateHandler<TCommand, TEntity>()
            where TEntity : Entity
            where TCommand : IUpdateCommand<TEntity>
            => _iocResolver.Resolve<IUpdateHandler<TCommand, TEntity>>();
    }
}
