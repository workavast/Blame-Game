using UnityEngine;
using Zenject;

namespace App.Audio.Ambience
{
    public class AmbienceInitializer : MonoBehaviour
    {
        [SerializeField] private AmbienceSource ambienceSourcePrefab;
        [SerializeField] private float transitionTime = 1;
        
        [Inject] private readonly AmbienceManager _ambienceManager;

        private void Start() 
            => _ambienceManager.Activate(ambienceSourcePrefab, transitionTime);
    }
}