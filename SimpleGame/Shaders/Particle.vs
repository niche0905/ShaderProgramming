#version 330

in vec3 a_Position;	// attribute vec3 a_Position;와 같은 의미
in float a_Value;
in vec4 a_Color;
in float a_STime;
in vec3 a_Velocity;
in float a_LifeTime;
in float a_Mass;
in float a_Period;
in vec2 a_Tex;
out vec4 v_Color;	// fragment shader로 전달할 변수
					// varying vec4 v_Color;와 같은 의미
out vec2 v_Tex;

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
	vec4 centerC = vec4(1, 1, 1, 1);
	vec4 borderC = vec4(1, 0, 0, 1);
	vec4 newColor = a_Color;

	vec4 newPosition = vec4(a_Position, 1);
	float newAlpha = 1.f;
	float lifeTime = a_LifeTime;
	float amp = 2 * (a_Value) - 1;
	float period = a_Period * 2;

	float newTime = u_Time - a_STime;
	if (newTime > 0)
	{
		float t = fract(newTime / lifeTime) * lifeTime;
		float tt = t * t;
		float nTime = fract(newTime / lifeTime);
		float x = 2 * (t / lifeTime) - 1;
		float y = sin(nTime * c_PI) * amp * sin(period * 2 * c_PI * (t / lifeTime));

		newPosition.xy += vec2(x, y);
		newAlpha = 1 - t / lifeTime;

		float d = abs(y);
		newColor = mix(centerC, borderC, d);
	}
	else 
	{
		newPosition.xy = vec2(-100000, 0);
	}

	gl_Position = newPosition;
	v_Color = vec4(newColor.rgb, newAlpha);
}

void circleParticle()
{
	vec4 newPosition = vec4(a_Position, 1);
	float newAlpha = 1.f;
	float lifeTime = a_LifeTime;
	
	float radius = 0.7;
	
	float newTime = u_Time - a_STime;
	if (newTime > 0)
	{
		float theta = 2 * c_PI * a_Value;
		float x = radius * cos(theta);
		float y = radius * sin(theta);

		float t = fract(newTime / lifeTime) * lifeTime;
		float tt = t * t;

		float newX = x + 0.5 * c_G.x * tt;
		float newY = y + 0.5 * c_G.y * tt;

		newAlpha = 1 - t / lifeTime;

		newPosition.xy += vec2(newX, newY);
	}
	else 
	{
		newPosition.xy = vec2(-100000, 0);
	}


	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb * 5 * newAlpha, newAlpha);
}

void q1()
{
	vec4 newPosition = vec4(a_Position, 1);
	
	float value = a_Value * c_PI * 2;
	float dx = 2 * (a_Value - 0.5);
	float dy = sin(value);

	newPosition.xy += vec2(dx, dy);

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, 1);
}

void q2()
{
	vec4 newPosition = vec4(a_Position, 1);
	
	float value = a_Value * c_PI * 2;
	float dx = sin(value);
	float dy = cos(value) * fract(u_Time);

	newPosition.xy += vec2(dx, dy);

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, 1);
}

void q3()
{
	vec4 newPosition = vec4(a_Position, 1);
	
	float value = a_Value * c_PI * 2;
	float dx = a_Value * sin(value * 4 + u_Time);
	float dy = a_Value * cos(value * 4 + u_Time);

	newPosition.xy += vec2(dx, dy);

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, 1);
}

void main()
{
	circleParticle();

	v_Tex = a_Tex;
}
