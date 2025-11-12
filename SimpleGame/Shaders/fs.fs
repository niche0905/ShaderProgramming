#version 330

layout(location = 0) out vec4 FragColor;

in vec2 v_UV;
uniform sampler2D u_RGBTexture;
vec2 u_Resolution = vec2(600, 600);
uniform float u_Time;

// 필터 종류 (0 = 없음, 1 = 가우시안 블러, 2 = 그레이스케일, 3 = 반전, 4 = 셀 쉐이딩, 5 = 샤프닝, 6 = 엣지 검출)
int u_FilterType = 0;

float gaussian(float x, float sigma)
{
    return exp(-(x * x) / (2.0 * sigma * sigma));
}

vec3 gaussianBlur(vec2 uv)
{
    float radius = 20.0;
    float sigma = 10.0;
    vec2 texel = 1.0 / u_Resolution;
    vec3 sum = vec3(0.0);
    float total = 0.0;

    for (float x = -radius; x <= radius; x++)
    {
        for (float y = -radius; y <= radius; y++)
        {
            float weight = gaussian(length(vec2(x, y)), sigma);
            sum += texture(u_RGBTexture, uv + vec2(x, y) * texel).rgb * weight;
            total += weight;
        }
    }
    return sum / total;
}

vec3 grayscale(vec3 color)
{
    float g = dot(color, vec3(0.299, 0.587, 0.114));
    return vec3(g);
}

vec3 invert(vec3 color)
{
    return vec3(1.0) - color;
}

vec3 cellShading(vec3 color)
{
    float levels = 4.0;
    return floor(color * levels) / levels;
}

vec3 sharpen(vec2 uv)
{
    vec2 texel = 1.0 / u_Resolution;

    vec3 c = texture(u_RGBTexture, uv).rgb;
    vec3 n = texture(u_RGBTexture, uv + vec2(0.0, texel.y)).rgb;
    vec3 s = texture(u_RGBTexture, uv - vec2(0.0, texel.y)).rgb;
    vec3 e = texture(u_RGBTexture, uv + vec2(texel.x, 0.0)).rgb;
    vec3 w = texture(u_RGBTexture, uv - vec2(texel.x, 0.0)).rgb;

    vec3 blurred = (n + s + e + w + c) / 5.0;
    return clamp(c * 2.0 - blurred, 0.0, 1.0);
}

vec3 edgeDetect(vec2 uv)
{
    vec2 texel = 1.0 / u_Resolution;

    mat3 sobelX = mat3(
        -1, 0, 1,
        -2, 0, 2,
        -1, 0, 1
    );

    mat3 sobelY = mat3(
        -1, -2, -1,
         0,  0,  0,
         1,  2,  1
    );

    float gx = 0.0;
    float gy = 0.0;

    for (int i = -1; i <= 1; i++)
    {
        for (int j = -1; j <= 1; j++)
        {
            vec3 c = texture(u_RGBTexture, uv + vec2(i, j) * texel).rgb;
            float intensity = dot(c, vec3(0.299, 0.587, 0.114));
            gx += intensity * sobelX[j + 1][i + 1];
            gy += intensity * sobelY[j + 1][i + 1];
        }
    }

    float edge = length(vec2(gx, gy));
    return vec3(edge);
}

void main()
{
    vec3 color = texture(u_RGBTexture, v_UV).rgb;

    if (u_FilterType == 1)
        color = gaussianBlur(v_UV);
    else if (u_FilterType == 2)
        color = grayscale(color);
    else if (u_FilterType == 3)
        color = invert(color);
    else if (u_FilterType == 4)
        color = cellShading(color);
    else if (u_FilterType == 5)
        color = sharpen(v_UV);
    else if (u_FilterType == 6)
        color = edgeDetect(v_UV);

    FragColor = vec4(color, 1.0);
}
