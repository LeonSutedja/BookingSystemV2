using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LeonSutedja.BookingSystem.Entities;

namespace LeonSutedja.BookingSystem.Services
{
    [AutoMapFrom(typeof(Schedule))]
    public class ScheduleDto : EntityDto
    {
        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int RoomId { get; set; }

        public string RoomShortName { get; set; }

        public string RoomName { get; set; }

        public string RoomLocation { get; set; }
    }
}
