namespace JavaGameButCSharp{
    class InputControl{
        public string GetInput(){
            string promptRead =  Console.ReadLine() ?? string.Empty;

            return promptRead;
        }

        public string ValidateAgainstOptions(string entry, List<String> options){
            if(!options.Contains(entry.ToUpper())){
                return string.Empty;
            }

            return entry;
        }

        public string ValidateForNonOptionInput(string entry){
            if(entry.Equals("test")){
                return string.Empty;
            }

            return entry;
        }
    }
}