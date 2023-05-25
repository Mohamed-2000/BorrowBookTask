using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBase.Common
{
    public class StringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            return new StringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new StringLocalizer();
        }
    }
}
