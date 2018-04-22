using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace SmartReaderApi.Middlewares
{
    public class HttpOverrider
    {
        RequestDelegate _nextRequest;
        string _headerName;
        List<string> _allowedHttpMethods;

        public HttpOverrider(RequestDelegate nextRequest, IOptions<HttpOverriderOptions> options)
        {
            _nextRequest = nextRequest ?? throw new ArgumentNullException(nameof(nextRequest));

            if (options?.Value == null)
                throw new ArgumentNullException(nameof(options));

            _headerName = options.Value.HeaderName;
            _allowedHttpMethods = new List<string>();
            foreach (string allowedMethod in options.Value.AllowedMethods)
            {
                _allowedHttpMethods.Add(allowedMethod);
            }
        }
        
        public Task Invoke(HttpContext context)
        {
            if (HttpMethods.IsPost(context.Request.Method))
            {
                if (context.Request.Headers.ContainsKey(_headerName))
                {
                    string xHttpValue = context.Request.Headers[_headerName];
                    if (_allowedHttpMethods.Contains(xHttpValue))
                    {
                        var httpRequestFeature = context.Features.Get<IHttpRequestFeature>();
                        httpRequestFeature.Method = xHttpValue;
                    }
                }
            }
            return _nextRequest(context);
        }
    }
}