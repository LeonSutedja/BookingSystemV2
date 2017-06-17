using System;
using System.Collections.Generic;

namespace LeonSutedja.BookingSystem.Shared
{
    public enum HandlerResponseStatus
    {
        Success,
        DomainFailure,
        DatabaseFailure
    }

    public class HandlerResponse
    {
        public static HandlerResponse Success(string message, int successId)
        {
            return new HandlerResponse(message, successId);
        }
        public static HandlerResponse Failed(string message)
        {
            return new HandlerResponse(message);
        }

        public HandlerResponseStatus Status { get; private set; }

        public List<Exception> Exceptions { get; private set; }

        public int Id { get; private set; }

        public string Message { get; private set; }

        private HandlerResponse(string successMessage, int successId)
        {
            Message = successMessage;
            Id = successId;
            Status = HandlerResponseStatus.Success;
        }

        private HandlerResponse(string failureMessage)
        {
            Message = failureMessage;
            Status = HandlerResponseStatus.DomainFailure;
        }
    }
}
