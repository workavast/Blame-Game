using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

namespace App.PostVFX
{
    public class PostVfxRenderFeature : ScriptableRendererFeature
    {
        [SerializeField] private RenderPassEvent injectionPoint = RenderPassEvent.BeforeRenderingPostProcessing;
        [SerializeField] private PostVfxRenderFeatureSettings settings;
    
        private PostVfxRenderFeaturePass m_ScriptablePass;
    
        public override void Create()
        {
            m_ScriptablePass = new PostVfxRenderFeaturePass(settings);
            m_ScriptablePass.renderPassEvent = injectionPoint;
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (settings.PostVfxMaterial == null)
            {
                Debug.LogError("PostVfxRenderFeaturePass requires PostVfxMaterial to be set.");
                return;
            }
        
            renderer.EnqueuePass(m_ScriptablePass);
        }

        [Serializable]
        public class PostVfxRenderFeatureSettings
        {
            public Material PostVfxMaterial;
            public GraphicsFormat GraphicsFormat = GraphicsFormat.R8G8B8A8_SRGB;
            public PostVfxRenderFeaturePassType PassType;
            public FilterMode FilterMode = FilterMode.Point;
            public int ShaderPass;
        }

        public enum PostVfxRenderFeaturePassType
        {
            CameraColor,
            CameraOpaqueTexture,
            ActiveColorTexture,
        }
    
        private class PostVfxRenderFeaturePass : ScriptableRenderPass
        {
            private readonly PostVfxRenderFeatureSettings _settings;
            private const string FeatureName = nameof(PostVfxRenderFeature);
            private Material PostVfxMaterial => _settings.PostVfxMaterial;

            public PostVfxRenderFeaturePass(PostVfxRenderFeatureSettings settings)
            {
                _settings = settings;
                requiresIntermediateTexture = true;
            }

            public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
            {
                var resourceData = frameData.Get<UniversalResourceData>();
                if (resourceData.isActiveTargetBackBuffer)
                {
                    Debug.LogError($"Skipping render pass: {FeatureName}");
                    return;
                }

                TextureHandle source = _settings.PassType switch
                {
                    PostVfxRenderFeaturePassType.CameraColor => resourceData.cameraColor,
                    PostVfxRenderFeaturePassType.CameraOpaqueTexture => resourceData.cameraOpaqueTexture,
                    PostVfxRenderFeaturePassType.ActiveColorTexture => resourceData.activeColorTexture,
                    _ => throw new ArgumentOutOfRangeException()
                };

                var destinationDesc = renderGraph.GetTextureDesc(source);
                destinationDesc.name = $"CameraColor-{FeatureName}";
                destinationDesc.clearBuffer = false;
                destinationDesc.format = _settings.GraphicsFormat;
                destinationDesc.colorFormat = _settings.GraphicsFormat;
                destinationDesc.msaaSamples = MSAASamples.None;
            
                var destination = renderGraph.CreateTexture(destinationDesc);
                RenderGraphUtils.BlitMaterialParameters parameters = new(source, destination, PostVfxMaterial, _settings.ShaderPass);
                renderGraph.AddBlitPass(parameters, passName: FeatureName);
            
                resourceData.cameraColor = destination;
            }
        }
    }
}
