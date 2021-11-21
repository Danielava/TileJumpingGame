using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class BloomEffect : MonoBehaviour
{
    public Shader bloomShader;

    Material bloom;

    const int mippass = 0;
    const int addpass = 1;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        if(bloom == null)
        {
            bloom = new Material(bloomShader);
            bloom.hideFlags = HideFlags.HideAndDontSave;
        }

        int width = source.width / 32;
        int height = source.height / 32;
        RenderTextureFormat format = source.format;

        //Blur first to a temporary texture
        RenderTexture temp = RenderTexture.GetTemporary(
             width, height, 0, format
        );

        Graphics.Blit(source, temp, bloom, mippass);
        Graphics.Blit(temp, destination);


        RenderTexture currentSource = destination;
        //Graphics.Blit(source, currentSource);

        //Now we're gonna have the source texture available to sample from
        //and apply our bloom to that.
        bloom.SetTexture("_SourceTex", source);
        Graphics.Blit(currentSource, destination, bloom, addpass);


        RenderTexture.ReleaseTemporary(temp);

        //First blit to temp texture, then from that to destination
        //Graphics.Blit(source, r);
        //Graphics.Blit(r, destination);
        //RenderTexture.ReleaseTemporary(r);
    }
}

/*
using UnityEngine;
using System;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class BloomEffect : MonoBehaviour {

    const int BoxDownPrefilterPass = 0;
    const int BoxDownPass = 1;
    const int BoxUpPass = 2;
    const int ApplyBloomPass = 3;
    const int DebugBloomPass = 4;

    public Shader bloomShader;

    [Range(0, 10)]
    public float intensity = 1;

    [Range(1, 16)]
    public int iterations = 4;

    [Range(0, 10)]
    public float threshold = 1;

    [Range(0, 1)]
    public float softThreshold = 0.5f;

    public bool debug;

    RenderTexture[] textures = new RenderTexture[16];

    [NonSerialized]
    Material bloom;

    void OnRenderImage (RenderTexture source, RenderTexture destination) {
        if (bloom == null) {
            bloom = new Material(bloomShader);
            bloom.hideFlags = HideFlags.HideAndDontSave;
        }

        float knee = threshold * softThreshold;
        Vector4 filter;
        filter.x = threshold;
        filter.y = filter.x - knee;
        filter.z = 2f * knee;
        filter.w = 0.25f / (knee + 0.00001f);
        bloom.SetVector("_Filter", filter);
        bloom.SetFloat("_Intensity", Mathf.GammaToLinearSpace(intensity));

        int width = source.width / 2;
        int height = source.height / 2;
        RenderTextureFormat format = source.format;

        RenderTexture currentDestination = textures[0] =
            RenderTexture.GetTemporary(width, height, 0, format);
        Graphics.Blit(source, currentDestination, bloom, BoxDownPrefilterPass);
        RenderTexture currentSource = currentDestination;

        int i = 1;
        for (; i < iterations; i++) {
            width /= 2;
            height /= 2;
            if (height < 2) {
                break;
            }
            currentDestination = textures[i] =
                RenderTexture.GetTemporary(width, height, 0, format);
            Graphics.Blit(currentSource, currentDestination, bloom, BoxDownPass);
            currentSource = currentDestination;
        }

        for (i -= 2; i >= 0; i--) {
            currentDestination = textures[i];
            textures[i] = null;
            Graphics.Blit(currentSource, currentDestination, bloom, BoxUpPass);
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDestination;
        }

        if (debug) {
            Graphics.Blit(currentSource, destination, bloom, DebugBloomPass);
        }
        else {
            bloom.SetTexture("_SourceTex", source);
            Graphics.Blit(currentSource, destination, bloom, ApplyBloomPass);
        }
        RenderTexture.ReleaseTemporary(currentSource);
    }
}
 */
