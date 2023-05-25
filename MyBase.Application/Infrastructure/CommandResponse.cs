namespace MyBase.Application.Infrastructure
{
    public class CommandResponse
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }
    public class JsonRespone<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
    }

    public class GenaricResponse<IRequest>
    {
        public string Message { get; set; }
        public IRequest Data { get; set; }
        public int StatusCode { get; set; }
    }

    public class CommandProfileReturn
    {
        public ManagerCommandProfileReturn ManagerCommandProfile { get; set; }
        public SupervisorCommandProfileReturn SupervisorCommandProfile { get; set; }
    }

    public class ManagerCommandProfileReturn
    {
        public bool? AddNewProfile { get; set; }
        public int? NewProfileId { get; set; }
        public string NewUserId { get; set; }
        public bool? RemoveOldProfile { get; set; }
        public int? OldProfileId { get; set; }
        public string OldUserId { get; set; }
    }

    public class SupervisorCommandProfileReturn
    {
        public bool? AddNewProfile { get; set; }
        public int? NewProfileId { get; set; }
        public string NewUserId { get; set; }
        public bool? RemoveOldProfile { get; set; }
        public int? OldProfileId { get; set; }
        public string OldUserId { get; set; }
    }
}
