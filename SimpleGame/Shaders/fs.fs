#version 330

layout(location = 0) out vec4 FragColor;

in vec2 v_UV;
uniform float u_Time;   // 초 단위 시간

const float PI = 3.141592;

// 물결 기본 파라미터
const float WAVE_SPEED = 2.0;
const float FREQUENCY = 20.0;
const float AMPLITUDE = 0.12;
const float DAMPING = 1.5;

// 여러 파문 설정 (최대 5개)
const int NUM_DROPS = 5;
const vec2 DROP_CENTERS[NUM_DROPS] = vec2[NUM_DROPS](
    vec2(0.5, 0.5),
    vec2(0.3, 0.7),
    vec2(0.7, 0.4),
    vec2(0.25, 0.3),
    vec2(0.8, 0.8)
);

// 각 물방울이 떨어지는 시간 오프셋
const float DROP_TIME_OFFSETS[NUM_DROPS] = float[NUM_DROPS](
    0.0,  // 첫 번째 즉시
    1.5,  // 두 번째 1.5초 후
    3.0,  // 세 번째 3초 후
    4.5,  // 네 번째 4.5초 후
    6.0   // 다섯 번째 6초 후
);

void main()
{
    float totalWave = 0.0;

    // 모든 물방울의 파문을 합산
    for (int i = 0; i < NUM_DROPS; i++)
    {
        float timeSinceDrop = u_Time - DROP_TIME_OFFSETS[i];
        if (timeSinceDrop > 0.0)
        {
            float dist = distance(v_UV, DROP_CENTERS[i]);
            float wave = sin(dist * FREQUENCY - timeSinceDrop * WAVE_SPEED);
            wave *= exp(-dist * DAMPING);
            wave *= exp(-timeSinceDrop * 0.5);  // 시간에 따라 점점 사라짐
            totalWave += wave;
        }
    }

    // 합산된 파문으로 밝기 변조
    float gray = 0.5 + totalWave * AMPLITUDE;
    vec3 baseColor = vec3(0.1, 0.4, 0.7);   // 물색
    vec3 color = baseColor * (0.8 + gray * 0.5);

    FragColor = vec4(color, 1.0);
}
