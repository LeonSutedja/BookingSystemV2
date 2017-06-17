using Abp.Dependency;
using Abp.Domain.Entities;

namespace LeonSutedja.BookingSystem.Shared.Handler.Create
{
    public interface ICreateCommandMapper<in TCommand, out TEntity>
        where TCommand : ICreateCommand<TEntity>
        where TEntity : Entity
    {
        TEntity Create(TCommand command);
    }

    public interface ICreateCommandMapperFactory
    {
        ICreateCommandMapper<TCommand, TEntity> Create<TCommand, TEntity>()
            where TCommand : ICreateCommand<TEntity>
            where TEntity : Entity;
    }

    public class CreateCommandMapperFactory : ICreateCommandMapperFactory
    {
        private readonly IIocResolver _iocResolver;

        public CreateCommandMapperFactory(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public ICreateCommandMapper<TCommand, TEntity> Create<TCommand, TEntity>() where TCommand : ICreateCommand<TEntity> where TEntity : Entity
            => (ICreateCommandMapper<TCommand, TEntity>)
                _iocResolver.Resolve(typeof(ICreateCommandMapper<,>).MakeGenericType(typeof(TCommand), typeof(TEntity)));
    }
}