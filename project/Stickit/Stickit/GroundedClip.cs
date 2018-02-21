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
    class GroundedClip : BaseClip
    {
        public bool Grounded { get; set; }
        public bool CurrentlyGrounded { get; set; }
        public float Elastic { get; set; }

        public GroundedClip(Vector3 position,
            float scale, IDrawable drawable,
            bool grounded, float elastic)
            : base(position, scale, drawable)
        {
            this.Grounded = grounded;
            this.Elastic = elastic;
        }

        public override void Update(GameTime gameTime)
        {
            velocity += S.gravity;
            position += velocity;

            float ty = S.terrain.get_y(position.X, position.Z, true);

            CurrentlyGrounded = false;

            if (Grounded || position.Y < ty)
            {
                CurrentlyGrounded = true;
                position.Y = ty;
                velocity.Y *= -Elastic;
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override Matrix calc_world()
        {
            return Matrix.CreateScale(Scale)
                    * Matrix.CreateRotationX(RotationX)
                    * Matrix.CreateRotationY(RotationY)
                    * Matrix.CreateRotationZ(RotationZ)
                    * Matrix.CreateTranslation(Position);
        }
    }
}
