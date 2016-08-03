namespace Izusoft.IdendityCardVerifier
{
    public interface ICardVerifier
    {
        bool TryVerify(string idendityCard);
    }
}