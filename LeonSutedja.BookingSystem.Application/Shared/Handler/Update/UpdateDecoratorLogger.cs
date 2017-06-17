using Abp.Domain.Entities;

namespace LeonSutedja.BookingSystem.Shared.Handler.Update
{
    public class UpdateDecoratorLogger<TCommand, TEntity> : IUpdateHandler<TCommand, TEntity>
        where TEntity : Entity
        where TCommand : IUpdateCommand<TEntity>
    {
        private readonly IUpdateHandler<TCommand, TEntity> _handler;

        public UpdateDecoratorLogger(IUpdateHandler<TCommand, TEntity> handler)
        {
            _handler = handler;
        }

        public HandlerResponse Update(TCommand input)
        {
            // Log the command here
            var result = _handler.Update(input);
            // Log the result here
            return result;
        }
    }
}
