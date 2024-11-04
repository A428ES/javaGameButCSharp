namespace JavaGameButCSharp{
    class InputOutManager{
        private readonly InputControl _inputControl;
        private string _lastUserInput = string.Empty;
        public string LastUserInput => _lastUserInput;

        public InputOutManager(){
            _inputControl = new InputControl();
        }

        private string GetUserFeed(){
            return _inputControl.GetInput();
        }

        private void SegmentDivider(){
            Console.WriteLine("===============================");
        }

        private string StringListBuilder(List<String> stringList){
            return string.Join("\n", stringList.Select(item => $"- {item}"));
        }

        private void Out(string payload){
            SegmentDivider();
            Console.WriteLine(payload);
            SegmentDivider();
        }


        public void OutWithSubject(string subject, string content){
            Out(subject + ": " + content);
        }

        public void OutWithPrompt(string content, string prompt){
            Out(content);

            Console.WriteLine($"{prompt}: ");

            _lastUserInput = _inputControl.ValidateForNonOptionInput(GetUserFeed());
        }

        public void OutWithOptionsPrompt(string message, List<string> options){

            Out($"{message}\n\n{StringListBuilder(options)}");

            Console.WriteLine("ENTER OPTION: ");

            _lastUserInput = _inputControl.ValidateAgainstOptions(GetUserFeed(),options);
        }

        public void OutWithMultipleMessages(List<string> messages){
            Out(StringListBuilder(messages));
        }

    }
}