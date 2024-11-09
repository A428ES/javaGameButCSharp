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
                } catch (ResourceNotFound) {
                    errorString = "Could not find requested resource";
                } catch (InvalidInput){
                    errorString = "Repeated invalid entry";
                } finally{
                    attempt++;
                    
                    if(onRetry != null){
                        action = onRetry;
                    }
                }
            }

            throw new ResourceNotFound($"Failed recovering from ResourceNotFound error citing: {errorString}");
        }
    }
}