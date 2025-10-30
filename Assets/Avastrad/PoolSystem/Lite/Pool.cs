using System;
using System.Collections.Generic;

namespace Avastrad.PoolSystem.Lite
{
    public class Pool<TElement> where TElement : IPoolable<TElement>
    {
        private readonly Func<TElement> _instantiateDelegate;
        private readonly Queue<TElement> _freeElements;
        private readonly List<TElement> _busyElements;

        public IReadOnlyList<TElement> FreeElements => _freeElements.ToArray();
        public IReadOnlyList<TElement> BusyElements => _busyElements;

        public Pool(Func<TElement> instantiateDelegate)
        {
            if (instantiateDelegate == null) 
                throw new Exception("instantiateDelegate is null");

            _instantiateDelegate = instantiateDelegate;
            _freeElements = new Queue<TElement>();
            _busyElements = new List<TElement>();
        }

        public TElement ExtractElement()
        {
            if (_freeElements.Count == 0) 
                InstantiateElement();

            var extractedElement = _freeElements.Dequeue();
            _busyElements.Add(extractedElement);

            extractedElement.DestroyElementEvent += OnDestroyElement;
            extractedElement.ReturnElementEvent += OnReturnElement;
            extractedElement.OnElementExtractFromPool();

            return extractedElement;
        }

        public void ReturnElement(TElement element) 
            => OnReturnElement(element);
        
        private void InstantiateElement() =>
            _freeElements.Enqueue(_instantiateDelegate());

        private void OnDestroyElement(TElement element)
        {
            if (_busyElements.Remove(element))
            {
                element.DestroyElementEvent -= OnDestroyElement;
                element.ReturnElementEvent -= OnReturnElement;
            }
        }

        private void OnReturnElement(TElement element)
        {
            if (_busyElements.Contains(element))
            {
                _freeElements.Enqueue(element);

                element.DestroyElementEvent -= OnDestroyElement;
                element.ReturnElementEvent -= OnReturnElement;

                _busyElements.Remove(element);

                element.OnElementReturnInPool();
            }
        }
    }

    public class Pool<TElement, TId> where TElement : IPoolable<TElement, TId>
    {
        private readonly Func<TId, TElement> _instantiateDelegate;
        private readonly Dictionary<TId, Pool<TElement>> _pools = new();

        public Pool(Func<TId, TElement> instantiateDelegate)
        {
            if (instantiateDelegate == null)
                throw new Exception("instantiateDelegate is null");

            _instantiateDelegate = instantiateDelegate;
        }

        public TElement ExtractElement(TId id)
        {
            if (!_pools.ContainsKey(id))
                AddPool(id);

            return _pools[id].ExtractElement();
        }

        public void ReturnElement(TElement element)
        {
            if (!_pools.ContainsKey(element.PoolId))
                throw new Exception($"pool doesnt contains this id: [{element.PoolId}]");

            _pools[element.PoolId].ReturnElement(element);
        }

        public bool Exist(TId id)
            => _pools.ContainsKey(id);

        public IReadOnlyCollection<TId> GetExistingIds()
            => _pools.Keys;

        /// <returns> if pool doesnt exist return null</returns>
        public IReadOnlyList<TElement> GetFreeElements(TId id)
        {
            if (_pools.ContainsKey(id))
                return _pools[id].FreeElements;
            else
                return null;
        }

        /// <returns> if pool doesnt exist return null</returns>
        public IReadOnlyList<TElement> GetBusyElements(TId id)
        {
            if (_pools.ContainsKey(id))
                return _pools[id].BusyElements;
            else
                return null;
        }

        private void AddPool(TId id) 
            => _pools.Add(id, new Pool<TElement>(() => _instantiateDelegate.Invoke(id)));
    }
}