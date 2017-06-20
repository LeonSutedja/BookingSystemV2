using System;
using System.ComponentModel.DataAnnotations;
using LeonSutedja.BookingSystem.Entities;
using LeonSutedja.BookingSystem.Shared.Handler;
using LeonSutedja.BookingSystem.Shared.Handler.Create;

namespace LeonSutedja.BookingSystem.Services.Commands
{
    public class CreateCustomerCommand : ICreateCommand<Customer>
    {
        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string MobileNo { get; set; }

        public IEvent GetEvent()
        {
            return new CustomerCreated(this);
        }
    }

    public class CustomerCreated : IEvent
    {
        public CustomerCreated(CreateCustomerCommand command)
        {
            FirstName = command.FirstName;
            LastName = command.LastName;
            DateOfBirth = command.DateOfBirth;
            Email = command.Email;
            MobileNo = command.MobileNo;
        }

        [Required]
        [StringLength(40)]
        public string FirstName { get; private set; }

        [Required]
        [StringLength(40)]
        public string LastName { get; private set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; private set; }

        [EmailAddress]
        [StringLength(50)]
        public string Email { get; private set; }

        [MaxLength(50)]
        public string MobileNo { get; private set; }

        public string RequestedBy => throw new NotImplementedException();

        public DateTime RequestedDateTime => throw new NotImplementedException();
    }

    public class CreateCustomerCommandMapper : ICreateCommandMapper<CreateCustomerCommand, Customer>
    {
        public Customer Create(CreateCustomerCommand command)
        {
            var customer = new Customer(command.FirstName, command.LastName, command.DateOfBirth);
            customer.SetEmail(command.Email);
            customer.SetMobileNo(command.MobileNo);
            return customer;
        }
    }
}