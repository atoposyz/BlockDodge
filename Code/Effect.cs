using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

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
            DamageType = 1;
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
            DamageType = 2;
        }
    }
    class SHIELD: BUFF
    {
        private int width;
        private int height;
        EffectType type;
        public SHIELD(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.OnceEffect;
        }
        public override void CauseEffect(Player block)
        {
            block.ShieldCapacity++;
        }
    }
    class SHIELD_TimeLimit: BUFF
    {
        private int width;
        private int height;
        EffectType type;
        Timer timer;
        Player block;
        public SHIELD_TimeLimit(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.ContinuousEffect;
        }
        public override void CauseEffect(Player block)
        {
            block.BulletIgnore = true;
            this.block = block;
            timer = new Timer(100);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.LoseEfficacy);
            timer.AutoReset = false;
            timer.Enabled = true;
        }
        private void LoseEfficacy(object source, System.Timers.ElapsedEventArgs e)
        {
            block.BulletIgnore = false;
        }
    }
    class BRAVE: DEBUFF
    {
        private int width;
        private int height;
        EffectType type;
        Timer timer;
        Player block;
        public BRAVE(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.OnceEffect;
            
        }
        public override void CauseEffect(Player block)
        {
            block.BulletIgnore = true;
            this.block = block;
            timer = new Timer(100);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.LoseEfficacy);
            timer.AutoReset = false;
            timer.Enabled = true;
            if (block.CoordinateX < 2)
            {
                block.changepos(block.CoordinateX + 1, block.CoordinateY);
            }
        }
        private void LoseEfficacy(object source, System.Timers.ElapsedEventArgs e)
        {
            block.BulletIgnore = false;
        }
    }
}
