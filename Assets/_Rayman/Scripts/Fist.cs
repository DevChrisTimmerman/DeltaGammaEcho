using UnityEngine;

public class Fist : MonoBehaviour
{

	#region Fields

	private bool _isFirstTouch = true;

	#endregion
	
	#region Constructor

	/// <summary> 
    /// Start is called before the first frame update
	/// </summary>
    private void Start()
    {
        
    }

	#endregion

	#region Methods

	/// <summary> 
    /// Update is called once per frame
	/// </summary>
    private void Update()
    {
        
    }

	private void OnTriggerExit2D(Collider2D other)
	{
		Avatar rayman = other.GetComponent<Avatar>();
		if (rayman != null)
		{
			_isFirstTouch = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_isFirstTouch)
		{
			return;
		}

		Avatar rayman = other.GetComponent<Avatar>();
		if (rayman != null)
		{
			Destroy(gameObject);
		}
	}

	#endregion

}
