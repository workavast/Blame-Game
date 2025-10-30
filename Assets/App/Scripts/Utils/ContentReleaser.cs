using Unity.Entities;
using Unity.Entities.Content;

namespace App.Utils
{
    public static class ContentReleaser
    {
        public static void TryRelease<TObject>(this WeakObjectReference<TObject> content)
            where TObject : UnityEngine.Object
        {
            if (World.DefaultGameObjectInjectionWorld != null)
                content.Release();
        }
    }
}