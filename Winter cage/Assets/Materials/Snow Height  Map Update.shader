Shader "Unlit/Snow Height  Map Update"
{
    Properties
    {
    }

    SubShader
    {
        Lighting Off
        Blend One Zero
        
        Pass
        {
            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0
            
            float4 _DrawPosition;
            float4 _DrawColor;

            
            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 color = tex2D(_SelfTexture2D,IN.localTexcoord.xy);


                return  color;
            }
            ENDCG
        }
    }
}
