using System;

namespace JavaGameButCSharp{
    abstract class Entity{
        private string name;
        private int health;
        private string location;
        private int strength;
        private int speed;
        private int money;
        private Inventory inventory;
        public Entity(){
        }
    }
}