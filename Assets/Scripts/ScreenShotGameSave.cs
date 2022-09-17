using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class ScreenShotGameSave : MonoBehaviour
{
    public static void TakeScreenshotGameScreen(string SaveName)
	{
		var dateTimeOfTheScreenshot = DateTimeOfTheScreenshot();
		{
			Directory.CreateDirectory(ScreenshotPath);

			//while (File.Exists($"{ScreenshotPath}/{dateTimeOfTheScreenshot}")) { }
			//ScreenCapture.CaptureScreenshot($"{ScreenshotPath}/{dateTimeOfTheScreenshot}");

			while (File.Exists($"{Application.persistentDataPath}/{SaveName}")) { }
			ScreenCapture.CaptureScreenshot($"{Application.persistentDataPath}/{SaveName}.png");
		}
	}

	public static string DateTimeOfTheScreenshot()
	{
		return $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day} [{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}].png";
	}

	public static string ScreenshotPath { get; private set; } = "Assets/Resources/Screenshots";
}
