using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Code
{   
    public enum EffectType
    {
        OnceEffect,
        TimesEffect,
        ContinuousEffect
    }
    internal abstract class Effect: Bullet
    {
        private int width;
        private int height;
        EffectType type;
        public Effect(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
        }
        abstract public void CauseEffect(Player block);
    }
    abstract class BUFF: Effect
    {
        private int width;
        private int height;
        EffectType type;
        public BUFF(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
        }
    }
    abstract class DEBUFF : Effect
    {
        private int width;
        private int height;
        EffectType type;
        public DEBUFF(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
        }
    }
    class BRAVE: DEBUFF
    {
        private int width;
        private int height;
        EffectType type;
        public BRAVE(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.OnceEffect;
        }
        public override void CauseEffect(Player block)
        {
            if(block.CoordinateX < 2)
            {
                block.changepos(block.CoordinateX + 1, block.CoordinateY);
            }
        }
    }
}
