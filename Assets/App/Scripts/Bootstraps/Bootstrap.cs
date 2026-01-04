using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Bootstraps
{
    public abstract class Bootstrap : MonoBehaviour
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        
        protected async Task Initialize(CancellationToken externalToken)
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cancellationTokenSource.Token, externalToken);
            var linkedToken = linkedCts.Token;
            
            await SelfInitialization(linkedToken);

            linkedToken.ThrowIfCancellationRequested();
            
            await InitializeChildren(linkedToken);
        }

        protected virtual void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        protected abstract Task SelfInitialization(CancellationToken cancellationToken);
        
        private async Task InitializeChildren(CancellationToken cancellationToken)
        {
            var children = GetChildrenBootstraps();
            foreach (var child in children)
            {
                await child.Initialize(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
        
        private List<Bootstrap> GetChildrenBootstraps()
        {
            var children = new List<Bootstrap>(gameObject.transform.childCount);
            for (var i = 0; i < gameObject.transform.childCount; i++)
            {
                var child = gameObject.transform.GetChild(i);
                if (child.gameObject.activeSelf && child.TryGetComponent<Bootstrap>(out var childBootstrap)) 
                    children.Add(childBootstrap);
            }

            return children;
        }
    }
}