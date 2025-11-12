#version 330

layout(location = 0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;

in vec2 v_UV;
uniform float u_Time;


void main()
{
    vec4 newColor = texture(u_RGBTexture, v_UV);

    float s = 0.01;
    newColor += texture(u_RGBTexture, vec2(v_UV.x - s * 1, v_UV.y));
    newColor += texture(u_RGBTexture, vec2(v_UV.x - s * 2, v_UV.y));
    newColor += texture(u_RGBTexture, vec2(v_UV.x - s * 3, v_UV.y));
    newColor += texture(u_RGBTexture, vec2(v_UV.x - s * 4, v_UV.y));
    newColor += texture(u_RGBTexture, vec2(v_UV.x + s * 1, v_UV.y));
    newColor /= 5;

    FragColor = newColor;
    //FragColor = vec4(v_UV, 0, 1.0);
}
