struct ScharrOperators
{
    float3x3 x;
    float3x3 y;
};

ScharrOperators GetEdgeDetectionKernels()
{
    ScharrOperators kernels;
    kernels.x = float3x3(-3, -10, -3, 0, 0, 0, 3, 10, 3);
    kernels.y = float3x3(-3, 0, 3, -10, 0, 10, -3, 0, 3);
    return kernels;
}

void DepthBasedOutlines_float(float2 screenUV, float2 px, out float outlines)
{
    outlines = 0;

#if defined(UNITY_DECLARE_DEPTH_TEXTURE_INCLUDED)

    float d00 = SampleSceneDepth(screenUV + px * float2(-1, -1));
    float d01 = SampleSceneDepth(screenUV + px * float2( 0, -1));
    float d02 = SampleSceneDepth(screenUV + px * float2( 1, -1));

    float d10 = SampleSceneDepth(screenUV + px * float2(-1,  0));
    float d12 = SampleSceneDepth(screenUV + px * float2( 1,  0));

    float d20 = SampleSceneDepth(screenUV + px * float2(-1,  1));
    float d21 = SampleSceneDepth(screenUV + px * float2( 0,  1));
    float d22 = SampleSceneDepth(screenUV + px * float2( 1,  1));

    float gx =
        -3*d00 -10*d01 -3*d02 +
         3*d20 +10*d21 +3*d22;

    float gy =
        -3*d00 +3*d02 +
        -10*d10 +10*d12 +
        -3*d20 +3*d22;

    float g = gx * gx + gy * gy;

    // ? Distance-aware boost (fixes missing far outlines)
    float centerDepth = SampleSceneDepth(screenUV);
    g *= (1.0 + centerDepth * 50.0);

    // ? Stable threshold
    outlines = smoothstep(0.0005, 0.002, g);

    // ? Sharpen
    outlines = outlines * outlines;

#endif
}

void NormalBasedOutlines_float(float2 screenUV, float2 px, out float outlines)
{
    outlines = 0;

#if defined(UNITY_DECLARE_NORMALS_TEXTURE_INCLUDED)

    float3 cn = SampleSceneNormals(screenUV);

    float3 n00 = SampleSceneNormals(screenUV + px * float2(-1, -1));
    float3 n01 = SampleSceneNormals(screenUV + px * float2( 0, -1));
    float3 n02 = SampleSceneNormals(screenUV + px * float2( 1, -1));

    float3 n10 = SampleSceneNormals(screenUV + px * float2(-1,  0));
    float3 n12 = SampleSceneNormals(screenUV + px * float2( 1,  0));

    float3 n20 = SampleSceneNormals(screenUV + px * float2(-1,  1));
    float3 n21 = SampleSceneNormals(screenUV + px * float2( 0,  1));
    float3 n22 = SampleSceneNormals(screenUV + px * float2( 1,  1));

    float e00 = 1 - dot(cn, n00);
    float e01 = 1 - dot(cn, n01);
    float e02 = 1 - dot(cn, n02);

    float e10 = 1 - dot(cn, n10);
    float e12 = 1 - dot(cn, n12);

    float e20 = 1 - dot(cn, n20);
    float e21 = 1 - dot(cn, n21);
    float e22 = 1 - dot(cn, n22);

    float gx =
        -3*e00 -10*e01 -3*e02 +
         3*e20 +10*e21 +3*e22;

    float gy =
        -3*e00 +3*e02 +
        -10*e10 +10*e12 +
        -3*e20 +3*e22;

    float g = gx * gx + gy * gy;

    outlines = smoothstep(0.01, 0.1, g);

    // ? Sharpen
    outlines = outlines * outlines;

#endif
}