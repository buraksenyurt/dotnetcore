using System.Collections.Generic;

public class QuoteFabric
{
    public static List<QuoteInfo> GetQuotes()
    {
        List<QuoteInfo> quotes=new List<QuoteInfo>{
                new QuoteInfo{
                    Actor="Andrew Ryan",
                    Game="Bioshock",
                    Quote="We all make choices in life, but in the end our choices make us.",
                },
                new QuoteInfo{
                    Actor="Scorpion",
                    Game="Mortal Kombat",
                    Quote="Get over here!",
                },
                new QuoteInfo{
                    Actor="G-Man",
                    Game="Half-life 2",
                    Quote="The right man in the wrong place can make all the difference in the world."
                },
                new QuoteInfo{
                    Actor="Javik",
                    Game="Mass Effect 3",
                    Quote="Stand in the ashes of a trillion dead souls, and asks the ghosts if honor matters. The silence is your answer."
                },
                new QuoteInfo{
                    Actor="Psycho",
                    Game="Borderlands 2",
                    Quote="Bring me a bucket, and I'll show you a bucket!"
                },
                new QuoteInfo{
                    Actor="Ezio Auditore",
                    Game="Assassin’s Creed 2",
                    Quote="Wanting something does not give you the right to have it.",
                },
                new QuoteInfo{
                    Actor="Khan",
                    Game="Metro 2033",
                    Quote="Even in dark times, we cannot relinquish the things that make us human.",
                },
                new QuoteInfo{
                    Actor="Halo",
                    Game="Halo",
                    Quote="A hero need not speak. When he is gone, the world will speak for him.",
                },
                new QuoteInfo{
                    Actor="Duke Nukem",
                    Game="Duke Nukem 3D",
                    Quote="It's time to kick ass and chew bubblegum... and I'm all outta gum." 
                }
            };
        return quotes;
    }
}