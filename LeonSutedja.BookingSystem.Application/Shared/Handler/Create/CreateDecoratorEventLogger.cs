using Abp.Domain.Entities;
using System;

namespace LeonSutedja.BookingSystem.Shared.Handler.Create
{
    public class CreateDecoratorEventLogger<TCommand, TEntity> : ICreateHandler<TCommand, TEntity>
        where TEntity : Entity
        where TCommand : ICreateCommand<TEntity>
    {
        private readonly ICreateHandler<TCommand, TEntity> _handler;

        public CreateDecoratorEventLogger(ICreateHandler<TCommand, TEntity> handler)
        {
            _handler = handler;
        }

        public HandlerResponse Create(TCommand input)
        {
            // Log the command here
            var result = _handler.Create(input);
            var e = input.GetEvent("SYSTEM", DateTime.Now);
            var eJson = e.ToJson();
            // Log the result here
            return result;
        }
    }
}
