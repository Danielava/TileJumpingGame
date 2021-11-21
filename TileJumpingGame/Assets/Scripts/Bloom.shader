Shader "Hidden/Bloom"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    
    CGINCLUDE
        #include "UnityCG.cginc"
            
        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        v2f vert (appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            return o;
        }

        sampler2D _MainTex, _SourceTex;
    ENDCG
    
    
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass //0 mippass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv+ float2(-0.0,-0.0));
                // just invert the colors
                float contrast = col.r*0.2126f + col.g*0.7152f + col.b*0.0722f;
                
                if(contrast > 0.4f)
                    return col;
                else
                    return 0;
            }
            ENDCG
        }
        
        Pass //1 addpass
        {
            Blend One One //Makes so we add the previous scene to the current.
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_SourceTex, i.uv);
                // just invert the colors
                //col.rgb = float3(0, 1, 0) * col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
