using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoulderDash
{
	public class GameManager : MonoBehaviour
	{

		#region EditorFields

		/// <summary>
		/// Miner.
		/// </summary>
		[SerializeField]
		private MinerMovement _miner;

		#endregion
		
		#region Fields

		/// <summary>
		/// The Game manager instance.
		/// </summary>
		private static GameManager _instance;

		/// <summary>
		/// Collected diamonds.
		/// </summary>
		private int _collectedDiamonds;

		#endregion
		
		#region Properties

		/// <summary>
		/// GameManager singleton instance.
		/// </summary>
		public static GameManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<GameManager>();
				}
				return _instance;
			}
		}

		/// <summary>
		/// UI manager.
		/// </summary>
		public UIManager UIManager { get; set; }

		/// <summary>
		/// Collected diamonds.
		/// </summary>
		public int CollectedDiamonds
		{
			get => _collectedDiamonds;
			set => _collectedDiamonds = value;
		}

		/// <summary>
		/// Scene session.
		/// </summary>
		public SceneSession SceneSession { get; set; }

		#endregion

		#region Intialization

		/// <summary>
		/// Called on awake.
		/// </summary>
		private void Awake()
		{
			var test = GameManager.Instance;
			SceneManager.LoadScene("BoulderDash_UI", LoadSceneMode.Additive);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Diamond collected.
		/// </summary>
		public void DiamondCollected()
		{
			_collectedDiamonds++;
			if (SceneSession.TotalLevelDiamonds == _collectedDiamonds)
			{
				UIManager.GameView.IsTimerRunning = false;
			}
		}

		#endregion
		
		
	}
}
