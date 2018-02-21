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
        class DrawableModel : IDrawable
        {
            Matrix world;
            Matrix[] bones;
            Model mdl;

            public DrawableModel(Model mdl)
            {
                this.mdl = mdl;

                bones = new Matrix[mdl.Bones.Count];
                mdl.CopyAbsoluteBoneTransformsTo(bones);
            }

            public void Animate(GameTime gameTime)
            {

            }
            public void Draw(BaseClip bc)
            {
                world = Matrix.CreateScale(bc.Scale)
        * Matrix.CreateRotationX(bc.RotationX)
        * Matrix.CreateRotationY(bc.RotationY)
        * Matrix.CreateRotationZ(bc.RotationZ)
            * Matrix.CreateTranslation(bc.Position);

               // S.gd.BlendState = BlendState.Opaque;
                S.be.EnableDefaultLighting();
                //S.be.VertexColorEnabled = false;
                //S.be.Techniques[0].Passes[0].Apply();

                foreach (ModelMesh mesh in mdl.Meshes)
                {
                    foreach (BasicEffect be in mesh.Effects)
                    {
                        be.View = S.be.View;
                        be.Projection = S.be.Projection;
                        be.World = bones[mesh.ParentBone.Index] * world;
                        //be.Alpha = 0;

                    
                    }

                    mesh.Draw();
                }

                S.gd.BlendState = BlendState.Opaque;
          
            }
        }
    }
