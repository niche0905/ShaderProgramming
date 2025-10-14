#version 330

in vec3 a_Position;

out vec4 v_Color;

uniform float u_Time;

void main()
{
	float val = 0.1;

	float x = a_Position.x;
	float theta = (x - (-0.5)) / (0.5 - (-0.5)) * 3.14 * 2;
	float dy = val * sin(theta);

	vec4 newPosition = vec4(a_Position.x, a_Position.y + dy, a_Position.z, 1);
	gl_Position = newPosition;

	v_Color = vec4(1);
}
