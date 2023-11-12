public enum SponsorshipType
{
    Exclusive,
    NonExclusive
}

[System.Serializable]
public class SponsorshipDeal
{
    public SponsorshipType Type;
    public float Duration;
    public int CashAmount;
    public float Timer;
    public int CashDistributed; // Track how much cash has already been distributed
    public float LastDistributionTime;
}
