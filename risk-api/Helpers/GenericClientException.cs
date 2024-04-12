namespace risk_api.Helpers;

public class GenericClientException : SystemException
{
    public readonly int ErrorCode;

    public GenericClientException(int errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}