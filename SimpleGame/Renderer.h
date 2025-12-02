#pragma once

#include <string>
#include <cstdlib>
#include <fstream>
#include <iostream>
#include <cassert>

#include "Dependencies\glew.h"
#include "LoadPng.h"

#define MAX_POINTS 100

class Renderer
{
public:
	Renderer(int windowSizeX, int windowSizeY);
	~Renderer();

	bool IsInitialized();
	void ReloadAllShaderPrograms();
	void DrawSolidRect(float x, float y, float z, float size, float r, float g, float b, float a);
	void DrawTest();
	void DrawParticle();
	void DrawGridMesh();
	void DrawFullScreenColor(float r, float g, float b, float a);
	void DrawFS();
	void DrawDebugTextures();
	void DrawFBOs();
	void DrawBloomParticle();

private:
	void Initialize(int windowSizeX, int windowSizeY);
	void DeleteAllShaderPrograms();
	void CompileAllShaderPrograms();
	bool ReadFile(char* filename, std::string *target);
	void AddShader(GLuint ShaderProgram, const char* pShaderText, GLenum ShaderType);
	GLuint CompileShaders(char* filenameVS, char* filenameFS);
	void CreateVertexBufferObjects();
	void CreateGridMesh(int x, int y);
	void GetGLPosition(float x, float y, float *newX, float *newY);
	void CreateParticles(int count);
	GLuint CreatePngTexture(char* filePath, GLuint samplingMethod);
	void DrawTexture(float x, float y, float sx, float sy, GLuint texID);
	void CreateFBOs();

	bool m_Initialized = false;
	
	unsigned int m_WindowSizeX = 0;
	unsigned int m_WindowSizeY = 0;

	GLuint m_VBORect = 0;
	GLuint m_SolidRectShader = 0;

	GLuint m_TestShader = 0;
	GLuint m_VBOTestPos = 0;
	GLuint m_VBOTestColor = 0;

	float m_Time = 0;

	// Particles
	GLuint m_VBOParticles = 0;
	GLuint m_VBOParticlesVertexCount = 0;
	GLuint m_ParticleShader = 0;

	// Grid mesh
	GLuint m_GridMeshVBO = 0;
	GLuint m_GridMeshVertexCount = 0;
	GLuint m_GridMeshShader = 0;

	// Rain drop
	int m_dropCount = 100;
	float m_Points[MAX_POINTS * 4];

	// Full screen
	GLuint m_FullScreenVBO = 0;
	GLuint m_FullScreenShader = 0;

	// fragment shader factory
	GLuint m_FSVBO = 0;
	GLuint m_FSShader = 0;

	// Textures
	GLuint m_RGBTexture = 0;
	GLuint m_UKTexture = 0;
	GLuint m_ParticleTexture = 0;
	GLuint m_numTextures[10] = {};
	GLuint m_TotalNumTextures = 0;

	// Texture
	GLuint m_TexVBO = 0;
	GLuint m_TexShader = 0;

	// FBOs
	GLuint m_FBO0 = 0;
	GLuint m_FBO1 = 0;
	GLuint m_FBO2 = 0;
	GLuint m_FBO3 = 0;
	GLuint m_FBO4 = 0;

	GLuint m_RTT0 = 0;
	GLuint m_RTT0_1 = 0;
	GLuint m_RTT1 = 0;
	GLuint m_RTT1_1 = 0;
	GLuint m_RTT2 = 0;
	GLuint m_RTT2_1 = 0;
	GLuint m_RTT3 = 0;
	GLuint m_RTT3_1 = 0;
	GLuint m_RTT4 = 0;
	GLuint m_RTT4_1 = 0;

	// HDR FBOs
	GLuint m_HDRFBO0 = 0;
	GLuint m_HDRRTT0_0 = 0;
	GLuint m_HDRRTT0_1 = 0;

	GLuint m_PingpongFBO[2] = { 0, };
	GLuint m_PingpongTexture[2] = { 0, };

};

