using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFps : MonoBehaviour
{
	public int frameRate = 45;

	float deltaTime = 0.0f;

	void Awake()
	{
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = frameRate;
	}

	void Update()
	{
		Application.targetFrameRate = frameRate;
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}
}
