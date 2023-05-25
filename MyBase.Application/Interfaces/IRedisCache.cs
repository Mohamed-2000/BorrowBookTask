namespace MyBase.Application.Interfaces
{
    public interface IRedisCache
    {
        Task SetPermissionsData(object data);
        Task<T> GetPermissionsData<T>();
        Task SetReportData(object data, string cachekey);
        Task<T> GetReportData<T>(string cachekey);
        Task  RemoveReportCache(string cachekey);
        Task SetSignalRToken(string data, string cachekey);
        Task<string> GetSignalRToken(string cachekey);

    }
}