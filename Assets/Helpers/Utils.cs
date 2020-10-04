using System;
using System.Collections;
using UnityEngine;

public static class Utils
{

	#region Methods

	/// <summary>
	/// Draw debug scanner.
	/// </summary>
	/// <param name="position">Position</param>
	/// <param name="size">Size</param>
	public static void DebugDrawScanner(Vector3 position, float size)
	{
		Debug.DrawLine(new Vector3(position.x - size, position.y, position.z), new Vector3(position.x + size, position.y, position.z), Color.red);
		Debug.DrawLine(new Vector3(position.x, position.y - size, position.z), new Vector3(position.x, position.y + size, position.z), Color.red);
		Debug.DrawLine(new Vector3(position.x - Mathf.Sin(45) * size, position.y - Mathf.Cos(45) * size, position.z), new Vector3(position.x + Mathf.Sin(45) * size, position.y + Mathf.Cos(45) * size, position.z), Color.red);
		Debug.DrawLine(new Vector3(position.x - Mathf.Sin(45) * size, position.y + Mathf.Cos(45) * size, position.z), new Vector3(position.x + Mathf.Sin(45) * size, position.y - Mathf.Cos(45) * size, position.z), Color.red);
	}

	/// <summary>
	/// Da something after amount of seconds.
	/// </summary>
	public static IEnumerator DoAfterTime(Action action, float delay)
	{
		if (delay <= 0) yield return null;
		else yield return new WaitForSeconds(delay);
		action.Invoke();
	}

	#endregion
	
}
