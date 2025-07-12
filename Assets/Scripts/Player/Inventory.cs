using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
	private static List<string> m_shootingObjects = new List<string>();

	public static void AddObject(string objectName)
	{
		Debug.Log(objectName);
		m_shootingObjects.Add(objectName);
	}
}
