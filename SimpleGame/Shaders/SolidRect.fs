#version 330

layout(location=0) out vec4 FragColor;
in vec4 v_Color;

uniform vec4 u_Color;

void main()
{
	//FragColor = vec4(u_Color.r, u_Color.g, u_Color.b, u_Color.a);
	if (v_Color.b < 0.5 && v_Color.r < 0.5)
		FragColor = v_Color;
	else
		discard;	// predication이 안되는 GPU에서는 성능 저하가 있을 수 있음
		
}
