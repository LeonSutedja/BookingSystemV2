using Abp.Dependency;
using Abp.Domain.Entities;

namespace LeonSutedja.BookingSystem.Shared.Handler.Update
{
    public interface IUpdateCommandHandler<in TCommand, TEntity>
        where TCommand : IUpdateCommand<TEntity>
        where TEntity : Entity
    {
        TEntity Execute(TEntity entity, TCommand command);
    }

    public interface IUpdateCommandExecutorFactory
    {
        IUpdateCommandHandler<TCommand, TEntity> Create<TCommand, TEntity>()
            where TCommand : IUpdateCommand<TEntity>
            where TEntity : Entity;
    }

    public class UpdateCommandExecutorFactory : IUpdateCommandExecutorFactory
    {
        private readonly IIocResolver _iocResolver;

        public UpdateCommandExecutorFactory(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IUpdateCommandHandler<TCommand, TEntity> Create<TCommand, TEntity>() where TCommand : IUpdateCommand<TEntity> where TEntity : Entity
            => (IUpdateCommandHandler<TCommand, TEntity>)
                _iocResolver.Resolve(typeof(IUpdateCommandHandler<,>).MakeGenericType(typeof(TCommand), typeof(TEntity)));
    }
}