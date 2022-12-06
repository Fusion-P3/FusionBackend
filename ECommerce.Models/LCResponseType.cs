namespace ECommerce.Models;

public class LCResponseType
{
    public MatchedUser matchedUser { get; set; } = new();
}

public class MatchedUser
{
    public string username { get; set; } = string.Empty;
    public SubmitStatsType submitStats { get; set; } = new();

}

public class SubmitStatsType
{
    public ACSubmissionNumType[] aCSubmissionNum { get; set; } = new ACSubmissionNumType[] { };
}

public class ACSubmissionNumType
{
    public string difficulty { get; set; } = string.Empty;
    public int count { get; set; }
    public int submissions { get; set; }
}

