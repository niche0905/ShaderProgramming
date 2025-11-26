#version 330

layout(location=0) out vec4 FragColor;

uniform sampler2D u_TexID;
const vec2 u_TexelSize = vec2(1/512, 1/512);
const float u_Strength = 0.2;   // +볼록, -오목
const vec2  u_Center = vec2(0.5, 0.5);

const float u_Chromatic = 1;     // 색수차 강도 (0.0 ~ 3.0)
const float u_Scanline = 0.05;      // 스캔라인 강도 (0.0 ~ 1.0)
const float u_Curvature = 0.05;     // 화면 곡면 왜곡 정도 (0.0 ~ 0.3)
const float u_Vignette = 0.05;      // 비네팅 강도 (0.0 ~ 1.0)

in vec2 v_Tex;

// 화면 곡면 왜곡 (배럴 왜곡)
vec2 curveUV(vec2 uv, float curvature)
{
    uv = uv * 2.0 - 1.0;  // -1~1

    float r2 = dot(uv, uv);
    uv *= 1.0 + curvature * r2;

    return (uv + 1.0) * 0.5;
}

void TV()
{
    // --- 1. 곡면 왜곡 적용 ---
    vec2 uv = curveUV(v_Tex, u_Curvature);

    // --- 2. 색수차 적용 ---
    // R, G, B를 서로 다른 UV로 샘플링
    float shift = u_Chromatic * 0.002;

    float r = texture(u_TexID, uv + vec2( shift, 0.0 )).r;
    float g = texture(u_TexID, uv).g;
    float b = texture(u_TexID, uv + vec2(-shift, 0.0 )).b;
    vec3 color = vec3(r, g, b);


    // --- 3. 가로 스캔라인 ---
    // y 방향으로 강도 변화
    float scan = sin(uv.y * 1080.0) * u_Scanline * 0.15;
    color -= scan;


    // --- 4. 비네팅(Vignette) ---
    vec2 centered = uv * 2.0 - 1.0;
    float vign = 1.0 - dot(centered, centered);
    vign = clamp(vign, 0.0, 1.0);
    color *= mix(1.0, vign, u_Vignette);


    // --- 5. 최종 색 ---
    FragColor = vec4(color, 1.0);
}

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
    //Lens();
    TV();
}