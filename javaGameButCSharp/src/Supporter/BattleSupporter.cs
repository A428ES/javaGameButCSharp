using static JavaGameButCSharp.OptionMap;
namespace JavaGameButCSharp{
    class BattleSupporter(SupporterContext supporterContext) : Supporter(supporterContext){
        List<string> BattleMessages = [];
        public override Dictionary<String, String> StageKeywordReplace(){
            return new Dictionary<string, string>
                    {
                        {"{target}", _supporterContext.GameState.ActiveTargetNPC.Name},
                        {"{target_weapon}", _supporterContext.GameState.ActiveTargetNPC.Inventory.ActiveWeapon}
                    };
        }

        public int DamageProcessing(Entity[] attackOrder){
            int damageOutcome = GameMath.DamageCalculation(attackOrder[0], attackOrder[1]);

            if(damageOutcome < 0){

                attackOrder[0].Health += damageOutcome;
                BattleMessages.Add($"{attackOrder[0].Name} took {damageOutcome} recoil damage.");
            } else {
                attackOrder[1].Health -= damageOutcome;
                BattleMessages.Add($"{attackOrder[1].Name} took {damageOutcome} battle damage.");
            }

            _supporterContext.GameState.SaveAllStates();

            return damageOutcome;      
        }

        public void BattleOutput(Entity[] attackOrder, int damage){
            GameStateController gameState = _supporterContext.GameState;

            CheckBattleOutcome();

            BattleMessages.Add($"YOUR HEALTH: {gameState.ActivePlayer.Health.ToString()}");
            BattleMessages.Add($"{gameState.ActiveTargetNPC.Name} HEALTH: {gameState.ActiveTargetNPC.Health.ToString()}");
            
            _supporterContext.IO.OutWithMultipleMessages(BattleMessages);

            BattleMessages = [];
        }

        public void EndBattleEvent(string message){
            _supporterContext.ToggleBattleOff();
            _supporterContext.SystemEvent = new(LOCATION_EVENT, _supporterContext.GameState.ActivePlayer.Location);
            BattleMessages.Add(message);
        }

        public void NPCDefeated(){
            _supporterContext.GameState.ActiveLocation.NpcList.Remove(_supporterContext.GameState.ActiveTargetNPC.Name);

            EndBattleEvent($"YOU DEFEATED {_supporterContext.GameState.ActiveTargetNPC.Name}");
        }

        public void PlayerDefeated(){
            _supporterContext.GameState.ActivePlayer.Health = 0;
            _supporterContext.GameState.ActiveTargetNPC.Health = 100;

            EndBattleEvent($"YOU WERE DEFEATED BY {_supporterContext.GameState.ActiveTargetNPC.Name}");
        }

        public void CheckBattleOutcome(){
            if(_supporterContext.GameState.ActivePlayer.Health <= 0){
                PlayerDefeated();
            } 
            
            if(_supporterContext.GameState.ActiveTargetNPC.Health <= 0){
                NPCDefeated();
            }
        }

        public void Attack(){
            Entity[] attackOrder = GameMath.AttackOrder(_supporterContext.GameState.ActivePlayer, _supporterContext.GameState.ActiveTargetNPC);
    
            BattleOutput(attackOrder, DamageProcessing(attackOrder));
            Array.Reverse(attackOrder);

            if(!_supporterContext.GameState.InBattle){
                return;
            }

            BattleOutput(attackOrder,  DamageProcessing(attackOrder));

            _supporterContext.GameState.SaveAllStates();
        }

        public void Escape(){
            EndBattleEvent("You escaped successfully");
        }

        public override List<string> FinalOptionsProcessing()
        {
            return GlobalMenuOptions(["RESUME"]);
        }

        public override Dictionary<OptionMap, Action> MapRoute(){
            return new Dictionary<OptionMap, Action>
                {
                    {ATTACK, () => Attack()},
                    {ESCAPE, () => Escape()}
                };
        }
    }
}