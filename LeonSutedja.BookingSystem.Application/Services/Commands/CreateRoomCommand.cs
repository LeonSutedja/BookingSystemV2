using System.ComponentModel.DataAnnotations;
using LeonSutedja.BookingSystem.Entities;
using LeonSutedja.BookingSystem.Shared.Handler;
using LeonSutedja.BookingSystem.Shared.Handler.Create;
using System;

namespace LeonSutedja.BookingSystem.Services.Commands
{
    public class CreateRoomCommand : ICreateCommand<Room>
    {
        [Required]
        [StringLength(20)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        public IEvent GetEvent(string triggeredBy, DateTime triggeredDateTime)
        {
            return new RoomCreated(this, triggeredBy, triggeredDateTime);
        }

        private class RoomCreated : Event
        {
            public RoomCreated(CreateRoomCommand cmd, string triggeredBy, DateTime triggeredDateTime)
                : base(Guid.NewGuid(), triggeredBy, triggeredDateTime)
            {
                ShortName = cmd.ShortName;
                Name = cmd.Name;
                Location = cmd.Location;
            }

            [Required]
            [StringLength(20)]
            public string ShortName { get; private set; }

            [Required]
            [StringLength(50)]
            public string Name { get; private set; }

            [Required]
            [StringLength(200)]
            public string Location { get; private set; }
        }
    }

    public class CreateRoomCommandMapper : ICreateCommandMapper<CreateRoomCommand, Room>
    {
        public Room Create(CreateRoomCommand command)
        {
            return new Room(command.ShortName, command.Name, command.Location);
        }
    }
}