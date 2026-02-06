using System.Collections.Generic;
using App.Unlocks.Storage;
using UnityEngine;

namespace App.Unlocks.Graphs
{
    public class Graph
    {
        private readonly List<Leaf> _roots;

        public Graph(IReadOnlyList<UnlockConfig> roots)
        {
            _roots = new List<Leaf>(roots.Count);
            foreach (var root in roots) 
                _roots.Add(new Leaf(root, null));
        }

        public UnlockState GetState(UnlockConfig config)
        {
            foreach (var root in _roots) 
                if (root.GetState(config.Id, out var state))
                    return state;

            return UnlockState.UnAvailable;
        }
        
        public void Unlock(UnlockConfig unlockConfig)
        {
            foreach (var root in _roots) 
                root.TryUnlock(unlockConfig.Id);
        }
    
        private class Leaf
        {
            private UnlockConfig UnlockConfig { get; set; }
            private bool Unlocked { get; set; }
            private Leaf Parent { get; set; }
            private List<Leaf> Children { get; set; }
            
            public Leaf(UnlockConfig unlockConfig, Leaf parent)
            {
                UnlockConfig = unlockConfig;
                Unlocked = unlockConfig.Perk.UnlockedByDefault;
                
                Parent = parent;
                
                Children = new List<Leaf>(unlockConfig.ChildUnlocks.Count);
                foreach (var child in unlockConfig.ChildUnlocks) 
                    Children.Add(new Leaf(child, this));
            }
            
            public bool GetState(string id, out UnlockState unlockState)
            {
                if (UnlockConfig.Id == id)
                {
                    if (Unlocked)
                    {
                        unlockState = UnlockState.Unlocked;
                        return true;
                    }

                    if (Parent == null || Parent.Unlocked)
                    {
                        unlockState = UnlockState.Available;
                        return true;
                    }
                }
                else
                {
                    foreach (var child in Children)
                        if (child.GetState(id, out unlockState))
                            return true;
                }
                
                unlockState = UnlockState.UnAvailable;
                return false;
            }
            
            public bool TryUnlock(string id)
            {
                if (UnlockConfig.Id == id) 
                {
                    Unlocked = true;
                    return true;
                }

                foreach (var child in Children)
                    if (child.TryUnlock(id))
                        return true;
                
                return false;
            }
        }
    }
}