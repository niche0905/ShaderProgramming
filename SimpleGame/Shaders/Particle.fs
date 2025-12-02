#version 330

layout(location=0) out vec4 FragColor;
layout(location=1) out vec4 FragColor1;
in vec4 v_Color;
in vec2 v_Tex;
uniform sampler2D u_Texture;

uniform float u_Time;

void main()
{
	//FragColor = v_Color;
	//FragColor = texture(u_Texture, v_Tex) * v_Color;
	//FragColor1 = vec4(v_Tex, 0, 1);
	
	vec4 result = texture(u_Texture, v_Tex) * v_Color * 2;

	float brightness = dot(result.rgb, vec3(0.2126, 0.7152, 0.0722));
	FragColor = result;
	//FragColor = clamp(result, 0, 1);
	if(brightness > 1.0)
		FragColor1 = result - vec4(0);
	else
		FragColor1 = vec4(0);
}
