#version 330

layout(location = 0) out vec4 FragColor;

in vec2 v_UV;
uniform float u_Time;

const float PI = 3.141592;

// 파문 파라미터
const float WAVE_SPEED = 2.5;
const float FREQUENCY = 15.0;
const float AMPLITUDE = 0.12;
const float DAMPING = 1.5;

// 한 번에 보이는 최대 빗방울 수
const int NUM_DROPS = 6;

// 간단한 의사랜덤 함수 (시간 + 인덱스로 위치 생성)
float hash(float n) {
    return fract(sin(n * 1234.567) * 43758.5453);
}

// 2D 랜덤 벡터
vec2 randomCenter(float seed) {
    return vec2(hash(seed * 1.3), hash(seed * 7.7));
}

void main()
{
    float totalWave = 0.0;

    // 주기적으로 빗방울이 떨어지는 타이밍 계산
    float period = 6; // 새로운 빗방울이 생기는 주기 (초)
    float timeCycle = u_Time / period;

    for (int i = 0; i < NUM_DROPS; i++)
    {
        // 빗방울마다 다른 시간 오프셋
        float dropIndex = floor(timeCycle) - float(i);
        float dropTime = fract(timeCycle - float(i) * 0.37); // 시간 오프셋 기반 반복
        float dropSeed = dropIndex + float(i) * 10.0;

        vec2 center = randomCenter(dropSeed);

        // 현재 파문까지의 시간 (0 ~ 1)
        float t = dropTime * period;

        // 중심으로부터 거리 계산
        float dist = distance(v_UV, center);

        // 파문 계산
        float wave = sin(dist * FREQUENCY - t * WAVE_SPEED);
        wave *= exp(-dist * DAMPING);
        wave *= exp(-t * 0.5);  // 시간 감쇠

        totalWave += wave;
    }

    float gray = 0.5 + totalWave * AMPLITUDE;

    // 물색 베이스
    vec3 baseColor = vec3(0.1, 0.4, 0.7);
    vec3 color = baseColor * (0.8 + gray * 0.5);

    FragColor = vec4(color, 1.0);
}
