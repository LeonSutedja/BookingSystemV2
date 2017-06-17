using System.Data.Entity;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using LeonSutedja.BookingSystem.Entities;
using LeonSutedja.BookingSystem.Services.Commands;
using LeonSutedja.BookingSystem.Shared;
using LeonSutedja.BookingSystem.Shared.Handler.Create;
using LeonSutedja.BookingSystem.Shared.Handler.Update;

namespace LeonSutedja.BookingSystem.Services
{
    public class CudAppService : ApplicationService, ICudAppService
    {
        private readonly ICreateHandlerFactory _createHandlerFactory;
        private readonly IUpdateHandlerFactory _updateHandlerFactory;
        private readonly IRepository<Schedule> _scheduleRepository;

        public CudAppService(
            ICreateHandlerFactory createHandlerFactory,
            IUpdateHandlerFactory updateHandlerFactory,
            IRepository<Schedule> scheduleRepository)
        {
            _createHandlerFactory = createHandlerFactory;
            _updateHandlerFactory = updateHandlerFactory;
            _scheduleRepository = scheduleRepository;
        }

        public Schedule GetSchedule(int id)
        {
            return _scheduleRepository.GetAll().Include(s => s.Appointments).First(s => s.Id == id);
        }

        public HandlerResponse CreateRoom(CreateRoomCommand command)
        {
            var handler = _createHandlerFactory.CreateHandler<CreateRoomCommand, Room>();
            var result = handler.Create(command);
            return result;
        }

        public HandlerResponse CreateCustomer(CreateCustomerCommand command)
        {
            var handler = _createHandlerFactory.CreateHandler<CreateCustomerCommand, Customer>();
            var result = handler.Create(command);
            return result;
        }

        public HandlerResponse CreateSchedule(CreateScheduleCommand command)
        {
            var handler = _createHandlerFactory.CreateHandler<CreateScheduleCommand, Schedule>();
            var result = handler.Create(command);
            return result;
        }

        public HandlerResponse CreateAppointment(BookAppointmentOnScheduleCommand command)
        {
            var handler = _updateHandlerFactory.CreateHandler<BookAppointmentOnScheduleCommand, Schedule>();
            var result = handler.Update(command);
            return result;
        }
    }
}
