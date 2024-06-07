using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrongCraft
{
    public class Building: Block
    {
        public string Resource;
        public int ResourceValue;
        public bool Create;
        public Building(int IX, int IY, int BLOCKINDICE, int SIZE, int LVL, int HP, int MAXHP, string TYPE, string RESOURCE, int RESOURCEVALUE, bool CREATE)
        {
            iX = IX;
            iY = IY;
            BlockIndice = BLOCKINDICE;
            Size = SIZE;
            Lvl = LVL;
            Hp = HP;
            MaxHp = MAXHP;
            Type = TYPE;
            Resource = RESOURCE;
            ResourceValue = RESOURCEVALUE;
            Create = CREATE;
        }
    }
}
