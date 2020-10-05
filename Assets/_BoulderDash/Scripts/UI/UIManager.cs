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
		
	}
}
