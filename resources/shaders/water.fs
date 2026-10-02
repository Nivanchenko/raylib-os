#version 330

in vec2 fragTexCoord;
in vec4 fragColor;
out vec4 finalColor;

uniform float uTime;
uniform vec2 uResolution;
uniform vec2 uMouse;       // 0..1, origin at bottom left
uniform float uRipple;     // 1 while left button is held

float hash(vec2 p)
{
    return fract(sin(dot(p, vec2(127.1, 311.7))) * 43758.5453);
}

float noise(vec2 p)
{
    vec2 cell = floor(p);
    vec2 f = fract(p);
    f = f * f * (3.0 - 2.0 * f);
    return mix(mix(hash(cell), hash(cell + vec2(1.0, 0.0)), f.x),
               mix(hash(cell + vec2(0.0, 1.0)), hash(cell + vec2(1.0, 1.0)), f.x), f.y);
}

void main()
{
    vec2 uv = gl_FragCoord.xy / uResolution;
    float aspect = uResolution.x / uResolution.y;
    float horizon = 0.55;
    vec2 sun = vec2(0.72, 0.74);
    vec2 skyPos = vec2((uv.x - sun.x) * aspect, uv.y - sun.y);
    float sunDistance = length(skyPos);

    // A warm horizon, drifting clouds and a soft solar halo.
    vec3 sky = mix(vec3(0.99, 0.56, 0.43), vec3(0.07, 0.17, 0.37),
                   smoothstep(horizon, 1.07, uv.y));
    float cloud = noise(vec2(uv.x * 5.0 + uTime * 0.014, uv.y * 9.0));
    cloud += 0.5 * noise(vec2(uv.x * 11.0 - uTime * 0.01, uv.y * 18.0));
    sky = mix(sky, vec3(0.58, 0.28, 0.35),
              smoothstep(0.76, 1.10, cloud) * 0.37 * smoothstep(horizon, 0.72, uv.y));
    sky += vec3(1.0, 0.56, 0.24) * exp(-sunDistance * 9.0) * 0.42;
    float disk = 1.0 - smoothstep(0.044, 0.051, sunDistance);
    sky = mix(sky, vec3(1.0, 0.89, 0.65), disk);

    // Perspective compression makes wavelets smaller near the horizon.
    float depth = max(horizon - uv.y, 0.0);
    vec2 p = vec2((uv.x - 0.5) * aspect * 7.0, 2.4) / (depth + 0.19);
    p.x += sin(p.y * 0.48 + uTime * 0.60) * 0.32;
    p.y += sin(p.x * 0.92 - uTime * 0.35) * 0.27;
    float swell = 0.5 + 0.5 * sin(p.y * 2.6 + p.x * 0.45 - uTime * 1.10);
    float wave = 0.5 + 0.5 * sin(p.y * 7.8 + sin(p.x * 1.8 + uTime) - uTime * 2.60);
    float detail = 0.5 + 0.5 * sin(p.y * 15.0 - p.x * 2.2 + uTime * 3.1);
    float crest = smoothstep(0.64, 0.94, swell * 0.36 + wave * 0.49 + detail * 0.15);

    vec3 sea = mix(vec3(0.045, 0.16, 0.22), vec3(0.015, 0.25, 0.32),
                   smoothstep(0.0, 0.55, depth));
    sea += vec3(0.13, 0.27, 0.30) * (swell * 0.22 + wave * 0.15);
    sea = mix(sea, vec3(0.27, 0.64, 0.66), crest * (0.12 + depth * 0.65));

    // A broken sun trail, stronger on the wave crests.
    float trailX = uv.x - sun.x + 0.018 * sin(p.y * 0.83 + uTime * 1.1);
    float trailWidth = 0.012 + 0.23 * depth;
    float trail = exp(-pow(trailX / trailWidth, 2.0));
    trail *= (0.12 + 0.88 * crest) * smoothstep(0.0, 0.28, depth);
    sea += vec3(1.0, 0.61, 0.29) * trail * 0.88;

    // Project both the fragment and mouse onto the water plane. A world-space
    // circle then appears flattened by perspective, especially near the horizon.
    float mouseDepth = horizon - uMouse.y;
    vec2 ripplePos = vec2((uv.x - 0.5) * aspect * 1.2, 2.4) / (depth + 0.19);
    vec2 rippleOrigin = vec2((uMouse.x - 0.5) * aspect * 1.2, 2.4)
                      / (max(mouseDepth, 0.0) + 0.19);
    float radius = length(ripplePos - rippleOrigin);
    float ring = sin(radius * 22.0 - uTime * 15.0);
    float rippleActive = uRipple * (1.0 - step(horizon, uMouse.y));
    sea += vec3(0.17, 0.42, 0.44) * rippleActive * ring
         * exp(-radius * 1.7) * smoothstep(0.02, 0.07, radius)
         * (0.65 + 0.35 * crest);

    float mist = exp(-depth * 44.0);
    sea = mix(sea, vec3(0.91, 0.60, 0.46), mist * 0.46);
    vec3 color = mix(sea, sky, smoothstep(horizon - 0.002, horizon + 0.002, uv.y));
    float vignette = 1.0 - 0.30 * dot((uv - 0.5) * vec2(0.85, 1.0),
                                      (uv - 0.5) * vec2(0.85, 1.0));
    finalColor = vec4(clamp(color * vignette, 0.0, 1.0), 1.0) * fragColor;
}
