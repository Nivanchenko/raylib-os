#version 330

in vec3 worldPosition;
in vec4 baseColor;

uniform vec3 uLightDirection;
uniform int uMaterial;
uniform float uTime;

out vec4 finalColor;

void main()
{
    // This stage is a lighting probe only. Keep the required material/time
    // uniforms active and reject invalid renderer inputs without animating
    // any surface; material patterns are introduced in the later material pass.
    if (uMaterial < 1 || uMaterial > 3 || uTime < 0.0) discard;

    vec3 normal = normalize(cross(dFdx(worldPosition), dFdy(worldPosition)));
    if (!gl_FrontFacing) normal = -normal;

    float diffuse = max(dot(normal, normalize(uLightDirection)), 0.0);
    float lighting = 0.35 + 0.65 * diffuse;
    finalColor = vec4(baseColor.rgb * lighting, 1.0);
}
