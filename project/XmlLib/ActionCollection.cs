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
        public static Dictionary<string, List<RootMovement>> bvhToLoad =
            new Dictionary<string, List<RootMovement>>();
        public List<RootMovement> rootMovements;
        public List<BVHAction> other;
        public List<BVHAction> defense;
        public List<BVHAction> damaged;
        public List<BVHAttackAction> attack;

        public BVHAction this[ActionType e, string name]
        {
            get
            {
                name = name.ToLower();

                if (e == ActionType.Attack)
                {
                    foreach (BVHAttackAction action in attack)
                    {
                        if (action.Name.ToLower() == name)
                        {
                            return action;
                        }
                    }
                }
                else
                {
                    List<BVHAction> actions = null;

                    switch (e)
                    {
                        case ActionType.Damaged:
                            actions = damaged; break;
                        case ActionType.Defense:
                            actions = defense; break;
                        case ActionType.Other:
                            actions = other; break;
                    }

                    foreach (BVHAction action in actions)
                    {
                        if (action.Name.ToLower() == name)
                        {
                            return action;
                        }
                    }

                }

                return null;
            }

        }

        public BVHAction this[string actionName]
        {
            get
            {
                BVHAction action;

                if ((action = this[ActionType.Other, actionName]) != null)
                    return action;
                else if ((action = this[ActionType.Defense, actionName]) != null)
                    return action;
                else if ((action = this[ActionType.Other, actionName]) != null)
                    return action;
                else if ((action = this[ActionType.Attack, actionName]) != null)
                    return action;

                return null;
            }
        }

        public BVHAction this[ActionType e, int index]
        {
            get
            {
                switch (e)
                {
                    case ActionType.Attack:
                        return attack[index];
                    case ActionType.Damaged:
                        return damaged[index];
                    case ActionType.Other:
                        return other[index];
                    case ActionType.Defense:
                        return defense[index];
                }

                return null;
            }
        }


        public ActionCollection()
        {
            this.other = new List<BVHAction>();
            this.defense = new List<BVHAction>();
            this.damaged = new List<BVHAction>();
            this.attack = new List<BVHAttackAction>();
        }

        public ActionCollection(params ActionCollection[] collections)
            : this()
        {
            foreach (ActionCollection ac in collections)
            {
                other.AddRange(ac.other);
                defense.AddRange(ac.defense);
                damaged.AddRange(ac.damaged);
                attack.AddRange(ac.attack);

                add_bvh_to_load(ac.other, ac);
                add_bvh_to_load(ac.defense, ac);
                add_bvh_to_load(ac.damaged, ac);
                add_bvh_to_load(ac.attack, ac);
            }
        }

        public bool add_bvh_to_load(List<BVHAction> actions, ActionCollection ac)
        {
            foreach (BVHAction act in actions)
            {
                act.Name = act.Name.ToLower();

                if (!bvhToLoad.ContainsKey(act.Bvhfile))
                {
                    bvhToLoad.Add(act.Bvhfile, ac.rootMovements);
                    return true;
                }
            }
            return false;
        }

        public bool add_bvh_to_load(List<BVHAttackAction> actions, ActionCollection ac)
        {
            foreach (BVHAttackAction act in actions)
            {
                act.Name = act.Name.ToLower();
                
                if (!bvhToLoad.ContainsKey(act.Bvhfile))
                {
                    bvhToLoad.Add(act.Bvhfile, ac.rootMovements);
                    return true;
                }
            }
            return false;
        }
    }
}
