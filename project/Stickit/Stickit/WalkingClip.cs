using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Processors;
using XmlLib;

namespace Stickit
{
    class WalkingClip : GroundedClip
    {
        public IFocusable Target { get; set; }
        public float WantedDistance { get; set; }
        public ActionCollection Actions { get; set; }
        public BVHActionPlayer Player { get; set; }
        public float WalkingSpeed { get; set; }

        public WalkingClip(
            Vector3 position,
            float walkingSpeed,
            float scale, BVHActionPlayer player,
            bool grounded, float elastic, IFocusable target, float wantedDistance,
            params ActionCollection[] actions)
            : base(position, scale, player, grounded, elastic)
        {
            this.Player = ((BVHActionPlayer)base.Drawable);
            this.Actions = new ActionCollection(actions);
            this.WalkingSpeed = walkingSpeed;

            this.Target = target;
            this.WantedDistance = wantedDistance;
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            RotationY = (float)Math.Atan2(
                (double)(Target.Position.X - position.X),
                (double)(Target.Position.Z - position.Z));

            if ((position - Target.Position).Length() <= WantedDistance)
            {
                if (Player.CurrentAction.Name.ToLower() == "walk")
                    Player.CurrentAction = Player.DefaultAction;

                velocity = Vector3.Zero;
            }
            else
            {
                if (Player.CurrentAction.Name.ToLower() == "stand")
                    Player.CurrentAction = Actions["walk"];

                velocity = Target.Position - position;
                velocity.Normalize();
                velocity.Y = 0;
                velocity *= WalkingSpeed;
            }
        }
    }
}
