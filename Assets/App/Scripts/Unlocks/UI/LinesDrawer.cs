using System;
using System.Collections.Generic;
using Radishmouse;
using UnityEngine;

namespace App.Unlocks.UI
{
    public class LinesDrawer : MonoBehaviour
    {
        [SerializeField] private UILineRenderer lineRendererPrefab;
        [SerializeField] private UnlocksConfig config;
        [SerializeField] private List<UnlockView> views;

        public void Initialize()
        {
            foreach (var view in views) 
                DrawLine(view);
        }
        
        private void DrawLine(UnlockView parentView)
        {
            var children = parentView.GetUnlockConfig().ChildUnlocks;

            foreach (var child in children)
            {
                foreach (var view in views)
                {
                    if (child.Id == view.GetUnlockConfig().Id)
                    {
                        var lineRenderer = Instantiate(lineRendererPrefab, transform);
                        
                        lineRenderer.points = new Vector2[2];
                        lineRenderer.points[0] = parentView.transform.position;
                        lineRenderer.points[1] = view.transform.position;
                    }
                }                    
            }
        }
    }
}