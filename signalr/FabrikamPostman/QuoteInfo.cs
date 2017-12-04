public class QuoteInfo
{
    public string Actor{get;set;}
    public string Game { get; set; }
    public string Quote { get; set; }

    public override string ToString()
    {
        return $"{Quote}-({Actor} from '{Game}')";
    }
}