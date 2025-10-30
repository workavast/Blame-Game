using System;
using App.ResolutionProviding;
using UnityEngine;

namespace App.PostVfx
{
    public class PostVfxResizer : IDisposable
    {
        private readonly Camera _postVfxCamera;
        private readonly IResolutionProviderRO _resolutionProvider;
        private readonly RenderTexture _renderTexture;

        public PostVfxResizer(Camera postVfxCamera, RenderTexture renderTexture, IResolutionProviderRO resolutionProvider)
        {
            _postVfxCamera = postVfxCamera;
            _renderTexture = renderTexture;
            
            _resolutionProvider = resolutionProvider;
            _resolutionProvider.OnResolutionChanged += UpdatePostVfxResolution;
            UpdatePostVfxResolution();
        }

        public void Dispose()
        {
            _resolutionProvider.OnResolutionChanged -= UpdatePostVfxResolution;
        }
        
        private void UpdatePostVfxResolution()
        {
            _renderTexture.Release();
            _renderTexture.width = _resolutionProvider.Resolution.x;
            _renderTexture.height = _resolutionProvider.Resolution.y;
            _renderTexture.Create();
            
            _postVfxCamera.Render();
        }
    }
}