using static JavaGameButCSharp.OptionMap;
namespace JavaGameButCSharp{
    class BattleSupporter(SupporterContext supporterContext) : Supporter(supporterContext){
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
                attackOrder[1].Health += damageOutcome;
            } else {
                attackOrder[0].Health -= damageOutcome;
            }

            _supporterContext.GameState.SaveAllStates();

            return damageOutcome;      
        }

        public void BattleOutput(Entity[] attackOrder, int damage){
            GameStateController gameState = _supporterContext.GameState;

            List<string> messages =
            [
                $"{attackOrder[0].Name} INFLICTED {damage} DAMAGE",
                $"YOUR HEALTH: {gameState.ActivePlayer.Health.ToString()}",
                $"{gameState.ActiveTargetNPC.Name} HEALTH: {gameState.ActiveTargetNPC.Health.ToString()}",
            ];
            
            _supporterContext.IO.OutWithMultipleMessages(messages);
        }

        public void EndBattleEvent(string message){
            _supporterContext.IO.OutWithSubject("ITS OVER", message);
            _supporterContext.SystemEvent = new(LOCATION_EVENT, _supporterContext.GameState.ActivePlayer.Location);
        }

        public void NPCDefeated(){
            _supporterContext.GameState.ActiveLocation.NpcList.Remove(_supporterContext.GameState.ActiveTargetNPC.Name);

            string message = $"YOU DEFEATED {_supporterContext.GameState.ActiveTargetNPC.Name}";
            EndBattleEvent(message);
        }

        public void PlayerDefeated(){
            _supporterContext.GameState.ActivePlayer.Health = 0;
            _supporterContext.GameState.ActiveTargetNPC.Health = 100;

            string message = $"YOU WERE DEFEATED BY {_supporterContext.GameState.ActiveTargetNPC.Name}";
            EndBattleEvent(message);
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
            BattleOutput(attackOrder,  DamageProcessing(attackOrder));

            CheckBattleOutcome();

            _supporterContext.GameState.SaveAllStates();
        }

        public void Escape(){
            _supporterContext.SystemEvent = new(LOCATION_EVENT, _supporterContext.GameState.ActiveLocation.Name);
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