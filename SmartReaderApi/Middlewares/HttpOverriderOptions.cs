using System.Collections.Generic;

namespace SmartReaderApi.Middlewares
{
    public class HttpOverriderOptions
    {
        public string HeaderName { get; set; }
        public string[] AllowedMethods { get; set; }
    }
}