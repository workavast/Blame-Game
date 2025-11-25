using System;
using System.Collections.Generic;
using App.Utils;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.EntityViews
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private CleanupCallback cleanupCallback;

        private WeakObjectReference<EntityView> _prefab;

        public CleanupCallback CleanupCallback => cleanupCallback;

        private List<IEntityViewElement> _views;
        
        private void Awake()
        {
            _views = new List<IEntityViewElement>(GetComponentsInChildren<IEntityViewElement>());
            foreach (var view in _views) 
                view.OnCleanupCompleted += CheckCallbacks;
            
            cleanupCallback.SetCallback(DestroyCallback);
        }

        private void OnDestroy()
        {
            _prefab.TryRelease();
        }

        private void DestroyCallback()
        {
            for (var i = 0; i < _views.Count; i++)
                if (_views[i].OnDestroyCallback()) 
                    _views.RemoveAt(i--);

            if (_views.Count <= 0) 
                Destroy(gameObject);
        }

        public void SetPrefab(ref WeakObjectReference<EntityView> prefab) 
            => _prefab = prefab;
        
        public TView GetView<TView>() where TView : MonoBehaviour, IEntityViewElement
        {
            if (!TryGetView<TView>(out var view))
                throw new NullReferenceException($"Entity View doesnt have requested view: [{name}] [{nameof(TView)}]");
            
            return view;
        }

        private bool TryGetView<TView>(out TView view) where TView : MonoBehaviour, IEntityViewElement
            => TryGetComponent(out view);

        private void CheckCallbacks(IEntityViewElement entityViewElement)
        {
            _views.Remove(entityViewElement);
            if (_views.Count <= 0) 
                Destroy(gameObject);
        }
    }
}