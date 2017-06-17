using Abp.Application.Services;
using LeonSutedja.BookingSystem.Entities;
using LeonSutedja.BookingSystem.Services.Commands;
using LeonSutedja.BookingSystem.Shared;

namespace LeonSutedja.BookingSystem.Services
{
    public interface ICudAppService : IApplicationService
    {
        HandlerResponse CreateCustomer(CreateCustomerCommand command);

        HandlerResponse CreateRoom(CreateRoomCommand command);

        HandlerResponse CreateSchedule(CreateScheduleCommand command);

        HandlerResponse CreateAppointment(BookAppointmentOnScheduleCommand command);

        Schedule GetSchedule(int id);
    }
}