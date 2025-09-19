using System;
using UnityEngine;

namespace App
{
    [DisallowMultipleComponent]
    public class CleanupCallback : MonoBehaviour
    {
        private Action _callbackAction;
        
        public void SetCallback(Action callbackAction)
        {
            _callbackAction = callbackAction;
        }
        
        public void Callback()
        {
            _callbackAction?.Invoke();   
        }
    }
}