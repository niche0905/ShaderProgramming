#version 330

layout(location=0) out vec4 FragColor;
in vec2 v_Tex;


void main()
{
	FragColor = vec4(v_Tex, 0, 1);		
}
