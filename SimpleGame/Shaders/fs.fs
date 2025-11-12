#version 330

layout(location = 0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;

in vec2 v_UV;
uniform float u_Time;


void main()
{
    float s = 0.01;

    vec4 newColor = texture(u_RGBTexture, vec2(v_UV.x + 0.05 * sin(v_UV.y * 2 * 3.141592), v_UV.y));

    FragColor = newColor;
    //FragColor = vec4(v_UV, 0, 1.0);
}
