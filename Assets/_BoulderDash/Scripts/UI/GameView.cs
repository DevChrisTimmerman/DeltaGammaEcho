using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace BoulderDash
{
	public class GameView : MonoBehaviour
	{

		#region EditorFields

		/// <summary>
		/// ElapsedTime.
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI _elapsedTimeText;
		
		/// <summary>
		/// Total level diamonds.
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI _totalLevelDiamonds;
		
		/// <summary>
		/// Collected diamonds.
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI _collectedDiamonds;

		#endregion

		#region Fields

		/// <summary>
		/// Elapsed time.
		/// </summary>
		private float _elapsedTime;

		#endregion

		#region Properties

		/// <summary>
		/// Total level diamonds.
		/// </summary>
		public TextMeshProUGUI TotalLevelDiamonds => _totalLevelDiamonds;

		/// <summary>
		/// Is the timer currently running.
		/// </summary>
		public bool IsTimerRunning { get; set; } = true;

		#endregion

		#region Initialization

		/// <summary>
		/// Called on start.
		/// </summary>
		private void Start()
		{
			_totalLevelDiamonds.text = GameManager.Instance.SceneSession.TotalLevelDiamonds.ToString();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Called every frame.q
		/// </summary>
		private void Update()
		{
			if (IsTimerRunning)
			{
				_elapsedTime += Time.deltaTime;
			}
			
			UpdateUI();
		}

		/// <summary>
		/// Used to update the UI.
		/// </summary>
		private void UpdateUI()
		{
			_elapsedTimeText.text = _elapsedTime.ToString("F");
			_collectedDiamonds.text = GameManager.Instance.CollectedDiamonds.ToString(CultureInfo.InvariantCulture);
		}

		#endregion

	}
}
