using Abp.Domain.Entities;

namespace LeonSutedja.BookingSystem.Shared.Handler.Validation
{
    public interface IBusinessRule<in TCommand, TEntity>
        where TCommand : ICommand<TEntity>
        where TEntity : Entity
    {
        ValidationCommandResult IsValid(TCommand command);
    }
}
