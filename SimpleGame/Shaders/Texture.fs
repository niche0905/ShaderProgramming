#version 330

layout(location=0) out vec4 FragColor;

uniform sampler2D u_TexID;

in vec2 v_Tex;


void main()
{
	//FragColor = vec4(v_Tex, 0, 1);
	FragColor = texture(u_TexID, v_Tex);
}
