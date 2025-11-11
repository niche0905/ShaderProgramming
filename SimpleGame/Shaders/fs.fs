#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;


void main()
{
	vec4 newColor = vec4(0);
	if (v_UV.x > 0.5)
	{
		newColor.r = 1;
	}
	else
	{
		newColor.g = 1;
	}
	FragColor = newColor;
}
