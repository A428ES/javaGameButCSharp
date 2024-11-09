using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class SupporterFactory{
        public static Supporter GenerateSupporter(OptionMap type, SupporterContext supportContext){
            return type switch{
                INVENTORY => new InventorySupporter(supportContext),
                MENU_EVENT => new MainMenuEventSupporter(supportContext),
                ENTITY_EVENT => new EntitySupporter(supportContext),
                LOCATION_EVENT => new LocationSupporter(supportContext),
                BATTLE_EVENT => new BattleSupporter(supportContext),
                _=> throw new Exception("No options")
            };
        }
    }
}