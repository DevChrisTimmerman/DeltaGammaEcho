using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoulderDash
{
	public class GameView : MonoBehaviour
	{

		#region EditorFields

		/// <summary>
		/// ElapsedTime.
		/// </summary>
		[SerializeField] private TextMeshProUGUI _elapsedTimeText;

		/// <summary>
		/// Total level diamonds.
		/// </summary>
		[SerializeField] private TextMeshProUGUI _totalLevelDiamonds;

		/// <summary>
		/// Collected diamonds.
		/// </summary>
		[SerializeField] private TextMeshProUGUI _collectedDiamonds;

		/// <summary>
		/// Diamond fill image.
		/// </summary>
		[SerializeField] private Image _diamondFillImage;

		#endregion

		#region Fields

		/// <summary>
		/// Elapsed time.
		/// </summary>
		private float _elapsedTime;

		/// <summary>
		/// Desired fill amount.
		/// </summary>
		private float _desiredFillAmount;

		/// <summary>
		/// Current fill amount.
		/// </summary>
		private float _currentFillAmount;

		/// <summary>
		/// Timer.
		/// </summary>
		private float _timer;

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
			SetFillAmount(0.0f, 0);
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
			//Timer
			_elapsedTimeText.text = _elapsedTime.ToString("F");

			//_diamondFillImage.fillAmount = (float) GameManager.Instance.CollectedDiamonds / GameManager.Instance.SceneSession.TotalLevelDiamonds;

			//Collected amount
			_collectedDiamonds.text = GameManager.Instance.CollectedDiamonds.ToString(CultureInfo.InvariantCulture);
		}

		public void SetFillAmount(float percentage, float duration)
		{
			_desiredFillAmount = percentage;
			StartCoroutine(FillCoroutine(duration));
		}

		private IEnumerator FillCoroutine(float duration)
		{
			while (_timer <= duration)
			{
				_diamondFillImage.fillAmount = Mathf.Lerp(_currentFillAmount, _desiredFillAmount, _timer / duration);
				_timer += Time.deltaTime;
				yield return null;
			}

			_timer = 0.0f;
			_currentFillAmount = _desiredFillAmount;
		}
		#endregion

	}
}
