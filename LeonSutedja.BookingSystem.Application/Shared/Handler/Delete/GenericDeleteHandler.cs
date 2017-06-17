using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using LeonSutedja.BookingSystem.Shared.Handler.Validation;

namespace LeonSutedja.BookingSystem.Shared.Handler.Delete
{
    public class GenericDeleteHandler<TCommand, TEntity> : IDeleteHandler<TCommand, TEntity> 
        where TEntity : Entity
        where TCommand : IDeleteCommand<TEntity>
    {
        private readonly IEnumerable<IBusinessRule<TCommand, TEntity>> _businessRules;
        private readonly IRepository<TEntity> _tRepository;

        public GenericDeleteHandler(
            IEnumerable<IBusinessRule<TCommand, TEntity>> businessRules,
            IRepository<TEntity> tRepository)
        {
            _businessRules = businessRules;
            _tRepository = tRepository;
        }

        public HandlerResponse Delete(TCommand input)
        {
            // do validation
            var validationResults = _businessRules.ToList().Select(br => br.IsValid(input)).ToList();
            var isNotValid = validationResults.Any(vr => !vr.IsValid);
            if (isNotValid) return HandlerResponse.Failed(
                string.Join(", ", 
                validationResults.Select(vr => string.Join(", ", vr.Messages))));

            var entity = _tRepository.Get(input.Id);
            _tRepository.Delete(entity);

            return HandlerResponse.Success("Delete!", 1);
        }
    }
}