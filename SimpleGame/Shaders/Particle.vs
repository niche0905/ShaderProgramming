#version 330

in vec3 a_Position;	// attribute vec3 a_Position;와 같은 의미
in float a_Value;
in vec4 a_Color;
in float a_STime;
in vec3 a_Velocity;
in float a_LifeTime;
in float a_Mass;
out vec4 v_Color;	// fragment shader로 전달할 변수
					// varying vec4 v_Color;와 같은 의미

const float c_PI = 3.141592;
const vec2 c_G = vec2(0, -9.8);

uniform float u_Time;
uniform vec3 u_Force;

void fountain()
{
	float lifeTime = a_LifeTime;
	float newAlpha = 1.f;
	float newTime = u_Time - a_STime;
	vec4 newPosition = vec4(a_Position, 1);

	if (newTime > 0)
	{
		vec3 acceleration = u_Force / a_Mass;

		float t = fract(newTime / lifeTime) * lifeTime;
		float tt = t * t;
		float x = a_Velocity.x * t + 0.5 * (c_G.x + acceleration.x) * tt;
		float y = a_Velocity.y * t + 0.5 * (c_G.y + acceleration.y) * tt;
		newPosition.xy += vec2(x, y);
		newAlpha = 1 - t / lifeTime;
	}
	else 
	{
		newPosition.xy = vec2(-100000, 0);
	}

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, newAlpha);
}

void sinParticle()
{
	vec4 newPosition = vec4(a_Position, 1);
	float newAlpha = 1.f;
	float lifeTime = a_LifeTime;

	float newTime = u_Time - a_STime;
	if (newTime > 0)
	{
		float t = fract(newTime / lifeTime) * lifeTime;
		float tt = t * t;
		float x = 2 * (t / lifeTime) - 1;
		float y = a_Value * sin(2 * c_PI * (t / lifeTime));

		newPosition.xy += vec2(x, y);
		newAlpha = 1 - t / lifeTime;
	}
	else 
	{
		newPosition.xy = vec2(-100000, 0);
	}



	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, newAlpha);
}

void main()
{
	sinParticle();
}
