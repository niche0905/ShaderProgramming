#version 330
#define MAX_POINTS 3

in vec3 a_Position;

out vec4 v_Color;

uniform float u_Time;

const float c_PI = 3.141592;
const vec2 c_Points[MAX_POINTS] = vec2[](vec2(0, 0),
							vec2(0.5, 0),
							vec2(-0.5, 0));

void Flag()
{
	vec4 newPosition = vec4(a_Position, 1);

	float value = (a_Position.x + 0.5) * 2 * c_PI;
	float value1 = a_Position.x + 0.5;
	float dx = 0;
	float dy = value1 * 0.3 * sin(value - u_Time * 10);

	newPosition.y *= (1 - value1);
	newPosition.xy += vec2(dx, dy);

	float shading = (sin(value - u_Time * 10) + 1) / 2 + 0.2;

	gl_Position = newPosition;

	v_Color = vec4(shading);
}

void Wave()
{
	vec4 newPosition = vec4(a_Position, 1);
	gl_Position = newPosition;

	float d = distance(a_Position.xy, vec2(0, 0));

	float value = sin(d * 4 * c_PI * 10 - u_Time * c_PI * 2 * 5);
	float p = 1 - clamp(d * 2, 0, 1);
	v_Color = vec4(value * p);
}

void RainDrop()
{
	vec4 newPosition = vec4(a_Position, 1);
	gl_Position = newPosition;

	vec2 pos = a_Position.xy;

	float newColor = 0;
	for (int i = 0; i < MAX_POINTS; ++i)
	{
		vec2 cen = c_Points[i];
		float d = distance(pos, cen);
		float value = sin(d * 4 * c_PI * 10 - u_Time * c_PI * 2 * 10);
		float p = clamp(0.5 - d, 0, 1);
		newColor += value * p;
	}

	v_Color = vec4(newColor);
}

void main()
{
	//Wave();
	RainDrop();
}
