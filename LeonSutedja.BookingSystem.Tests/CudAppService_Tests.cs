using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LeonSutedja.BookingSystem.Services;
using LeonSutedja.BookingSystem.Services.Commands;
using LeonSutedja.BookingSystem.Shared;
using Shouldly;
using Xunit;

namespace LeonSutedja.BookingSystem.Tests
{
    public class CudAppService_Tests : BookingSystemTestBase
    {
        private readonly ICudAppService _cudAppService;

        public CudAppService_Tests()
        {
            //Creating the class which is tested (SUT - Software Under Test)
            _cudAppService = LocalIocManager.Resolve<ICudAppService>();
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        public static IEnumerable<object[]> ValidCreateCustomerCommand()
        {
            yield return new object[] { "firstName", "lastName", new DateTime(1988, 11, 11), null, null };
            yield return new object[] { "firstName", "lastName", new DateTime(1988, 11, 11), "a@a.com.au", null };
            yield return new object[] { "firstName", "lastName", new DateTime(1988, 11, 11), "a@a.com", null };
        }
        [Theory]
        [MemberData("ValidCreateCustomerCommand")]
        public void Should_Create_New_Customer(
            string firstName,
            string lastName,
            DateTime dob,
            string email,
            string mobileNo)
        {
            //Prepare for test
            var command = new CreateCustomerCommand
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dob,
                Email = email,
                MobileNo = mobileNo
            };

            var result = _cudAppService.CreateCustomer(command);
            result.Id.ShouldBeGreaterThan(0);
        }

        public static IEnumerable<object[]> InvalidCreateCustomerCommand()
        {
            yield return new object[] { null, null, null, null, null };
            yield return new object[] { "firstName", null, new DateTime(1988, 11, 11), null, null };
            yield return new object[] { null, "lastName", new DateTime(1988, 11, 11), null, null };
            yield return new object[] { "12345678901234567890123456789012345678901", "lastName", new DateTime(1988, 11, 11), null, null };
            yield return new object[] { "firstName", "12345678901234567890123456789012345678901", new DateTime(1988, 11, 11), null, null };
            yield return new object[] { "firstName", "lastName", new DateTime(1988, 11, 11), "aaaa", null };
        }
        [Theory]
        [MemberData("InvalidCreateCustomerCommand")]
        public void Should_Not_ModelBind_With_Invalid_CreateCustomerCommand_Model(
            string firstName,
            string lastName,
            DateTime dob,
            string email,
            string mobileNo)
        {
            //Description is not set
            var command = new CreateCustomerCommand
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dob,
                Email = email,
                MobileNo = mobileNo
            };
            Assert.True(ValidateModel(command).Count > 0);
        }

        public static IEnumerable<object[]> ValidCreateRoomCommand()
        {
            yield return new object[] { "Andy", "Andy Locat", "9/9 Milky Way Road, 3150 GGGG"};
            yield return new object[] { "12345678901234567890", "Andy Locat", "9/9 Milky Way Road, 3150 GGGG"};
            yield return new object[] { "Andy", "12345678901234567890123456789012345678901234567890", "9/9 Milky Way Road, 3150 GGGG"};
        }
        [Theory]
        [MemberData("ValidCreateRoomCommand")]
        public void Should_Create_New_Room(
            string shortName,
            string name,
            string location)
        {
            //Prepare for test
            var command = new CreateRoomCommand(shortName, name, location);
            var result = _cudAppService.CreateRoom(command);
            result.Id.ShouldBeGreaterThan(0);
        }

        public static IEnumerable<object[]> InvalidCreateRoomCommand()
        {
            yield return new object[] { "", "Andy Location", "9/9 Milky Way Road, 3150 GGGG" };
            yield return new object[] { "Andy", "", "9/9 Milky Way Road, 3150 GGGG" };
            yield return new object[] { "Andy", "Andy Location", "" };
            yield return new object[] { "", "Andy Location", "" };
            yield return new object[] { "", "", "" };
            yield return new object[] { "012345678901234567891", "Lion", "9/9 Milky Way Road, 3150 GGGG" };
            yield return new object[] { "Andy", "012345678901234567890123456789012345678901234567890123456789", "9/9 Milky Way Road, 3150 GGGG" };
        }
        [Theory]
        [MemberData("InvalidCreateRoomCommand")]
        public void Should_Not_ModelBind_CreateNewRoomCommand(
            string shortName,
            string name,
            string location)
        {
            var command = new CreateRoomCommand(shortName, name, location);
            Assert.True(ValidateModel(command).Count > 0);
        }
        
        public static IEnumerable<object[]> ValidNonoverlapCreateScheduleCommand()
        {
            yield return new object[] {
                new DateTime(2017, 1, 10),
                new DateTime(2017, 1, 10, 8, 0, 0),
                new DateTime(2017, 1, 10, 12, 0, 0),
                new DateTime(2017, 1, 10),
                new DateTime(2017, 1, 10, 13, 0, 0),
                new DateTime(2017, 1, 10, 18, 0, 0)
            };
            yield return new object[] {
                new DateTime(2017, 1, 10),
                new DateTime(2017, 1, 10, 8, 0, 0),
                new DateTime(2017, 1, 10, 12, 0, 0),
                new DateTime(2017, 1, 10),
                new DateTime(2017, 1, 10, 12, 0, 0),
                new DateTime(2017, 1, 10, 18, 0, 0)
            };
            yield return new object[] {
                new DateTime(2017, 1, 10),
                new DateTime(2017, 1, 10, 12, 0, 0),
                new DateTime(2017, 1, 10, 18, 0, 0),
                new DateTime(2017, 1, 10),
                new DateTime(2017, 1, 10, 8, 0, 0),
                new DateTime(2017, 1, 10, 12, 0, 0)
            };
        }
        [Theory]
        [MemberData("ValidNonoverlapCreateScheduleCommand")]
        public void Should_Create_Schedule_NonOverlap(
            DateTime date,
            DateTime startTime,
            DateTime endTime,
            DateTime date2,
            DateTime startTime2,
            DateTime endTime2)
        {
            //Prepare for test
            var command = new CreateScheduleCommand()
            {
                Date = date,
                StartTime = startTime,
                EndTime = endTime,
                RoomId = 1
            };

            var result = _cudAppService.CreateSchedule(command);
            result.Id.ShouldBeGreaterThan(0);

            //Prepare for test
            var overlapCommand = new CreateScheduleCommand()
            {
                Date = date2,
                StartTime = startTime2,
                EndTime = endTime2,
                RoomId = 1
            };
            var overlapResult = _cudAppService.CreateSchedule(overlapCommand);
            overlapResult.Id.ShouldBeGreaterThan(0);
        }

        public static IEnumerable<object[]> InvalidCreateScheduleCommand()
        {
            yield return new object[] {
                new DateTime(2017, 1, 2),
                new DateTime(2017, 1, 2, 8, 0, 0),
                new DateTime(2017, 1, 2, 13, 0, 0),
                new DateTime(2017, 1, 2),
                new DateTime(2017, 1, 2, 8, 0, 0),
                new DateTime(2017, 1, 2, 13, 0, 0)
            };
            yield return new object[] {
                new DateTime(2017, 1, 3),
                new DateTime(2017, 1, 3, 8, 0, 0),
                new DateTime(2017, 1, 3, 18, 0, 0),
                new DateTime(2017, 1, 3),
                new DateTime(2017, 1, 3, 10, 0, 0),
                new DateTime(2017, 1, 3, 17, 0, 0)
            };
            yield return new object[] {
                new DateTime(2017, 1, 3),
                new DateTime(2017, 1, 3, 8, 0, 0),
                new DateTime(2017, 1, 3, 18, 0, 0),
                new DateTime(2017, 1, 3),
                new DateTime(2017, 1, 3, 10, 0, 0),
                new DateTime(2017, 1, 3, 21, 0, 0)
            };
            yield return new object[] {
                new DateTime(2017, 1, 3),
                new DateTime(2017, 1, 3, 8, 0, 0),
                new DateTime(2017, 1, 3, 18, 0, 0),
                new DateTime(2017, 1, 3),
                new DateTime(2017, 1, 3, 7, 0, 0),
                new DateTime(2017, 1, 3, 21, 0, 0)
            };
        }
        [Theory]
        [MemberData("InvalidCreateScheduleCommand")]
        public void Should_Not_Create_Schedule_On_Overlap(
            DateTime date, 
            DateTime startTime, 
            DateTime endTime,
            DateTime date2,
            DateTime startTime2,
            DateTime endTime2)
        {
            //Prepare for test
            var command = new CreateScheduleCommand()
            {
                Date = date,
                StartTime = startTime,
                EndTime = endTime,
                RoomId = 1
            };

            var result = _cudAppService.CreateSchedule(command);
            result.Id.ShouldBeGreaterThan(0);

            //Prepare for test
            var overlapCommand = new CreateScheduleCommand()
            {
                Date = date2,
                StartTime = startTime2,
                EndTime = endTime2,
                RoomId = 1
            };
            var overlapResult = _cudAppService.CreateSchedule(overlapCommand);
            overlapResult.Status.ShouldBe(HandlerResponseStatus.DomainFailure); 
        }

        public static IEnumerable<object[]> ValidCreateScheduleCommand()
        {
            yield return new object[] {
                new DateTime(2017, 1, 2, 8, 0, 0),
                new DateTime(2017, 1, 2, 9, 0, 0),
                1,
                1
            };}
        [Theory]
        [MemberData("ValidCreateScheduleCommand")]
        public void Should_Create_Valid_Appointment(
            DateTime startTime,
            DateTime endTime,
            int customerId,
            int scheduleId)
        {
            //Prepare for test
            var command = new BookAppointmentOnScheduleCommand()
            {
                StartTime = startTime,
                EndTime = endTime,
                CustomerId = customerId,
                Id = scheduleId
            };

            var result = _cudAppService.CreateAppointment(command);
            result.Id.ShouldBeGreaterThan(0);

            var schedule = _cudAppService.GetSchedule(scheduleId);
            schedule.Appointments.Count.ShouldBe(1);
        }
    }
}