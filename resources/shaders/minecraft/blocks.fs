#version 330

in vec3 worldPosition;
in vec4 baseColor;

uniform vec3 uLightDirection;
uniform int uMaterial;
uniform float uTime;

out vec4 finalColor;

float hashCell(vec3 cell)
{
    vec3 p = fract(cell * 0.1031);
    p += dot(p, p.yxz + 33.33);
    return fract((p.x + p.y) * p.z);
}

void main()
{
    if (uMaterial < 1 || uMaterial > 3) discard;

    vec3 normal = normalize(cross(dFdx(worldPosition), dFdy(worldPosition)));
    if (!gl_FrontFacing) normal = -normal;

    float diffuse = max(dot(normal, normalize(uLightDirection)), 0.0);
    float lighting = 0.35 + 0.65 * diffuse;
    float detail = 1.0;

    if (uMaterial == 1)
    {
        // Deterministic grain in world space stays attached to the voxel.
        float fineGrain = hashCell(floor(worldPosition * 12.0));
        float broadGrain = hashCell(floor(worldPosition * 3.0));
        detail = 0.88 + 0.20 * fineGrain + 0.06 * broadGrain;
    }
    else if (uMaterial == 2)
    {
        // Evaluate one continuous field for all adjoining water blocks.
        float waveX = sin(worldPosition.x * 2.4 + uTime * 1.1);
        float waveZ = cos(worldPosition.z * 2.1 - uTime * 1.3);
        float diagonalWave = sin((worldPosition.x + worldPosition.z) * 1.3 + uTime * 0.75);
        float wave = 0.5 + 0.25 * (waveX + waveZ);
        wave = clamp(wave + 0.12 * diagonalWave, 0.0, 1.0);
        float highlight = smoothstep(0.65, 0.92, wave);
        detail = 0.91 + 0.11 * wave + 0.08 * highlight;
    }
    else
    {
        // Long fibers run along Y; X/Z affect their phase and local variation.
        vec2 fiberCell = floor(worldPosition.xz * 2.0);
        float fiberOffset = (hashCell(vec3(fiberCell.x, 0.0, fiberCell.y)) - 0.5) * 1.2;
        float fibers = 0.5 + 0.5 * sin(worldPosition.y * 8.0 + fiberOffset);
        float knots = hashCell(floor(worldPosition * 5.0));
        detail = 0.82 + 0.21 * fibers + 0.05 * knots;
    }

    finalColor = vec4(baseColor.rgb * detail * lighting, 1.0);
}
