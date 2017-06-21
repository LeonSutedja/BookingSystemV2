using System;
using Abp.Domain.Entities;

namespace LeonSutedja.BookingSystem.Shared.Handler
{
    public interface IEvent
    {
        Guid Id { get; }

        string TriggeredBy { get; }

        DateTime TriggeredDateTime { get; }
    }

    public abstract class Event : IEvent
    {
        public Guid Id { get; }

        public string TriggeredBy { get; }

        public DateTime TriggeredDateTime { get; }

        protected Event(Guid id, string triggeredBy, DateTime triggeredDateTime)
        {
            Id = id;
            TriggeredBy = triggeredBy;
            TriggeredDateTime = triggeredDateTime;
        }
    }

    public interface ICommand<out T> where T : Entity
    {
        IEvent GetEvent(string triggeredBy, DateTime triggeredDateTime);
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