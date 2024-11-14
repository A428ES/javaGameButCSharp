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

            while (attempt < maxRetries) {
                try {
                    action();
                    return;
                } catch (ResourceNotFound e) {
                    _IO.CentralErrorOutput($"Unable to locate the requested system resource: {e.Message}");
                } catch (InvalidInput e){
                    _IO.CentralErrorOutput($"Invalid Input: {e.Message}");
                } finally{
                    attempt++;
                    
                    if(onRetry != null){
                        action = onRetry;
                    }
                }
            }
        }
    }
}