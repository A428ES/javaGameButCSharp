using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class SupporterFactory{
        public static Supporter GenerateSupporter(OptionMap type, SupporterContext supportContext){
            return type switch{
                INVENTORY_EVENT => new InventorySupporter(supportContext),
                MENU_EVENT => new MainMenuEventSupporter(supportContext),
                ENTITY_EVENT => new EntitySupporter(supportContext),
                LOCATION_EVENT => new LocationSupporter(supportContext),
                BATTLE_EVENT => new BattleSupporter(supportContext),
                ITEM_EVENT => new ItemSupporter(supportContext),
                _=> throw new Exception("No options")
            };
        }
    }
}