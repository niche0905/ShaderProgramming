#version 330

layout(location = 0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;

in vec2 v_UV;
uniform float u_Time;

const float c_PI = 3.14159265359;

void main()
{
    float s = 0.01;
    vec2 newPos = v_UV;
    newPos += vec2(0, 0.2 * sin(2 * c_PI * v_UV.x + u_Time));

    vec4 newColor = texture(u_RGBTexture, newPos);

    FragColor = newColor;
    //FragColor = vec4(v_UV, 0, 1.0);
}
