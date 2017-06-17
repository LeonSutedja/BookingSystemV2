using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeonSutedja.BookingSystem.Entities
{
    public class Appointment : Abp.Domain.Entities.Entity
    {
        [Required]
        public DateTime StartTime { get; private set; }

        [Required]
        public DateTime EndTime { get; private set; }

        [Required]
        public int CustomerId { get; private set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; private set; }

        [Required]
        public int ScheduleId { get; private set; }

        [ForeignKey("ScheduleId")]
        public virtual Schedule Schedule { get; private set; }

        protected Appointment() { }

        public Appointment(DateTime startTime, DateTime endTime, int customerId, int scheduleId)
        {
            StartTime = startTime;
            EndTime = endTime;
            CustomerId = customerId;
            ScheduleId = scheduleId;
        }
    }
}
