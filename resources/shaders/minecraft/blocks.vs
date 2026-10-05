#version 330

in vec3 vertexPosition;
in vec4 vertexColor;

uniform mat4 mvp;

out vec3 worldPosition;
out vec4 baseColor;

void main()
{
    // DrawCube uses world-space vertices and has no per-cube model matrix.
    worldPosition = vertexPosition;
    baseColor = vertexColor;
    gl_Position = mvp * vec4(vertexPosition, 1.0);
}
