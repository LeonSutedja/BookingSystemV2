using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using LeonSutedja.BookingSystem.Shared.Handler.Validation;

namespace LeonSutedja.BookingSystem.Shared.Handler.Update
{
    public class GenericUpdateHandler<TCommand, TEntity> : IUpdateHandler<TCommand, TEntity> 
        where TEntity : Entity
        where TCommand : IUpdateCommand<TEntity>
    {
        private readonly IEnumerable<IBusinessRule<TCommand, TEntity>> _businessRules;
        private readonly IRepository<TEntity> _tRepository;
        private readonly IUpdateCommandExecutorFactory _commandExecutorFactory;

        public GenericUpdateHandler(
            IEnumerable<IBusinessRule<TCommand, TEntity>> businessRules,
            IRepository<TEntity> tRepository,
            IUpdateCommandExecutorFactory commandExecutorFactory)
        {
            _businessRules = businessRules;
            _tRepository = tRepository;
            _commandExecutorFactory = commandExecutorFactory;
        }

        public HandlerResponse Update(TCommand input)
        {
            // do validation
            var validationResults = _businessRules.ToList().Select(br => br.IsValid(input)).ToList();
            var isNotValid = validationResults.Any(vr => !vr.IsValid);
            if (isNotValid) return HandlerResponse.Failed(
                string.Join(", ", 
                validationResults.Select(vr => string.Join(", ", vr.Messages))));

            var executor = _commandExecutorFactory.Create<TCommand, TEntity>();
            var entityToUpdate = _tRepository.Get(input.Id);
            executor.Execute(entityToUpdate, input);

            var id = _tRepository.Update(entityToUpdate);
            return HandlerResponse.Success("Updated", input.Id);
        }
    }
}