using System;
using UnityEngine;

namespace BoulderDash
{
	public class Hover : MonoBehaviour
	{

		#region EditorFields

		/// <summary>
		/// Hover speed.
		/// </summary>
		[SerializeField]
		private float _speed;
		
		/// <summary>
		/// Hover distance.
		/// </summary>
		[SerializeField]
		private float _distance;

		#endregion

		private void Update()
		{
			Vector2 pos = transform.localPosition; 
			pos.y = _distance * Mathf.Sin(Time.realtimeSinceStartup * _speed);
			transform.localPosition = pos; 
			
		}
	}
}
