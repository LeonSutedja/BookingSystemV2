using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Repositories;
using LeonSutedja.BookingSystem.Entities;
using LeonSutedja.BookingSystem.Shared.Handler;
using LeonSutedja.BookingSystem.Shared.Handler.Update;

namespace LeonSutedja.BookingSystem.Services.Commands
{
    public class BookAppointmentOnScheduleCommand : IUpdateCommand<Schedule>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int CustomerId { get; set; }
        
        public IEvent GetEvent(string triggeredBy, DateTime triggeredDateTime)
        {
            return new BookAppointmentScheduled(this, triggeredBy, triggeredDateTime);
        }
    }

    public class BookAppointmentScheduled : Event
    {
        public BookAppointmentScheduled(BookAppointmentOnScheduleCommand cmd, string triggeredBy, DateTime triggeredDateTime)
            : base(Guid.NewGuid(), triggeredBy, triggeredDateTime)
        {
            StartTime = cmd.StartTime;
            EndTime = cmd.EndTime;
            CustomerId = cmd.CustomerId;
        }
        
        [Required]
        public DateTime StartTime { get; private set; }

        [Required]
        public DateTime EndTime { get; private set; }

        [Required]
        public int CustomerId { get; private set; }        
    }

    public class CreateAppointmentCommandHandler : IUpdateCommandHandler<BookAppointmentOnScheduleCommand, Schedule>
    {
        private readonly IRepository<Customer> _customerRepository;

        public CreateAppointmentCommandHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Schedule Execute(Schedule entity, BookAppointmentOnScheduleCommand command)
        {
            var customer = _customerRepository.Get(command.CustomerId);
            entity.AddAppointment(customer, command.StartTime, command.EndTime);
            return entity;
        }
    }
}