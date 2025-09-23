#version 330

in vec3 a_Position;	// attribute vec3 a_Position;와 같은 의미
in float a_Value;
in vec4 a_Color;
in float a_STime;
in vec3 a_Velocity;
out vec4 v_Color;	// fragment shader로 전달할 변수
					// varying vec4 v_Color;와 같은 의미

const float c_PI = 3.141592;
const vec2 c_G = vec2(0, -9.8);

uniform float u_Time;

void main()
{
	float newTime = u_Time - a_STime;
	vec4 newPosition = vec4(a_Position, 1);

	if (newTime > 0)
	{
		float t = fract(newTime / 2) * 2;
		float tt = t * t;
		float x = 0;
		float y = 0.5 * c_G.y * tt;
		newPosition.xy += vec2(x, y);
	}
	else 
	{
		newPosition.xy = vec2(-100000, 0);
	}

	gl_Position = newPosition;
	v_Color = a_Color;

}
