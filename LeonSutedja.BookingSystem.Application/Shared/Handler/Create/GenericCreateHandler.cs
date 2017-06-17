using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using LeonSutedja.BookingSystem.Shared.Handler.Validation;

namespace LeonSutedja.BookingSystem.Shared.Handler.Create
{
    public class GenericCreateHandler<TCommand, TEntity> : ICreateHandler<TCommand, TEntity> 
        where TEntity : Entity
        where TCommand : ICreateCommand<TEntity>
    {
        private readonly IEnumerable<IBusinessRule<TCommand, TEntity>> _businessRules;
        private readonly IRepository<TEntity> _tRepository;
        private readonly ICreateCommandMapperFactory _commandMapperFactory;

        public GenericCreateHandler(
            IEnumerable<IBusinessRule<TCommand, TEntity>> businessRules,
            IRepository<TEntity> tRepository, 
            ICreateCommandMapperFactory commandMapperFactory)
        {
            _businessRules = businessRules;
            _tRepository = tRepository;
            _commandMapperFactory = commandMapperFactory;
        }

        public HandlerResponse Create(TCommand input)
        {
            // do validation
            var validationResults = _businessRules.ToList().Select(br => br.IsValid(input)).ToList();
            var isNotValid = validationResults.Any(vr => !vr.IsValid);
            if (isNotValid) return HandlerResponse.Failed(
                string.Join(", ", 
                validationResults.Select(vr => string.Join(", ", vr.Messages))));

            var mapper = _commandMapperFactory.Create<TCommand, TEntity>();
            var entity = mapper.Create(input);

            var id = _tRepository.InsertAndGetId(entity);
            return HandlerResponse.Success("Created", id);
        }
    }
}