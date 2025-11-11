#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;


void main()
{
	vec4 newColor = vec4(sin(2 * 3.14 * v_UV.x * 4));
	FragColor = newColor;
}
