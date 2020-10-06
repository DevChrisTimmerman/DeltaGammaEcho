using System;
using UnityEngine;

namespace BoulderDash
{
	public class UIManager : MonoBehaviour
	{

		#region EditorFields

		[SerializeField]
		private GameView _gameView;

		#endregion

		#region Properties

		/// <summary>
		/// Game view.
		/// </summary>
		public GameView GameView => _gameView;

		#endregion

		#region Initialization

		/// <summary>
		/// Called on start.
		/// </summary>
		private void Start()
		{
			GameManager.Instance.UIManager = this;
		}

		#endregion
		
	}
}
