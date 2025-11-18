#version 330

layout(location = 0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;

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
    float width = 0.2 * (1 - v_UV.x);
    
    if (sinValue + width > newUV.y && sinValue - width < newUV.y)
    {
        newColor = vec4(1);
    }

    FragColor = newColor;
}

void main()
{
    Flag();
}
