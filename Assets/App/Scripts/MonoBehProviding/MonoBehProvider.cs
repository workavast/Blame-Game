using System.Collections;
using UnityEngine;

namespace App.MonoBehProviding
{
    public class MonoBehProvider : MonoBehaviour, IMonoBehProvider
    {
        void IMonoBehProvider.StopCoroutine(Coroutine coroutine)
        {
            if (coroutine == null)
                return;
            
            StopCoroutine(coroutine);
        }
    }

    public interface IMonoBehProvider
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);

        public void StopCoroutine(Coroutine coroutine);
    }
}