using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace LeonSutedja.BookingSystem.Entities
{
    public class Schedule : Entity
    {
        public static Schedule CreateScheduleRoom(Room room, DateTime date, DateTime startTime, DateTime endTime)
        {
            return new Schedule(room.Id, date, startTime, endTime);
        }

        [Required]
        public DateTime Date { get; private set; }

        [Required]
        public DateTime StartTime { get; private set; }

        [Required]
        public DateTime EndTime { get; private set; }

        [Required]
        public int RoomId { get; private set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; private set; }

        public bool IsFull { get; private set; }

        public virtual ICollection<Appointment> Appointments { get; private set; }

        protected Schedule()
        {
            Appointments = new List<Appointment>();
        }

        public Schedule(int roomId, DateTime date, DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime) throw new Exception("Invalid Time");
            
            RoomId = roomId;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }

        public void AddAppointment(Customer customer, DateTime startTime, DateTime endTime)
        {
            var newAppointment = new Appointment(startTime, endTime, customer.Id, Id);
            Appointments.Add(newAppointment);
        }
    }
}