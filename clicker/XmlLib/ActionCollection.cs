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
using Processors;

namespace XmlLib
{
    public class ActionCollection
    {
        public List<RootMovement> rootMovements;
        public List<BVHAction> other;
        public List<BVHAction> defense;
        public List<BVHAction> damaged;
        public List<BVHAttackAction> attack;
    }

}
