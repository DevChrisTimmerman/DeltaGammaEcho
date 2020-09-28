using UnityEngine;

public class Dirt : MonoBehaviour
{

	#region EditorFields

	/// <summary>
	/// Delay before block is removed.
	/// </summary>
	[SerializeField]
	private float _removeDelay;
	
	/// <summary>
	/// Collider.
	/// </summary>
	[SerializeField]
	private BoxCollider2D _collider;

	/// <summary>
	/// Renderer.
	/// </summary>
	[SerializeField]
	private SpriteRenderer _renderer;
	
	/// <summary>
	/// Audio source.
	/// </summary>
	[SerializeField]
	private AudioSource _audioSource;
	
	/// <summary>
	/// Pitch range.
	/// </summary>
	[SerializeField]
	private float _pitchRange;
	
	/// <summary>
	/// Particle GameObject.
	/// </summary>
	[SerializeField]
	private GameObject _particleGameObject;
	
	#endregion

	#region Methods

	/// <summary>
	/// On trigger enter 2D.
	/// </summary>
	/// <param name="other">Collider2D</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<MinerMovement>() != null)
		{
			Clear();
		}
	}

	/// <summary>
	/// Clears current dirt block.
	/// </summary>
	private void Clear()
	{
		_renderer.enabled = false;
		_collider.enabled = false;
		Destroy(gameObject,_removeDelay);
		_audioSource.pitch = 1 + Random.Range(-_pitchRange, _pitchRange);
		_audioSource.Play();
		_particleGameObject.SetActive(true);
	}

	#endregion
	
}
