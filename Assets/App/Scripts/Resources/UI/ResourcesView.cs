using System.Collections.Generic;
using App.Resources.ResourcesConfigs;
using App.Resources.Storage;
using App.Utils;
using Avastrad.Libs.EnumValuesLib;
using UnityEngine;
using Zenject;

namespace App.Resources.UI
{
    public abstract class ResourcesView : MonoBehaviour
    {
        [SerializeField] private ResourceView resourceViewPrefab;
        
        [Inject] protected readonly ResourcesConfigsStorage _resourcesConfigs;

        protected abstract IReadOnlyResourceStorage ResourceStorage { get; }
        
        private List<ResourceView> _resourceViews;

        private void Awake() 
            => Initialize();

        public void Initialize()
        {
            transform.DestroyChildren();
            
            _resourceViews = new List<ResourceView>();
            var resourceTypes = EnumValuesTool.GetValues<ResourceType>();

            foreach (var resourceType in resourceTypes)
            {
                var resourceView = Instantiate(resourceViewPrefab, transform);
                var resourceCell = ResourceStorage.GetResourceCell(resourceType);
                var resourceConfig = _resourcesConfigs.GetConfig(resourceType);
                
                resourceView.Initialize(resourceCell, resourceConfig);
                
                _resourceViews.Add(resourceView);
            }
        }

        private void OnEnable()
        {
            foreach (var view in _resourceViews) 
                view.ManualOnEnable();
        }

        private void OnDisable()
        {
            foreach (var view in _resourceViews)
                view.ManualOnDisable();
        }
    }
}