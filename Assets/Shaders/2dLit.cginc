//CGINC

#if !defined(SPRITELIT_INC)
#define SPRITELIT_INC

#include "UnityPBSLighting.cginc"
#include "AutoLight.cginc"

sampler2D _MainTex; // Access sprite

float4 _Color; // Tint
float _MinValue; // How dark is no light?

// Struct to handle Vertex Data
struct VData {
  float4 vertex : POSITION;
  float2 uv : TEXCOORD0;
};

// Structure to Interpolate between Vertex and Fragment programs
struct Interpolators {
  float4 pos : SV_POSITION;
  float2 uv : TEXCOORD0;
  float3 wPos : TEXCOORD1;
};

float max3(float3 v) {
  return max(v.x, max(v.y, v.z));
}

// Utility to create a UnityLight struct based on our interpolated data
UnityLight CreateLight (Interpolators i) {
  UnityLight light;

  UNITY_LIGHT_ATTENUATION(attenuation, i, i.wPos);

  #if defined(POINT) || defined(SPOT)
  light.dir = -normalize(i.wPos - _WorldSpaceLightPos0.xyz );
  light.color = _LightColor0.rgb * attenuation; // Use attenuation for non-directional lights
  #else
  light.dir = _WorldSpaceLightPos0.xyz;
  light.color = _LightColor0.rgb * _MinValue; // Use _MinValue for base directionl light
  #endif

  // Make sure our color doesn't exceed the normal range
  // That way we can adjust the fully-lit radius through the light's intensity
  float max = max3(light.color);
  max = max < 1 ? 1 : max;
  light.color /= max;
  light.ndotl = 1;

  return light;
}

// Vertex Program
Interpolators Vert (VData v) {
  Interpolators v2f;

  v2f.pos = UnityObjectToClipPos(v.vertex);
  v2f.uv = v.uv;

  v2f.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;

  return v2f;
}

// Fragment Program
float4 Frag (Interpolators i) : SV_TARGET
{
  float4 texel = tex2D(_MainTex, i.uv);
  float3 albedo = texel.rgb*_Color.rgb;
  float alpha = texel.a;

  UnityLight light = CreateLight(i);
  albedo *= light.color;

  float4 col = float4(albedo, alpha);

  return col;
}

#endif
