using System;
using Abp.Domain.Entities;

namespace LeonSutedja.BookingSystem.Shared.Handler
{
    public interface IEvent
    {
        string RequestedBy { get; }

        DateTime RequestedDateTime { get; }
    }

    public abstract class Event : IEvent
    {
        public string RequestedBy { get; }

        public DateTime RequestedDateTime { get; }

        protected Event(string requestedBy, DateTime requestedDateTime)
        {
            RequestedBy = requestedBy;
            RequestedDateTime = requestedDateTime;
        }
    }

    public interface ICommand<out T> where T : Entity
    {
        IEvent GetEvent();
    }

    public interface ICreateCommand<out T> : ICommand<T> where T: Entity
    {
    }

    public interface IUpdateCommand<out T> : ICommand<T> where T : Entity
    {
        int Id { get; }
    }

    public interface IDeleteCommand<out T> : ICommand<T> where T : Entity
    {
        int Id { get; }
    }
}