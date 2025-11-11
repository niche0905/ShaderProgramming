#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;
uniform float u_Time;   // 초 단위 시간

const int MAX_ITER = 100;
const vec2 CENTER = vec2(-0.5, 0.0);

void main()
{
    // 시간 기반 줌 애니메이션 (sin 파형으로 부드럽게)
    float zoom = 1.5 + sin(u_Time * 0.5) * 1.0; // 0.5Hz 주기로 확대/축소
    float scale = 1.0 / zoom;                   // 확대 시 더 세밀하게

    // UV -> 프랙탈 좌표 변환
    vec2 c = (v_UV - 0.5) * 2.0 * scale + CENTER;

    vec2 z = vec2(0.0);
    int iter = 0;

    for (int i = 0; i < MAX_ITER; i++)
    {
        float x = (z.x * z.x - z.y * z.y) + c.x;
        float y = (2.0 * z.x * z.y) + c.y;

        if (x * x + y * y > 4.0)
            break;

        z = vec2(x, y);
        iter++;
    }

    float t = float(iter) / float(MAX_ITER);
    vec3 color = vec3(t * 0.6, t * t, t * 0.8);  // 그라데이션

    FragColor = vec4(color, 1.0);
}
