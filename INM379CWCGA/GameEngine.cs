using System;
using System.Collections.Generic;
using System.Text;

namespace INM379CWCGA
{
    class GameEngine
    {
        private static GameEngine currentInstance = null;
        public static GameEngine Instance
        {
            get
            {
                if (currentInstance == null)
                    currentInstance = new GameEngine();
                return currentInstance;
            }

            set { currentInstance = value; }
        }

        public PlayerStats PlayerStats;
        public MageSets MageSets;
    }

    public class PlayerStats
    {
        public int Health = 3;
    }


    public class MageSets
    {
        public float speed = 0.0f;
    }

}
