using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Stickit
{
    abstract class BaseClip : IFocusable
    {
        #region VARS
        protected Vector3 position;
        public Vector3 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }
        protected Vector3 velocity;
        public Vector3 Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value;
            }
        }
        protected Vector3 rotation;
        public float RotationX { get { return rotation.X; } set { rotation.X=value; } }
        public float RotationY { get { return rotation.Y; } set { rotation.Y = value; } }
        public float RotationZ { get { return rotation.Z; } set { rotation.Z = value; } }
        public float Scale { get; set; }
        public IDrawable Drawable { get; set; } 
        #endregion

        public BaseClip(Vector3 position, float scale, IDrawable drawable)
        {
            this.position = position;
            this.Scale = scale;
            this.Drawable = drawable;
            Game1.Draw3DEvent += Draw;
            Game1.UpdateEvent += Update;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Drawable != null)
                Drawable.Animate(gameTime);
        }

        public virtual void Draw()
        {
            if (Drawable != null)
                Drawable.Draw(this);
        }

        public abstract Matrix calc_world();
    }
}
