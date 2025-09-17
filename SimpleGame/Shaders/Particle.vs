#version 330

in vec3 a_Position;	// attribute vec3 a_Position;와 같은 의미
in float a_Value;
in vec4 a_Color;
out vec4 v_Color;	// fragment shader로 전달할 변수
					// varying vec4 v_Color;와 같은 의미

const float c_PI = 3.141592;

uniform float u_Time;

void main()
{
	vec4 newPosition = vec4(a_Position, 1);
	gl_Position = newPosition;

	v_Color = a_Color;
}
