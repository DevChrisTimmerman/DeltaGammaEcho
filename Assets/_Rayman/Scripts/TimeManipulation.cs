using UnityEngine;

public class TimeManipulation : MonoBehaviour
{

	#region EditorFields

	[SerializeField]
	private float _factor;

	#endregion

	#region Fields

	private float _timeScale = 1.0f;

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
	    if (Input.GetKeyUp(KeyCode.KeypadEnter))
	    {
		    _timeScale = 1;
	    }
		else if (Input.GetKeyUp(KeyCode.KeypadPlus))
	    {
		    _timeScale *= _factor;
	    }
	    else if (Input.GetKeyUp(KeyCode.KeypadMinus))
	    {
		    _timeScale /= _factor;
	    }

	    _timeScale = Mathf.Clamp(_timeScale ,0, 100);
	    Time.timeScale = _timeScale;
    }

	#endregion

}
