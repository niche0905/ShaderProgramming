#version 330

in vec3 a_Position;	// attribute vec3 a_Position;와 같은 의미
in vec4 a_Color;
out vec4 v_Color;	// fragment shader로 전달할 변수
					// varying vec4 v_Color;와 같은 의미
void main()
{
	vec4 newPosition = vec4(a_Position, 1);
	vec4 subPosition = vec4(0.5, 0.5, 0.0, 0.0);
	// or newPosition.xy = newPosition.xy - vec2(0.5, 0.5);
	gl_Position = (newPosition - subPosition);

	v_Color = a_Color;
}
