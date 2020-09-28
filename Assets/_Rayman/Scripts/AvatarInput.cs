using UnityEngine;

[RequireComponent(typeof(Avatar))]
public class AvatarInput : MonoBehaviour
{

	#region Fields

	private Avatar _avatar;

	private bool _jump;

	private bool _helicopter;

	#endregion

	#region Constructor

	/// <summary> 
    /// Start is called before the first frame update
	/// </summary>
    private void Start()
	{
		_avatar = GetComponent<Avatar>();
	}

	#endregion

	#region Methods

	/// <summary> 
    /// Update is called once per frame
	/// </summary>
    private void Update()
    {
		//Register jump key pressed and cache until next FixedUpdate
	    if (_jump == false)
	    {
		    _jump = Input.GetButtonDown("Jump");
	    }

		_helicopter = Input.GetButton("Jump");

	    if (Input.GetKeyDown(KeyCode.G))
	    {
		    _avatar.ToggleGodMode();
	    }

	    if (Input.GetButtonDown("Fire1"))
	    {
		    _avatar.ChargePunch();
	    }

		if (Input.GetButtonUp("Fire1"))
	    {
		    _avatar.ReleasePunch();
	    }
	}

	private void FixedUpdate()
	{
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"),
			Input.GetAxis("Vertical"));
		bool isCrouching = input.y < -0.1f;
		bool isSprinting = Input.GetButton("Fire3");
		_avatar.Move(input, _jump, _helicopter, isCrouching, isSprinting);

		_jump = false;
	}

	#endregion

}
