Shader "Unlit/Texture_reflect_bump" {
Properties {
	_MainTex ("Texture", 2D) = "white" {}
	_BumpMap ("Normal Map", 2D) = "bump" {}
	_Cube ("CubeMap", CUBE) = "black" {}
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }

      CGPROGRAM

      #pragma surface surf NoLighting noforwardadd noambient novertexlights
      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float3 worldRefl;
          INTERNAL_DATA
      };
      sampler2D _MainTex;
      sampler2D _BumpMap;
      samplerCUBE _Cube;

      fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
      	{
      		fixed4 c;
      		c.rgb = s.Albedo;
      		return c;
      	}

      void surf (Input IN, inout SurfaceOutput o) {
          fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

			float3 worldRefl = WorldReflectionVector (IN, o.Normal);
			fixed4 reflcol = texCUBE (_Cube, worldRefl) * c.a;
			o.Emission = reflcol.rgb;
		}
      ENDCG
    }
    Fallback "Diffuse"
}