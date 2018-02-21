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
    class BVHClip : GroundedClip
    {
        public ActionCollection Actions { get; set; }
        public BVHActionPlayer Player { get; set; }
        public float WalkingSpeed { get; set; }

        public BVHClip(Vector3 position,
            float walkingSpeed,
            float scale, BVHActionPlayer player,
            bool grounded, float elastic,
            params ActionCollection[] actions)
            : base(position, scale, player, grounded, elastic)
        {
            this.Player = ((BVHActionPlayer)base.Drawable);
            this.Actions = new ActionCollection(actions);
            this.WalkingSpeed = walkingSpeed;

            this.Player.OnActionComplete += new ActionComplete(player_OnActionComplete);
        }
        void player_OnActionComplete(BVHAction previous, BVHAction current)
        {
            if (previous.Name == "leap")
            {
                velocity.Y = 10;
                Player.CurrentAction = Actions["jump"];
            }
        }

        public override void Update(GameTime gametime)
        {
            if (S.clicked_key(Keys.R))
                Player.CurrentAction = Player.DefaultAction;

            if (CurrentlyGrounded)
            {
                if (is_ready_to_act())
                {
                    #region Check for attack clicks
                    if (S.clicked_key(Keys.X))
                    {
                        Player.CurrentAction = Actions.attack[0];
                        velocity = Vector3.Zero;
                    }
                    if (S.clicked_key(Keys.C))
                    {
                        Player.CurrentAction = Actions.attack[1];
                        velocity = Vector3.Zero;
                    }
                    #endregion
                }

                if (is_ready_to_act())
                {
                    if (!walk() && Player.CurrentAction.Name != "stand")
                    {
                        Player.CurrentAction = Actions["stand"];
                    }

                    if (S.clicked_key(Keys.Space))
                    {
                        Player.CurrentAction = Actions["sitting"];
                        velocity = Vector3.Zero;
                    }
                }
            }

            #region Land
            if (CurrentlyGrounded && Player.CurrentAction.Name == "jump")
                Player.CurrentAction = Actions["land"];
            #endregion

            base.Update(gametime);
        }



        private bool is_ready_to_act()
        {
            return Player.CurrentAction.Name == "stand"
                     || Player.CurrentAction.Name == "walk";
        }

        private bool walk()
        {
            Vector3 drc = Vector3.Zero;

            if (S.kb.IsKeyDown(Keys.Down))
                drc.Z++;

            if (S.kb.IsKeyDown(Keys.Up))
                drc.Z--;

            if (S.kb.IsKeyDown(Keys.Left))
                drc.X--;

            if (S.kb.IsKeyDown(Keys.Right))
                drc.X++;

            drc = Vector3.Transform(drc, Matrix.CreateRotationY(S.camera.AngleA));

            if (drc != Vector3.Zero)
            {
                drc.Normalize();
                drc *= WalkingSpeed;

                RotationY = (float)Math.Atan2(drc.X, drc.Z);

                if (Player.CurrentAction == null ||
                     Player.CurrentAction.Name != "walk")
                    Player.CurrentAction = Actions["walk"];

                velocity.X = drc.X;
                velocity.Z = drc.Z;

                return true;
            }

            velocity.X = velocity.Z = 0;
            return false;
        }

        public void GotoAction(string action)
        {
            Player.CurrentAction = Actions[action];
        }


    }
}
