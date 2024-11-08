namespace JavaGameButCSharp{
class RetryHelper
{
    public static void ExecuteWithRetry(Action action, int maxRetries, Action onRetry)
    {
        int attempt = 0;
        string errorString = string.Empty;

        while (attempt < maxRetries)
        {
            try
            {
                action();
                return;
            }
            catch (ResourceNotFound ex)
            {
                errorString = ex.Message;
                action = onRetry;
                attempt++;
            }
        }

        throw new ResourceNotFound($"Failed recovering from ResourceNotFound error citing: {errorString}");
    }
}
}