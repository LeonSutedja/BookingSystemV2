using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using Abp.Domain.Repositories;
using LeonSutedja.BookingSystem.Entities;
using LeonSutedja.BookingSystem.Shared;
using LeonSutedja.BookingSystem.Shared.Handler;
using LeonSutedja.BookingSystem.Shared.Handler.Create;
using LeonSutedja.BookingSystem.Shared.Handler.Validation;

namespace LeonSutedja.BookingSystem.Services.Commands
{
    public class CreateScheduleCommand : ICreateCommand<Schedule>
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int RoomId { get; set; }

        public IEvent GetEvent()
        {
            return new ScheduleCreated(this);
        }
    }

    public class ScheduleCreated : IEvent
    {
        public ScheduleCreated(CreateScheduleCommand cmd)
        {
            Date = cmd.Date;
            StartTime = cmd.StartTime;
            EndTime = cmd.EndTime;
            RoomId = cmd.RoomId;
        }

        [Required]
        public DateTime Date { get; private set; }

        [Required]
        public DateTime StartTime { get; private set; }

        [Required]
        public DateTime EndTime { get; private set; }

        [Required]
        public int RoomId { get; private set; }
    }

    public class CreateScheduleCommandMapper : ICreateCommandMapper<CreateScheduleCommand, Schedule>
    {
        private readonly IRepository<Room> _roomRepository;

        public CreateScheduleCommandMapper(IRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Schedule Create(CreateScheduleCommand command)
        {
            var room = _roomRepository.Get(command.RoomId);
            return Schedule.CreateScheduleRoom(room, command.Date, command.StartTime, command.EndTime);
        }
    }

    public class RoomMuseNotHaveOtherSchedule : IBusinessRule<CreateScheduleCommand, Schedule>
    {
        private readonly IRepository<Schedule> _scheduleRepository;

        public RoomMuseNotHaveOtherSchedule(IRepository<Schedule> scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public ValidationCommandResult IsValid(CreateScheduleCommand createCommand)
        {
            var allScheduleOnTheDay = _scheduleRepository
                .GetAll()
                .FirstOrDefault(s => 
                    DbFunctions.TruncateTime(s.Date) == 
                    DbFunctions.TruncateTime(createCommand.Date));
            if (allScheduleOnTheDay == null) return ValidationCommandResult.Valid();
            // inside scenario
            var scheduleTimeRange = new TimeRange(allScheduleOnTheDay.StartTime, allScheduleOnTheDay.EndTime);
            var commandTimeRange = new TimeRange(createCommand.StartTime, createCommand.EndTime);
            if (scheduleTimeRange.IsOverlap(commandTimeRange)) return ValidationCommandResult.NotValid("Invalid Time Range");
            return ValidationCommandResult.Valid();
        }
    }
}