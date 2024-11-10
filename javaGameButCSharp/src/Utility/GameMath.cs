namespace JavaGameButCSharp{
    class GameMath{
        public static double AdjustedSpeed(Entity entity) {
            double adjustedSpeed = entity.Speed + (entity.Strength * 0.05);
            double randomFactor = 0.8 + (new Random().NextDouble() * (1.2 - 0.8));

            return adjustedSpeed * randomFactor;
        }

        public static Entity[] AttackOrder(Entity player, Entity target){
            double playerSpeed = AdjustedSpeed(player);
            double targetSpeed = AdjustedSpeed(target);

            return playerSpeed > targetSpeed ? [player, target] : [target, player];
        }

        public static int DamageCalculation(Entity attacker, Entity defender)
        {
            Item defenseArmor = defender.Inventory.GetActiveArmor();
            Item attackWeapon = attacker.Inventory.GetActiveWeapon();
            
            double weaponImpact = attackWeapon.Modifier + attackWeapon.Condition;
            double armorImpact = defenseArmor.Modifier + defenseArmor.Condition * 1.2; 
            
            weaponImpact += attacker.Strength * 0.25;
            armorImpact += defender.Strength * 0.15;

            double speedFactor = 0.9 + (new Random().NextDouble() * (1.1 - 0.9)); 

            int damageOutcome = (int)((weaponImpact - armorImpact) * speedFactor);

            return damageOutcome;
        }
    }
}