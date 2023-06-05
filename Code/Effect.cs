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
    class MAGNET: BUFF
    {
        private int width;
        private int height;
        EffectType type;
        public MAGNET(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.ContinuousEffect;
        }
        public override void CauseEffect(Player block)
        {
            block.Magnet = true;
            Tool.MagnetTime = 10500;
            Tool.MagnetTimer.Stop();
            Tool.MagnetTimer.Start();
            //Tool.form.setlabel1("magnet!!!");
        }
        public static void LoseEfficacy(object source, System.Timers.ElapsedEventArgs e)
        {
            //Tool.form.setlabel1("LOSE magnet!!!");
            Tool.block.Magnet = false;
        }
    }
    class DEFENSE: BUFF
    {
        private int width;
        private int height;
        EffectType type;
        Timer timer;
        Player block;
        public DEFENSE(Point position, int width, int height, Image image) : base(position, width, height, image)
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
            if (block.CoordinateX > 0)
            {
                block.changepos(block.CoordinateX - 1, block.CoordinateY);
            }
        }
        private void LoseEfficacy(object source, System.Timers.ElapsedEventArgs e)
        {
            block.BulletIgnore = false;
        }
    }
    class TIMESLACK: BUFF
    {
        private int width;
        private int height;
        EffectType type;
        public TIMESLACK(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.ContinuousEffect;
        }
        public override void CauseEffect(Player block)
        {
            Tool.form.BulletSpeed = 3;
            Tool.transmitter.Interval *= 5;
            Tool.TimeslackTime = 5500;
            Tool.TimeslackTimer.Stop();
            Tool.TimeslackTimer.Start();
        }
        public static void LoseEfficacy(object source, System.Timers.ElapsedEventArgs e)
        {
            Tool.form.BulletSpeed = Tool.BULLETSPEED;
            Tool.transmitter.Interval /= 5;
        }
    }
    class INVINCIBILITY: BUFF  
    {
        private int width;
        private int height;
        EffectType type;
        public INVINCIBILITY(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.ContinuousEffect;
        }
        public override void CauseEffect(Player block)
        {
            block.EffectIgnore = true;
            block.BulletIgnore = true;
            Tool.InvincibilityTime = 5500;
            Tool.InvincibilityTimer.Stop();
            Tool.InvincibilityTimer.Start();
        }
        public static void LoseEfficacy(object source, System.Timers.ElapsedEventArgs e)
        {
            Tool.block.EffectIgnore = false;
            Tool.block.BulletIgnore = false;
        }
    }
    class SPRINT: BUFF
    {
        private int width;
        private int height;
        EffectType type;
        public SPRINT(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.ContinuousEffect;
        }
        public override void CauseEffect(Player block)
        {
            Tool.form.BulletSpeed = 20;
            Tool.transmitter.Interval /= 2;
            block.EffectIgnore = true;
            block.BulletIgnore = true;
            Tool.SprintTime = 4500;
            Tool.SprintTimer.Stop();
            Tool.SprintTimer.Start();
            //block.Magnet = true;
        }
        public static void LoseEfficacy(object source, System.Timers.ElapsedEventArgs e)
        {
            Tool.form.BulletSpeed = 5;
            Tool.transmitter.Interval *= 2;
            Tool.block.BulletIgnore = false;
            Tool.block.EffectIgnore = false;
            //Tool.block.Magnet = false;
        }
    }
    class PURE: BUFF
    {
        private int width;
        private int height;
        EffectType type;
        public PURE(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.OnceEffect;
        }
        public override void CauseEffect(Player block)
        {
            block.EffectIgnore = false;
            block.BulletIgnore= false;
            block.Magnet = false;
        }
    }
    class BRAVE: DEBUFF
    {
        private int width;
        private int height;
        EffectType type;
        Timer timer;
        public BRAVE(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.OnceEffect;
            
        }
        public override void CauseEffect(Player block)
        {
            block.BulletIgnore = true;
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
            Tool.block.BulletIgnore = false;
        }
    }
    class FEARLESS: DEBUFF
    {
        private int width;
        private int height;
        EffectType type;
        public FEARLESS(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            type = EffectType.ContinuousEffect;

        }
        public override void CauseEffect(Player block)
        {
            Tool.transmitter.Interval = 50;
            Tool.FearlessTime = 6500;
            Tool.FearlessTimer.Stop();
            Tool.FearlessTimer.Start();
        }
        public static void LoseEfficacy(object source, System.Timers.ElapsedEventArgs e)
        {
            Tool.transmitter.Interval = 200;
        }
    }
}
