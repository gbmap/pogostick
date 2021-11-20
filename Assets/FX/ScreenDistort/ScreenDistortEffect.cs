using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.PostProcessing;

[System.Serializable, VolumeComponentMenu("CustomPostProcess/Screen Distort")]
public class ScreenDistortEffect : VolumeComponent
{
}

[CustomPostProcess("Screen Distort", CustomPostProcessInjectionPoint.AfterPostProcess)]
public class ScreenDistortEffectRenderer : CustomPostProcessRenderer
{
    private Material m_Material;

    public override void Initialize()
    {
        base.Initialize();
        m_Material = CoreUtils.CreateEngineMaterial("PostProcess/Screen Distort");
    }

    public override void Render(
        CommandBuffer cmd, 
        RenderTargetIdentifier source, 
        RenderTargetIdentifier destination, 
        ref RenderingData renderingData, 
        CustomPostProcessInjectionPoint injectionPoint
    ) {
        cmd.SetGlobalTexture(Shader.PropertyToID("_MainTex"), source);
        CoreUtils.DrawFullScreen(cmd, m_Material, destination);
    }

    // Called for each camera/injection point pair on each frame. 
    // Return true if the effect should be rendered for this camera.
    public override bool Setup(
        ref RenderingData renderingData,
        CustomPostProcessInjectionPoint injectionPoint
    ) {
        return true;
    }
 
}
