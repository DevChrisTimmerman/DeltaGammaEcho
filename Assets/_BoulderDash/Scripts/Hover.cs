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

		#region Fields

		/// <summary>
		/// Random offset.
		/// </summary>
		private float _offset;

		#endregion

		#region Initialization

		/// <summary>
		/// Called upon start.
		/// </summary>
		private void Start()
		{
			_offset = UnityEngine.Random.Range(0, (2.0f*Mathf.PI)/_speed);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Called every frame.
		/// </summary>
		private void Update()
		{
			Vector2 pos = transform.localPosition; 
			pos.y = _distance * Mathf.Sin((Time.realtimeSinceStartup + _offset) * _speed);
			transform.localPosition = pos; 
			
		}

		#endregion

	}
}
