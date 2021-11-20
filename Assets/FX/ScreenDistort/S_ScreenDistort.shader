Shader "PostProcess/Screen Distort"
{
    HLSLINCLUDE 

    // #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.yetman.render-pipelines.universal.postprocess/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

    TEXTURE2D_X(_MainTex);
    float4 _MainTex_TexelSize;
    //TEXTURE2D_X(_CameraDepthTexture);
    //TEXTURE2D_X(_CameraNormalsTexture);
    //TEXTURE2D_X(_CameraDepthNormalsTexture);
    

    // /*
    // https://github.com/yahiaetman/URPCustomPostProcessingStack/blob/master/Samples~/Examples/Resources/Shaders/PostProcessing/EdgeDetection.shader
    // */

     // Curtom Scene normal and depth sampling function to read from a custom texture and sampler
    float3 SampleSceneNormals(float2 uv, TEXTURE2D_X_FLOAT(_Texture), SAMPLER(sampler_Texture))
    {
        return UnpackNormalOctRectEncode(SAMPLE_TEXTURE2D_X(_Texture, sampler_Texture, UnityStereoTransformScreenSpaceTex(uv)).xy) * float3(1.0, 1.0, -1.0);
    }

    float SampleSceneDepth(float2 uv, TEXTURE2D_X_FLOAT(_Texture), SAMPLER(sampler_Texture))
    {
        return SAMPLE_TEXTURE2D_X(_Texture, sampler_Texture, UnityStereoTransformScreenSpaceTex(uv)).r;
    }

    // A Bilinear sampler to allow for subpixel thickness for the edge 
    SAMPLER(sampler_linear_clamp);

    // Sample normal & depth and combine them to a single 4D vector (xyz for normal, w for depth)
    float4 SampleSceneDepthNormals(float2 uv){
        float depth = SampleSceneDepth(uv, _CameraDepthTexture, sampler_linear_clamp);
        float depthEye = LinearEyeDepth(depth, _ZBufferParams);
        float3 normal = SampleSceneNormals(uv, _CameraNormalsTexture, sampler_linear_clamp);
        return float4(normal, depthEye);
    }

    // Read the 8 surrounding samples and average them with perspective correction
    // The perspective correction helps when the surface normal is almost orthogonal to the view direction
    float4 SampleNeighborhood(float2 uv, float thickness){
        // The surrounding pixel offsets
        const float2 offsets[8] = {
            float2(-1, -1),
            float2(-1, 0),
            float2(-1, 1),
            float2(0, -1),
            float2(0, 1),
            float2(1, -1),
            float2(1, 0),
            float2(1, 1)
        };
        
        float2 delta = _MainTex_TexelSize.xy * thickness;
        float4 sum = 0;
        float weight = 0;
        for(int i=0; i<8; i++){
            float4 sample = SampleSceneDepthNormals(uv + delta * offsets[i]);
            // The sum is weight by 1/depth for perspecive correction
            //sum += sample / sample.w;
            sum += sample/sample.w;
            weight += 1/sample.w;
        }
        sum /= weight;
        // Doesn't make a visible difference but it feels more correct
        // May remove it for performance benefits
        // sum.xyz = normalize(sum.xyz);
        return sum;
    }

    float CalculateEdge(float2 uv)
    {
        float4 center = SampleSceneDepthNormals(uv);
        float4 neighborhood = SampleNeighborhood(uv, 3.0);
        return (1.-smoothstep(-5., 5., center.w - neighborhood.w))*abs(center.w-neighborhood.w);

        float2 nt = float2(3.14, 3.14*5.);
        float2 dt = float2(10., 20.);
        float deg2rad = 0.01745329;
        float4 t = float4(cos(nt.y*deg2rad), cos(nt.x*deg2rad), dt.x, dt.y);

        //float4 t = float4(0.0, 0.01, 0.1, 0.3);
        float normal_similarity = smoothstep(t.x, t.y, dot(center.rgb, neighborhood.rgb));
        float depth_similarity = smoothstep(t.z*center.w, t.w*center.w, abs(center.w - neighborhood.w));
        float edge = 1 - normal_similarity*depth_similarity;
        return edge;
    }
    // 2D Random
    float random (float2 st) 
    {
        
        return frac(sin(dot(st.xy,
                            float2(12.9898,78.233)))
                    * 43758.5453123);
    }

    float noise (float2 st) 
    {
        float2 i = floor(st);
        float2 f = frac(st);

        // Four corners in 2D of a tile
        float a = random(i);
        float b = random(i + float2(1.0, 0.0));
        float c = random(i + float2(0.0, 1.0));
        float d = random(i + float2(1.0, 1.0));

        // Smooth Interpolation

        // Cubic Hermine Curve.  Same as SmoothStep()
        float2 u = f*f*(3.0-2.0*f);
        // u = smoothstep(0.,1.,f);

        // Mix 4 coorners percentages
        return lerp(a, b, u.x) +
                (c - a)* u.y * (1.0 - u.x) +
                (d - b) * u.x * u.y;
    }

    float4 ScreenDistortEffect(PostProcessVaryings input) : SV_Target
    {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

        float2 uv = UnityStereoTransformScreenSpaceTex(input.texcoord);

        float depth = LOAD_TEXTURE2D_X(_CameraDepthTexture, uv*_ScreenSize.xy).x;
        float ldepth = 1.0 / (_ZBufferParams.z * depth + _ZBufferParams.w);

        // float t = frac(_Time.y/10.0)*10.0;
        // float w = 1.0;

        // float tt = step(t-w, ldepth)-step(t+w, ldepth);
        // tt = smoothstep(t+w, t, ldepth) * smoothstep(t-w, t, ldepth);
        //tt = depth;
        //depth = 1.0 / (_ZBufferParams.z * depth + _ZBufferParams.w);

        // uv = uv*2.-1.;
        // uv = uv+(uv*uv)*tt;
        // uv = (uv+1.)*.5;
        float edge = CalculateEdge(input.texcoord);
        // uv = uv*2.-1.;
        // uv = uv+edge*0.05*depth*frac(_Time.y);
        // uv = (uv+1.)*0.5;

        float4 color = LOAD_TEXTURE2D_X(_MainTex, uv * _ScreenSize.xy);
        // return color;

        //color = lerp(color, float4(0.0, 1.0, 0.0, 1.0), edge);
        return color;

        //return float4(depth, 0., 0., 1.0);
        // return color + float4(1.0, 1.0, 1.0, 1.0) * tt;
    }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            HLSLPROGRAM
            #pragma vertex FullScreenTrianglePostProcessVertexProgram
            #pragma fragment ScreenDistortEffect
            ENDHLSL
        }
    }
}
