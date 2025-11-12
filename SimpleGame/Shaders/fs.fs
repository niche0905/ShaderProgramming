#version 330

layout(location = 0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;
in vec2 v_UV;

vec2 u_Resolution = vec2(600, 600);
uniform float u_Time;

const float blurRadius = 20.0; // 20px 범위
const float sigma = 5.0;      // 가우시안 표준편차 (조절 가능)

float gaussian(float x, float sigma)
{
    return exp(-(x * x) / (2.0 * sigma * sigma));
}

void main()
{
    vec2 texelSize = 1.0 / u_Resolution; // 텍셀 단위
    vec3 color = vec3(0.0);
    float totalWeight = 0.0;

    // 20px 범위를 기준으로 가우시안 샘플
    for (float x = -blurRadius; x <= blurRadius; x++)
    {
        for (float y = -blurRadius; y <= blurRadius; y++)
        {
            float weight = gaussian(length(vec2(x, y)), sigma);
            vec2 offset = vec2(x, y) * texelSize;
            color += texture(u_RGBTexture, v_UV + offset).rgb * weight;
            totalWeight += weight;
        }
    }

    color /= totalWeight;
    FragColor = vec4(color, 1.0);
}
