using App.Utils;
using Unity.Entities.Content;

namespace App.Audio.Sources
{
    public class AudioPoolRelease : AudioSourceHolderPoolable
    {
        private WeakObjectReference<AudioPoolRelease> _sfxRef;
        
        protected override void OnDestroy()
        {
            if (_sfxRef.IsReferenceValid)
                _sfxRef.TryRelease();
            
            base.OnDestroy();
        }

        public void SetAudioRef(WeakObjectReference<AudioPoolRelease> prefabRef)
        {
            if (_sfxRef.IsReferenceValid)
            {
                if (!_sfxRef.Equals(prefabRef))
                    prefabRef.LoadAsync();
            }
            else
            {
                prefabRef.LoadAsync();
            }
            
            _sfxRef = prefabRef;
        }
    }
}