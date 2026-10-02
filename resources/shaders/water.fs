#version 330

in vec2 fragTexCoord;
in vec4 fragColor;
out vec4 finalColor;

uniform float uTime;
uniform vec2 uResolution;
uniform vec2 uMouse;       // Last click, 0..1, origin at bottom left
uniform float uRipple;     // Strength, fading after the button is released
uniform float uRippleAge;  // Seconds since that click

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

float clouds(vec2 p)
{
    float large = noise(p);
    float medium = noise(p * 2.07 + 9.4);
    float small = noise(p * 4.13 - 5.1);
    return large * 0.57 + medium * 0.29 + small * 0.14;
}

void main()
{
    vec2 uv = gl_FragCoord.xy / uResolution;
    float aspect = uResolution.x / uResolution.y;
    float horizon = 0.55;
    vec2 sun = vec2(0.72, 0.74);
    vec2 skyPos = vec2((uv.x - sun.x) * aspect, uv.y - sun.y);
    float sunDistance = length(skyPos);

    // Indigo dusk fades into a glowing peach horizon. Clouds are layered
    // rather than drawn as smooth oval patches.
    vec3 sky = mix(vec3(0.98, 0.42, 0.27), vec3(0.19, 0.25, 0.46),
                   smoothstep(horizon, 0.88, uv.y));
    sky = mix(sky, vec3(0.025, 0.055, 0.14), smoothstep(0.78, 1.08, uv.y));
    vec2 cloudPos = vec2(uv.x * 4.5 + uTime * 0.012, uv.y * 11.0);
    float cloud = clouds(cloudPos);
    float cover = smoothstep(0.52, 0.63, cloud)
                * smoothstep(horizon + 0.025, 0.76, uv.y);
    vec3 cloudShade = mix(vec3(0.47, 0.25, 0.32), vec3(0.08, 0.11, 0.23),
                          smoothstep(0.62, 1.0, uv.y));
    sky = mix(sky, cloudShade, cover * 0.76);
    sky += vec3(1.0, 0.42, 0.17) * exp(-sunDistance * 6.0) * 0.42;
    sky += vec3(1.0, 0.64, 0.34) * exp(-sunDistance * 24.0) * 0.42;
    float disk = 1.0 - smoothstep(0.036, 0.044, sunDistance);
    sky = mix(sky, vec3(1.0, 0.90, 0.70), disk);

    // Perspective compression makes wavelets smaller near the horizon.
    float depth = max(horizon - uv.y, 0.0);
    float mouseDepth = horizon - uMouse.y;
    vec2 ripplePos = vec2((uv.x - 0.5) * aspect * 1.2, 2.4) / (depth + 0.19);
    vec2 rippleOrigin = vec2((uMouse.x - 0.5) * aspect * 1.2, 2.4)
                      / (max(mouseDepth, 0.0) + 0.19);
    vec2 fromRipple = ripplePos - rippleOrigin;
    float radius = length(fromRipple);

    // Integrating slow gusts keeps the phase moving forward while its speed
    // changes; a gentle shear changes the direction of travel as well.
    float windTime = uTime + 1.8 * sin(uTime * 0.25) + 0.8 * sin(uTime * 0.11);
    vec2 p = vec2((uv.x - 0.5) * aspect * 7.0, 2.4) / (depth + 0.19);
    p.x += p.y * 0.07 * sin(uTime * 0.14);
    p.x += sin(p.y * 0.43 + windTime * 0.42) * 0.34;
    p.y += sin(p.x * 0.62 - windTime * 0.31) * 0.25;
    // Bend the advancing front around the underlying wind-driven swells.
    float front = radius - uRippleAge * 0.85
                + 0.09 * sin(p.y * 1.65 + p.x * 0.47 - windTime * 1.15);
    float ripple = sin(front * 18.0) * exp(-abs(front) * 1.5)
                 * exp(-uRippleAge * 0.27) * uRipple
                 * (1.0 - step(horizon, uMouse.y));
    vec2 radial = fromRipple / max(radius, 0.001);
    p += vec2(radial.x * 2.0, radial.y) * ripple * 0.36;
    float swell = 0.5 + 0.5 * sin(p.y * 1.65 + p.x * 0.47 - windTime * 1.15);
    float wave = 0.5 + 0.5 * sin(p.y * 4.65 + p.x * 0.60
                                + sin(p.x * 1.3 + windTime) * 0.9 - windTime * 2.1);
    float detail = 0.5 + 0.5 * sin(p.y * 11.2 - p.x * 2.1 + windTime * 3.2);
    float crest = smoothstep(0.67, 0.91, swell * 0.34 + wave * 0.48
                                         + detail * 0.18 + ripple * 0.17);
    float foam = smoothstep(0.89, 0.99, swell * 0.29 + wave * 0.55
                                        + detail * 0.16 + ripple * 0.14);

    vec3 sea = mix(vec3(0.12, 0.29, 0.37), vec3(0.012, 0.12, 0.19),
                   smoothstep(0.0, 0.62, depth));
    sea += vec3(0.08, 0.23, 0.25) * swell * 0.60;
    sea = mix(sea, vec3(0.20, 0.54, 0.57), crest * (0.12 + depth * 0.77));
    sea += vec3(0.28, 0.55, 0.51) * foam * depth * 0.24;
    sea += vec3(0.12, 0.29, 0.29) * ripple * (0.55 + 0.45 * wave);

    // Tiny, broken highlights glitter within the sun's widening reflection.
    float trailX = uv.x - sun.x + 0.015 * sin(p.y * 0.78 + windTime * 0.9);
    float trailWidth = 0.018 + 0.32 * depth;
    float trail = exp(-pow(trailX / trailWidth, 2.0));
    float flecks = noise(vec2(p.x * 0.85, p.y * 2.8) + vec2(0.0, -windTime * 1.3));
    float sparkle = smoothstep(0.64, 0.86, wave * 0.40 + detail * 0.37
                                              + flecks * 0.23);
    float reflection = trail * (0.10 + 1.7 * sparkle + foam * 0.7);
    reflection *= smoothstep(0.0, 0.13, depth);
    sea += vec3(1.0, 0.57, 0.27) * reflection * 0.88;
    sea += vec3(1.0, 0.87, 0.60) * pow(sparkle, 3.0) * trail * 0.76;

    float mist = exp(-depth * 29.0);
    sea = mix(sea, vec3(0.96, 0.54, 0.39), mist * 0.32);
    vec3 color = mix(sea, sky, smoothstep(horizon - 0.002, horizon + 0.002, uv.y));
    float vignette = 1.0 - 0.30 * dot((uv - 0.5) * vec2(0.85, 1.0),
                                      (uv - 0.5) * vec2(0.85, 1.0));
    finalColor = vec4(clamp(color * vignette, 0.0, 1.0), 1.0) * fragColor;
}
