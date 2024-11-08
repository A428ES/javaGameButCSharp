using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class SupporterFactory{
        public static ISupporter GenerateSupporter(OptionMap type, SupporterContext supportContext){
            return type switch{
                ITEM_EVENT => new ItemSupporter(supportContext),
                MENU_EVENT => new MainMenuEventSupporter(supportContext),
                BATTLE_EVENT => new BattleSupporter(supportContext),
                LOCATION_EVENT => new LocationSupporter(supportContext)
            };
        }
    }
}