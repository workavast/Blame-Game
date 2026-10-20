#ifndef PXL_CRUSH_COLOR_V2_INCLUDED
#define PXL_CRUSH_COLOR_V2_INCLUDED

float3 PxlV2LinearToSRGB(float3 color)
{
    float3 low = color * 12.92;
    float3 high = 1.055 * pow(max(color, 0.0), 1.0 / 2.4) - 0.055;
    return float3(color.r <= 0.0031308 ? low.r : high.r,
        color.g <= 0.0031308 ? low.g : high.g,
        color.b <= 0.0031308 ? low.b : high.b);
}

float3 PxlV2RGBToHSV(float3 color)
{
    float4 k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float4 p = lerp(float4(color.bg, k.wz), float4(color.gb, k.xy), step(color.b, color.g));
    float4 q = lerp(float4(p.xyw, color.r), float4(color.r, p.yzx), step(p.x, color.r));
    float chroma = q.x - min(q.w, q.y);
    return float3(abs(q.z + (q.w - q.y) / max(6.0 * chroma, 1e-10)),
        chroma / max(q.x, 1e-10), q.x);
}

// All color/address math stays float, including on mobile.
void PxlCrushPaletteV2_float(float3 Color, float Quantization, float Layers,
    UnityTexture2D Palette, out float3 Out)
{
    float3 rgb = saturate(Color);
#ifndef UNITY_COLORSPACE_GAMMA
    rgb = PxlV2LinearToSRGB(rgb);
#endif
    // Preserve hue and saturation before quantization. Only quantize value:
    // rounding individual RGB channels would repeatedly erase weak chroma.
    float3 hsv = PxlV2RGBToHSV(rgb);
    float levels = max(1.0, floor(Quantization + 0.5));
    hsv.z = saturate(floor(hsv.z * levels + 0.5) / levels);

    float2 size = max(float2(1.0, 1.0), floor(Palette.texelSize.zw + 0.5));
    float layers = clamp(floor(Layers + 0.5), 1.0, size.y);
    float rowsPerLayer = max(1.0, floor(size.y / layers));
    const float boundaryEpsilon = 1e-5;
    float layer = min(floor(saturate(hsv.y) * layers + boundaryEpsilon), layers - 1.0);
    float row = min(floor(hsv.z * rowsPerLayer), rowsPerLayer - 1.0);
    float column = min(floor(frac(hsv.x) * size.x + boundaryEpsilon), size.x - 1.0);
    float2 uv = (float2(column, layer * rowsPerLayer + row) + 0.5) / size;
    Out = SAMPLE_TEXTURE2D_LOD(Palette.tex, Palette.samplerstate, uv, 0).rgb;
}

#endif
