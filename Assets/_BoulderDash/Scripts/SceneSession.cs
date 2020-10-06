using System;
using UnityEngine;

namespace BoulderDash
{
	public class SceneSession : MonoBehaviour
	{

		#region Fields

		/// <summary>
		/// LevelDiamonds
		/// </summary>
		private Diamond[] _levelDiamonds;

		#endregion

		#region Properties

		/// <summary>
		/// Total level diamonds.
		/// </summary>
		public int TotalLevelDiamonds => _levelDiamonds.Length;

		#endregion

		#region Initialization

		/// <summary>
		/// Called on awake.
		/// </summary>
		private void Awake()
		{
			GameManager.Instance.SceneSession = this;
		}

		/// <summary>
		/// Called on start.
		/// </summary>
		private void Start()
		{
			_levelDiamonds = (Diamond[])FindObjectsOfType(typeof(Diamond));
		}

		#endregion
	
		#region Methods

	

		#endregion

	}
}
