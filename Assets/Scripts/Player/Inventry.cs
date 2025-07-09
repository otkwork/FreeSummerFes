using System.Collections.Generic;
using UnityEngine;

public class Inventry
{
	private static List<string> m_shootingObjects = new List<string>();

	public static void AddObject(string objectName)
	{
		Debug.Log(objectName);
		m_shootingObjects.Add(objectName);
	}
}
