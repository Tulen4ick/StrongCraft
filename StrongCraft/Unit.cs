using StrongCraft.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StrongCraft
{
    public class Unit: Block
    {
        public int AttackDamage;
        public PointF realPoint;
        public Bitmap Frame;
        public int range;
        public Unit target;
        public int AttackIndice;
        public Block buildingtarget;
        public Unit(int IX, int IY, int BLOCKINDICE, int SIZE, int LVL, int HP, int MAXHP, string TYPE, int ATTACKDAMAGE, PointF realPoint, int range, Unit target, Block buildingtarget)
        {
            iX = IX;
            iY = IY;
            BlockIndice = BLOCKINDICE;
            Size = SIZE;
            Lvl = LVL;
            Hp = HP;
            MaxHp = MAXHP;
            Type = TYPE;
            AttackDamage = ATTACKDAMAGE;
            this.realPoint = realPoint;
            this.range = range;
            this.target = target;
            this.buildingtarget = buildingtarget;
        }
        public Unit()
        {
            iX = -1;
            iY = -1;
            BlockIndice = -2;
            Size = 0;
            Lvl = 0;
            Hp = 0;
            MaxHp = 0;
            Type = "Unit";
            AttackDamage = 0;
            realPoint = new Point(-1, -1);
            range = 0;
            target = null;
            buildingtarget = null;
        }
    }
}
