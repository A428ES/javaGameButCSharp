namespace JavaGameButCSharp{
    class DisplayContext(GameRenderer gameRenderer)
    {
        private Sprite _playerSprite;
        private List<Sprite> _npcSprites;
        public GameRenderer Renderer { get; } = gameRenderer;

        public void SpawnPlayer(){
            if(PlayerLoaded()){
                return;
            }

            _playerSprite = Renderer.RenderPlayerSprite(@"C:\Walk.PNG");
            _npcSprites = new();
        }

        public void LoadSprite(Sprite newSprite){
            _npcSprites.Add(newSprite);
        }

        public bool PlayerLoaded(){
            return _playerSprite != null;
        }

        public void DestroyPlayer(){
            if(!PlayerLoaded()){
                return ;
            }

            Renderer.DestroySprite(_playerSprite);
            _playerSprite = null;
        }

        public void UpdateMenu(Dictionary<OptionMap, Action> menuMap){
            Renderer.GameMenu.RefreshMenuStack("MENU", menuMap);
        }
    }
}