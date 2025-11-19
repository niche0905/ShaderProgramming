#version 330

layout(location = 0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;
uniform sampler2D u_NumTexture;
uniform sampler2D u_TotalNumTexture;
uniform int u_Number;

in vec2 v_UV;
uniform float u_Time;

const float c_PI = 3.14159265359;

void Test()
{
    float s = 0.01;
    vec2 newPos = v_UV;
    newPos += vec2(0, 0.2 * sin(2 * c_PI * v_UV.x + u_Time));

    vec4 newColor = texture(u_RGBTexture, newPos);

    FragColor = newColor;
    //FragColor = vec4(v_UV, 0, 1.0);
}

void Circles()
{
    vec2 newUV = v_UV;
    vec2 center = vec2(0.5, 0.5);
    float d = distance(newUV, center);
    vec4 newColor = vec4(0);
    
    float value = sin(d * 4 * c_PI * 8 - u_Time * 5);
    newColor = vec4(value);

    FragColor = newColor;
}

void Flag()
{
    vec2 newUV = vec2(v_UV.x, (1 - v_UV.y) - 0.5);
    float sinValue = v_UV.x * 0.2 * sin(v_UV.x * 2 * c_PI - u_Time);
    vec4 newColor = vec4(0);
    float width = 0.2 * abs(sin((1 - v_UV.x) * 1 * c_PI));
    
    if (sinValue + width > newUV.y && sinValue - width < newUV.y)
    {
        newColor = vec4(1);
    }
    else
    {
        discard;
    }

    FragColor = newColor;
}

void Q1()
{
    float newX = v_UV.x;
    float newY = 1 - abs((v_UV.y * 2) - 1);
    //float newY = v_UV.y * 2;
    //if (newY > 1.0)
    //{
    //    newY = 2 - newY;
    //}

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q2()
{
    float newX = fract(v_UV.x * 3);
    float newY = v_UV.y / 3 + (2 - floor(v_UV.x * 3)) / 3;
    //float newY = v_UV.y / 3 + floor(3 - v_UV.x * 3)) / 3;

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q3()
{
    float newX = fract(v_UV.x * 3);
    float newY = v_UV.y / 3 + floor(v_UV.x * 3) / 3;

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q4()
{
    float newX = v_UV.x;
    float newY = fract(v_UV.y * 3) / 3 + (2 - floor(v_UV.y * 3)) / 3;

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q5()
{
    float newX = 2 * v_UV.x + (1 - floor(v_UV.y * 2)) / 2;
    float newY = 2 * v_UV.y;

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q6()
{
    float newX = 2 * v_UV.x;
    float newY = 2 * v_UV.y + floor(v_UV.x * 2) * 3 / 2;

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q7()
{
    float newX = fract(v_UV.x * 3);
    float newY = v_UV.y / 3 + floor(v_UV.x * 3) * 2 / 3;

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q8()
{
    float newX = fract(v_UV.x * 3);
    float newY = v_UV.y / 3 + (floor(v_UV.x * 3) * 2 + 1) / 3;

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q33()
{
    float newX = fract(v_UV.x * 3);
    float newY = floor(v_UV.x * 3) / 3 + v_UV.y / 3;

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q44()
// maybe 5
{
    float count = 8;    // uniform
    float shift = 0.1 * u_Time;  // uniform

    float newX = fract(fract(v_UV.x * count) + (floor(v_UV.y * count) + 1) * shift);
    float newY = fract(v_UV.y * count);

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q55()
{
    float count = 2;    // uniform
    float shift = 0.5;  // uniform

    float newX = fract(v_UV.x * count);
    float newY = fract(fract(v_UV.y * count) + floor(v_UV.x * count) * shift);

    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Number()
{
    FragColor = texture(u_NumTexture, v_UV);
}

void TotalNumber()
{
    // u_UV // 0~1 --> x: 0->0 1->1/5, y: 0->0 1->1/2
    int tileIndex = (u_Number + 9) % 10; // 0~9)

    const float cols = 5.0;
    const float rows = 2.0;

    float col = tileIndex % 5;
    float row = tileIndex / 5;

    vec2 tileSize = vec2(1.0/ cols, 1.0/ rows);
    vec2 tileOffset = vec2(col, row) * tileSize;
    
    vec2 atlasUV = tileOffset + v_UV * tileSize;

    FragColor = texture(u_TotalNumTexture, atlasUV);
}

vec4 GetDigitColor(int digit, vec2 uv)
{
    int tileIndex = (digit == 0) ? 9 : (digit - 1);

    const float cols = 5.0;
    const float rows = 2.0;

    float col = float(tileIndex % 5);
    float row = float(tileIndex / 5);

    vec2 tileSize = vec2(1.0 / cols, 1.0 / rows);
    vec2 tileOffset = vec2(col * tileSize.x, row * tileSize.y);

    vec2 atlasUV = tileOffset + uv * tileSize;

    return texture(u_TotalNumTexture, atlasUV);
}

void FiveNumber()
{
    // 1) u_Number를 각 숫자로 나누기
    int digit0 = (u_Number / 10000) % 10;
    int digit1 = (u_Number / 1000)  % 10;
    int digit2 = (u_Number / 100)   % 10;
    int digit3 = (u_Number / 10)    % 10;
    int digit4 = (u_Number / 1)     % 10;

    // 2) 어떤 digit을 그릴 지 선택
    float segW = 1.0 / 5.0;
    int digit;

    if(v_UV.x < segW * 1.0)
        digit = digit0;
    else if(v_UV.x < segW * 2.0)
        digit = digit1;
    else if(v_UV.x < segW * 3.0)
        digit = digit2;
    else if(v_UV.x < segW * 4.0)
        digit = digit3;
    else
        digit = digit4;

    // 3) 각 digit 영역 내에서 로컬 UV 구하기
    float localX = fract(v_UV.x * 5.0); // 분리된 타일 내부(0~1)
    vec2 localUV = vec2(localX, v_UV.y);

    // 4) 선택된 digit 타일 색상
    FragColor = GetDigitColor(digit, localUV);
}

void main()
{
    FiveNumber();
}
