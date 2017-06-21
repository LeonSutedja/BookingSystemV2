using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace LeonSutedja.BookingSystem.Entities
{
    public class Customer : Abp.Domain.Entities.Entity
    {
        [Required]
        [MaxLength(40)]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; private set; }

        [Required]
        public DateTime DateOfBirth { get; private set; }

        [MaxLength(100)]
        public string Email { get; private set; }

        [MaxLength(20)]
        public string MobileNo { get; private set; }

        // EF purpose only
        protected Customer() { }

        public Customer(string firstName, string lastName, DateTime dob)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
        }

        public void SetMobileNo(string mobileNo)
        {
            if (string.IsNullOrEmpty(mobileNo)) return;
            if (mobileNo.Length > 20)
            {
                throw new Exception("Mobile No Length is too long.");
            }
            MobileNo = mobileNo;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return;
            if (email.Length > 100)
            {
                throw new Exception("Email Length is too long.");
            }
            Email = email;
        }
    }
}
