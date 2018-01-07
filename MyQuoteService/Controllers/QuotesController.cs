using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyQuoteService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var id = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
            var email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            var blog = User.FindFirst(c => c.Type == "urn:github:blog")?.Value;
            Console.WriteLine($"{DateTime.Now}\nCurrent user:{userName}({id})\n{email}\n{blog}");
            return new ObjectResult(_quotes);
        }

        List<Quote> _quotes = new List<Quote>{
                new Quote{Id=122548,Owner="Michael Jordan",Text="I have missed more than 9000 shots in my career. I have lost almost 300 games. 26 times, I have been trusted to take the game winning shot and missed. I have failed over and over and over again in my life. And that is why I succeed."},
                new Quote{Id=325440,Owner="Vince Lombardi",Text="We didn't lose the game; we just ran out of time"},
                new Quote{Id=150094,Owner="Randy Pausch",Text="We cannot change the cards we are dealt, just how we play the game"},
                new Quote{Id=167008,Owner="Johan Cruyff",Text="Football is a game of mistakes. Whoever makes the fewest mistakes wins."},
                new Quote{Id=650922,Owner="Gary Lineker",Text="Football is a simple game. Twenty-two men chase a ball for 90 minutes and at the end, the Germans always win."},
                new Quote{Id=682356,Owner="Paul Pierce",Text="The game isn't over till the clock says zero."},
                new Quote{Id=156480,Owner="Jose Mourinho",Text="Football is a game about feelings and intelligence."},
                new Quote{Id=777592,Owner="LeBron James",Text="You know, when I have a bad game, it continues to humble me and know that, you know, you still have work to do and you still have a lot of people to impress."},
                new Quote{Id=283941,Owner="Roman Abramovich",Text="I'm getting excited before every single game. The trophy at the end is less important than the process itself."},
                new Quote{Id=185674,Owner="Shaquille O'Neal",Text="I'm tired of hearing about money, money, money, money, money. I just want to play the game, drink Pepsi, wear Reebok."}
            };
    }

    class Quote
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Text { get; set; }
    }
}