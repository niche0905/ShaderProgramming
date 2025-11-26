#version 330

layout(location=0) out vec4 FragColor;

uniform sampler2D u_TexID;
const vec2 u_TexelSize = vec2(1/512, 1/512);

in vec2 v_Tex;

void main()
{
    vec2 tex = vec2(v_Tex.x, 1.0 - v_Tex.y);

    vec3 col = vec3(0.0);

    // 3x3 blur kernel
    for(int x = -1; x <= 1; x++)
    {
        for(int y = -1; y <= 1; y++)
        {
            vec2 offset = vec2(x, y) * u_TexelSize;
            col += texture(u_TexID, tex + offset).rgb;
        }
    }

    col /= 9.0;

    FragColor = vec4(col, 1.0);
}
