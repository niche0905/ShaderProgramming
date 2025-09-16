#version 330

in vec3 a_Position;	// attribute vec3 a_Position;와 같은 의미
in vec4 a_Color;
out vec4 v_Color;	// fragment shader로 전달할 변수
					// varying vec4 v_Color;와 같은 의미

uniform float u_Time;

void main()
{
	float value = (2 * fract(u_Time)) - 1;
	float sinValue = sin(2 * 3.14 * fract(u_Time)) / 2;
	vec4 newPosition = vec4(a_Position, 1);
	newPosition.xy += vec2(value, sinValue);
	gl_Position = newPosition;

	v_Color = a_Color;
}
