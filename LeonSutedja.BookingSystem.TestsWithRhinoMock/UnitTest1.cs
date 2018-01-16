using Abp.Domain.Repositories;
using LeonSutedja.BookingSystem.Entities;
using LeonSutedja.BookingSystem.Services.Commands;
using LeonSutedja.BookingSystem.Shared.Handler.Create;
using LeonSutedja.BookingSystem.Shared.Handler.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeonSutedja.BookingSystem.TestsWithRhinoMock
{
    /* Mock extensions to capture argument from Jimmy Bogard blog */
    /* https://lostechies.com/jimmybogard/2010/06/09/capturing-rhino-mocks-arguments-in-c-4-0/ */
    public static class MockExtensions
    {
        public static CaptureExpression<T> Capture<T>(this T stub)
            where T : class
        {
            return new CaptureExpression<T>(stub);
        }
    }

    public class CaptureExpression<T>
    where T : class
    {
        private readonly T _stub;

        public CaptureExpression(T stub)
        {
            _stub = stub;
        }

        public IList<U> Args<U>(Action<T, U> methodExpression)
        {
            var argsCaptured = new List<U>();

            Action<U> captureArg = argsCaptured.Add;
            Action<T> stubArg = stub => methodExpression(stub, default(U));

            _stub.Stub(stubArg).IgnoreArguments().Do(captureArg);

            return argsCaptured;
        }

        public IList<Tuple<U1, U2>> Args<U1, U2>(Action<T, U1, U2> methodExpression)
        {
            var argsCaptured = new List<Tuple<U1, U2>>();

            Action<U1, U2> captureArg = (u1, u2) => argsCaptured.Add(Tuple.Create(u1, u2));
            Action<T> stubArg = stub => methodExpression(stub, default(U1), default(U2));

            _stub.Stub(stubArg).IgnoreArguments().Do(captureArg);

            return argsCaptured;
        }
    }

    [TestClass]
    public class UnitTest1
    { 
        [TestMethod]
        public void ValidCreateRoomCommand_ShouldCallInsertAndGetId()
        {
            // ARRANGE
            var createHandlerFactory = MockRepository.GenerateStub<ICreateHandlerFactory>();
            var repositoryRoomMock = MockRepository.GenerateStub<IRepository<Room>>();
            var mapperRepositoryMock = MockRepository.GenerateStub<ICreateCommandMapperFactory>();
            mapperRepositoryMock.Stub(repo => repo.Create<CreateRoomCommand, Room>()).Return(new CreateRoomCommandMapper());
            var newGenericCreateHandler = new GenericCreateHandler<CreateRoomCommand, Room>(new List<IBusinessRule<CreateRoomCommand, Room>>(), repositoryRoomMock, mapperRepositoryMock);
            createHandlerFactory.Stub(repo => repo.CreateHandler<CreateRoomCommand, Room>()).Return(newGenericCreateHandler);

            var createRoomCommand = new CreateRoomCommand("Room1", "ROOM2", "Short Location");
            var handler = createHandlerFactory.CreateHandler<CreateRoomCommand, Room>();
                       
            var response = handler.Create(createRoomCommand);
            repositoryRoomMock.AssertWasCalled(repo => repo.InsertAndGetId(Arg<Room>.Is.Anything));
            response.Status.ShouldBe(Shared.HandlerResponseStatus.Success);
        }

        [TestMethod]
        public void ValidCreateCustomerCommand_ShouldCallInsertAndGetId()
        {
            // ARRANGE
            var createHandlerFactory = MockRepository.GenerateStub<ICreateHandlerFactory>();
            var repositoryMock = MockRepository.GenerateStub<IRepository<Customer>>();
            var mapperRepositoryMock = MockRepository.GenerateStub<ICreateCommandMapperFactory>();
            mapperRepositoryMock.Stub(repo => repo.Create<CreateCustomerCommand, Customer>()).Return(new CreateCustomerCommandMapper());
            var genericCreateHandler = new GenericCreateHandler<CreateCustomerCommand, Customer>(new List<IBusinessRule<CreateCustomerCommand, Customer>>(), repositoryMock, mapperRepositoryMock);
            createHandlerFactory.Stub(repo => repo.CreateHandler<CreateCustomerCommand, Customer>()).Return(genericCreateHandler);

            var command = new CreateCustomerCommand() { FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now, Email = "John.Smith@gmail.com", MobileNo = "0491 184 123" };
            var handler = createHandlerFactory.CreateHandler<CreateCustomerCommand, Customer>();

            var response = handler.Create(command);
            repositoryMock.AssertWasCalled(repo => repo.InsertAndGetId(Arg<Customer>.Is.Anything));
            response.Status.ShouldBe(Shared.HandlerResponseStatus.Success);
        }

        [TestMethod]
        public void ValidCreateScheduleCommand_ShouldCallInsertAndGetId()
        {
            // ARRANGE
            var createHandlerFactory = MockRepository.GenerateStub<ICreateHandlerFactory>();
            var repositoryMock = MockRepository.GenerateStub<IRepository<Schedule>>();
            var roomRepository = MockRepository.GenerateStub<IRepository<Room>>();
            var roomToBeScheduled = new Room("Room1", "Du Point", "Final Frontier");
            roomRepository.Stub(repo => repo.Get(Arg<int>.Is.Anything)).Return(roomToBeScheduled);
            var mapperRepositoryMock = MockRepository.GenerateStub<ICreateCommandMapperFactory>();

            repositoryMock.Stub(repo => repo.GetAll()).Return(new List<Schedule>().AsQueryable());
            var businessRules = new List<IBusinessRule<CreateScheduleCommand, Schedule>>();
            businessRules.Add(new RoomMuseNotHaveOtherSchedule(repositoryMock));
            mapperRepositoryMock.Stub(repo => repo.Create<CreateScheduleCommand, Schedule>()).Return(new CreateScheduleCommandMapper(roomRepository));
            var genericCreateHandler = new GenericCreateHandler<CreateScheduleCommand, Schedule>(new List<IBusinessRule<CreateScheduleCommand, Schedule>>(), repositoryMock, mapperRepositoryMock);
            createHandlerFactory.Stub(repo => repo.CreateHandler<CreateScheduleCommand, Schedule>()).Return(genericCreateHandler);

            var command = new CreateScheduleCommand() { Date = DateTime.Today, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(45), RoomId = 1 };
            var handler = createHandlerFactory.CreateHandler<CreateScheduleCommand, Schedule>();

            var response = handler.Create(command);
            repositoryMock.AssertWasCalled(repo => repo.InsertAndGetId(Arg<Schedule>.Is.Anything));
            response.Status.ShouldBe(Shared.HandlerResponseStatus.Success);
        }
    }
}
