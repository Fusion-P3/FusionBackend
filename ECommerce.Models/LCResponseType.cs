namespace ECommerce.Models;

public class LCResponseType
{
    public MatchedUser matchedUser { get; set; }
}

public class MatchedUser
{
    public string username { get; set; }
    public SubmitStatsType submitStats { get; set; }

}

public class SubmitStatsType
{
    public ACSubmissionNumType[] aCSubmissionNum { get; set; }
}

public class ACSubmissionNumType
{
    public string difficulty { get; set; }
    public int count { get; set; }
    public int submissions { get; set; }
}

