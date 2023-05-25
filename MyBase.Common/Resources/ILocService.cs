using Microsoft.Extensions.Localization;

namespace MyBase.Common
{
    public interface ILocService
    {
        public LocalizedString GetLocalizedString(string key);
        public string Back();
        public string Add(string key);
        public string Exit(string key);
        public IStringLocalizer GetLocalizer();
    }
}
