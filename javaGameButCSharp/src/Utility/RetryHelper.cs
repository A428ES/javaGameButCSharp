namespace JavaGameButCSharp{
    class RetryHelper
    {
        
        private InputOutManager _IO;
        public RetryHelper(InputOutManager IO){
            _IO = IO;
        }

        public void ExecuteWithRetry(Action action, int maxRetries, Action? onRetry=null)
        {
            int attempt = 0;
            string errorString = string.Empty;

            while (attempt < maxRetries) {
                try {
                    action();
                    return;
                } catch (ResourceNotFound e) {
                    errorString = $"Unable to locate the requested system resource: {e.Message}";
                } catch (InvalidInput e){
                    errorString = $"Invalid Input: {e.Message}";
                } finally{
                    attempt++;
                    
                    if(onRetry != null){
                        action = onRetry;
                    }

                    _IO.OutWithSubject("ERROR", errorString);
                }
            }

            throw new ResourceNotFound($"Failed recovering from ResourceNotFound error citing: {errorString}");
        }
    }
}