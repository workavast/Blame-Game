using System;
using UnityEngine;

namespace App
{
    public class RequestCounter
    {
        public bool RequestMoreZero => RequestsCount > 0;
        public int RequestsCount { get; private set; }
        
        private readonly Action<bool> _onStateChanged;

        public RequestCounter(Action<bool> onStateChanged)
        {
            _onStateChanged = onStateChanged;
        }

        public void ChangeRequests(bool addRequest)
        {
            if (addRequest)
                AddRequest();
            else
                RemoveRequest();
        }

        public void AddRequest()
        {
            RequestsCount++;
            if (1 == RequestsCount)
                _onStateChanged?.Invoke(true);
        }

        public void RemoveRequest()
        {
            RequestsCount--;
            if (RequestsCount < 0)
            {
                RequestsCount = 0;
                Debug.LogWarning("You try reduce requests count when it equal 0");
                return;
            }
            
            if (0 == RequestsCount)
                _onStateChanged?.Invoke(false);
        }
    }
}