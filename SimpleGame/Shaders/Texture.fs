#version 330

layout(location=0) out vec4 FragColor;

uniform sampler2D u_TexID;
const vec2 u_TexelSize = vec2(1/512, 1/512);
const float u_Strength = 0.2;   // +볼록, -오목
const vec2  u_Center = vec2(0.5, 0.5);

in vec2 v_Tex;

void GaussianBlur()
{
    vec2 tex = vec2(v_Tex.x, 1.0 - v_Tex.y);

    vec3 col = vec3(0.0);

    // 3x3 blur kernel
    for(int x = -1; x <= 1; x++)
    {
        for(int y = -1; y <= 1; y++)
        {
            vec2 offset = vec2(x, y) * u_TexelSize;
            col += texture(u_TexID, tex + offset).rgb;
        }
    }

    col /= 9.0;

    FragColor = vec4(col, 1.0);
}

void Lens()
{
    // 렌즈 중심 기준 좌표 (-1 ~ 1)
    vec2 uv = v_Tex - u_Center;
    uv /= vec2(0.5, 0.5);  // 중심 기준 정규화 (-1~1)
    
    // 거리
    float r = length(uv);

    // 왜곡 계수
    // Convex(+): uv / (1 + k*r^2)
    // Concave(-): uv * (1 + |k|*r^2)
    float k = u_Strength;

    vec2 distortedUV;

    
    if (k > 0.0)
    {
        float distortion = 1.0 + k * r * r;
        distortedUV = uv / distortion;
    }
    else
    {
        float distortion = 1.0 - k * r * r; // k 음수라서 -k*r^2가 양수 증가
        distortedUV = uv * distortion;
    }

    // 렌즈 중심 기준, 원래 텍스처 범위로 변환
    distortedUV *= 0.5;
    distortedUV += u_Center;

    // Y 플립
    distortedUV.y = 1.0 - distortedUV.y;

    FragColor = texture(u_TexID, distortedUV);
}

void main()
{
    //GaussianBlur();
    Lens();
}