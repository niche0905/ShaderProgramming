#version 330

layout(location = 0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;

in vec2 v_UV;
uniform float u_Time;


void main()
{
    vec4 newColor = texture(u_RGBTexture, v_UV);

    FragColor = newColor;
    //FragColor = vec4(v_UV, 0, 1.0);
}
