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
}
