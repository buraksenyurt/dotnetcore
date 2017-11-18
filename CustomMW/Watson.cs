using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

public class Watson
{
    private readonly RequestDelegate _nextMiddleWare;
    private readonly WatsonOptions _options;

    public Watson(RequestDelegate next, IOptions<WatsonOptions> options)
    {
        _nextMiddleWare = next;
        _options = options.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        var request=context.Request;
        if(request.Method=="POST")
        {
            var contentLength=request.ContentLength;
            Console.WriteLine("[{0}]:{1}-{2}",DateTime.Now.ToLongTimeString(),request.Method,request.Path);
            
            if(contentLength>_options.MaxSizeForPostContent)
            {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("POST size limit violation : {0} bytes\nLimit->{1}",contentLength,_options.MaxSizeForPostContent);
                Console.ForegroundColor=ConsoleColor.White;
                //TODO Something
            }
            else
            {
                Console.ForegroundColor=ConsoleColor.Green;
                Console.WriteLine("Length is OK ({0})",contentLength);
                Console.ForegroundColor=ConsoleColor.White;
            }
        }

        await _nextMiddleWare(context);                  
    }  
}