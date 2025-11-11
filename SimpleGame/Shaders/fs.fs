#version 330

layout(location = 0) out vec4 FragColor;

in vec2 v_UV;
uniform float u_Time;   // 초 단위 시간

const float PI = 3.141592;

// 파문의 중심 좌표 (UV 기준)
const vec2 CENTER = vec2(0.5, 0.5);

// 파문 관련 파라미터
const float WAVE_SPEED = 2.0;   // 파문이 퍼져나가는 속도
const float FREQUENCY = 20.0;   // 물결의 주기
const float AMPLITUDE = 0.03;   // 진폭 (파문의 강도)
const float DAMPING = 1.5;      // 감쇠 속도

void main()
{
    // 중심으로부터의 거리
    float dist = distance(v_UV, CENTER);

    // 시간에 따라 퍼지는 원형 파문 (sin 기반)
    float wave = sin(dist * FREQUENCY - u_Time * WAVE_SPEED);

    // 파문이 멀어질수록 약해지는 감쇠 효과
    wave *= exp(-dist * DAMPING);

    // 색상은 파문 높이에 따라 밝기 변화를 줌
    float gray = 0.5 + wave * AMPLITUDE;

    // 물 느낌을 주기 위해 약간의 푸른빛 추가
    vec3 color = vec3(0.1, 0.4, 0.7) * (0.8 + gray * 0.5);

    FragColor = vec4(color, 1.0);
}
