using UnityEngine;

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
		
		/// <summary>
		/// UI manager.
		/// </summary>
		[SerializeField]
		private UIManager _uiManager;

		#endregion
		
		#region Fields

		/// <summary>
		/// The Game manager instance.
		/// </summary>
		private static GameManager _instance;

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

		#endregion
		
		
	}
}
