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
		/// Score.
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI _score;

		#endregion

		#region Fields

		/// <summary>
		/// Is the timer currently running.
		/// </summary>
		private bool _isTimerRunning = true;
		
		/// <summary>
		/// Elapsed time.
		/// </summary>
		private float _elapsedTime;
		
		

		#endregion

		#region Methods

		/// <summary>
		/// Called every frame.q
		/// </summary>
		private void Update()
		{
			if (_isTimerRunning)
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
			//TODO: update score.
		}

		#endregion

	}
}
