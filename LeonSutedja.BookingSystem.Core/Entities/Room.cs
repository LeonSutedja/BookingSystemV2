using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace LeonSutedja.BookingSystem.Entities
{
    public class Room : Entity
    {
        [Required]
        [MaxLength(20)]
        public string ShortName { get; private set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; private set; }

        protected Room() { }

        public Room(string shortName, string name, string location)
        {
            ShortName = shortName;
            Name = name;
            Location = location;
        }
    }
}