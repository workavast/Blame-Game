using System;
using System.Collections.Generic;

namespace Avastrad.PoolSystem.LiteDynamic
{
    public class Pool<TElement> where TElement : IPoolable<TElement>
    {
        private readonly Queue<TElement> _freeElements = new();
        private readonly List<TElement> _busyElements = new();

        public IReadOnlyList<TElement> FreeElements => _freeElements.ToArray();
        public IReadOnlyList<TElement> BusyElements => _busyElements;

        public TElement ExtractElement(Func<TElement> instantiateDelegate)
        {
            if (_freeElements.Count == 0)
            {
                var instance = instantiateDelegate();
                _freeElements.Enqueue(instance);
            }

            var extractedElement = _freeElements.Dequeue();
            _busyElements.Add(extractedElement);

            extractedElement.DestroyElementEvent += OnDestroyElement;
            extractedElement.ReturnElementEvent += OnReturnElement;
            extractedElement.OnElementExtractFromPool();

            return extractedElement;
        }

        public void ReturnElement(TElement element) 
            => OnReturnElement(element);
        
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
}