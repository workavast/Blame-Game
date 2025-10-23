using System.Collections.Generic;
using Avastrad.Extensions;
using Avastrad.Libs.CheckOnNullLib;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.EscProviding
{
    public class EscProvider : MonoBehaviour
    {
        private readonly List<IEscListener> _listeners = new();
        
        public void Sub(IEscListener listener)
        {
            if (_listeners.Contains(listener))
            {
                Debug.LogError("");
                return;
            }
            
            _listeners.Add(listener);
        }
        
        public void UnSub(IEscListener listener)
        {
            _listeners.TryRemove(listener);
        }

        public void OnEscPressed(InputAction.CallbackContext input)
        {
            if (!input.started)
                return;
            
            ClearNulls();
            if (_listeners.Count > 0) 
                _listeners[0].OnEscPressed();
        }

        private void ClearNulls()
        {
            for (int i = 0; i < _listeners.Count; i++)
            {
                if (_listeners[i].IsAnyNull())
                {
                    _listeners.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}