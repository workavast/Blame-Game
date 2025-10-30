using System;

namespace Avastrad.PoolSystem.LiteDynamic
{
    public interface IPoolable<TElement>
    {
        public event Action<TElement> ReturnElementEvent;
        public event Action<TElement> DestroyElementEvent;
    
        public void OnElementExtractFromPool();
        public void OnElementReturnInPool();
    }
}