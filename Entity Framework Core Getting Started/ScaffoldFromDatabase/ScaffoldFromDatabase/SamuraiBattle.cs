using System;
using System.Collections.Generic;

namespace ScaffoldFromDatabase
{
    public partial class SamuraiBattle
    {
        public int BattleId { get; set; }
        public int SamuraiId { get; set; }

        public Battle Battle { get; set; }
        public Samurai Samurai { get; set; }
    }
}
