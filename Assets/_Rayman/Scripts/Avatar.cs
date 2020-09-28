using UnityEngine;

public class Avatar : MonoBehaviour
{
	#region EditorFields

	[Header("Walking")]
	[SerializeField]
	private float _maxSpeed;

	[SerializeField]
	[Range(0,1)]
	private float _walkingDeadZone;

	[Header("Crawling")]
	//TODO: Set animationStateMachine condition through code
	//[SerializeField]
	//private float _crawlingDeadZone;

	[SerializeField]
	private float _crawlingSpeed = 0.8f;

	[Header("Sprinting")]
	[SerializeField]
	private float _sprintingSpeed = 1.5f;

	[SerializeField]
	private AnimationCurve _deceleration;

	[Header("Jump")]
	[SerializeField]
	private Transform _groundedCheckPos;

	[SerializeField]
	private float _groundCheckRadius;

	[SerializeField]
	private LayerMask _groundMask;

	[SerializeField]
	private float _jumpForce;

	[Header("Leaning")]
	[SerializeField]
	private Transform _backwardLeanCheckPos;

	[SerializeField]
	private Transform _forwardLeanCheckPos;

	[SerializeField]
	private float _leanCheckDistance;

	[Header("Punch")]
	[SerializeField]
	private GameObject _punchPrefab;

	[SerializeField]
	private float _punchPower;

	[SerializeField]
	private float _maxPower;

	[SerializeField]
	private float _minPower;

	[SerializeField]
	private float _returningPunchPower;

	[SerializeField]
	private AnimationCurve _returningCurve;

	#endregion

	#region Fields

	private AvatarInput _avatarInput;

	private Animator _anim;

	private Rigidbody2D _rb;

	private bool _isFacingLeft;

	private bool _isGrounded;

	private bool _godMode;

	private bool _backwardLeanCheck;

	private bool _forwardLeanCheck;

	private float _punchTimer;

	private bool _isChargingPunch;

	private GameObject _punchGameObject;

	#endregion

	#region Constructor

	/// <summary> 
    /// Start is called before the first frame update
	/// </summary>
    private void Start()
	{
		_anim = GetComponent<Animator>();
		_rb = GetComponent<Rigidbody2D>();
	}

	#endregion

	#region Methods

	/// <summary> 
    /// Update is called once per frame
	/// </summary>
    private void Update()
	{
		if (_isChargingPunch)
		{
			_punchTimer += Time.deltaTime;
		}

		if (_punchGameObject != null)
		{
			_punchGameObject.GetComponent<Rigidbody2D>().AddForce((_punchGameObject.transform.position - transform.position).normalized * (_returningPunchPower + _returningCurve.Evaluate(_punchTimer)), ForceMode2D.Force);
			_punchTimer += Time.deltaTime;
			if (_punchTimer >= 5.0f)
			{
				Destroy(_punchGameObject);
				_punchTimer = 0.0f;
			}
		}

	}

	public void Move(Vector2 input, bool jump, bool helicopter, bool crouching, bool sprinting)
	{
		if (_godMode)
		{
			_rb.MovePosition(_rb.transform.position + new Vector3(input.x, input.y, 0));
			return;
		}

		// Reduce the speed if not is default walking state
		float move;
		if (crouching)
		{
			move = input.x * _crawlingSpeed;
		}
		else if (IsBraking())
		{
			move = (_isFacingLeft ? -1 : 1) * _deceleration.Evaluate(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		}
		else if (sprinting)
		{
			move = input.x * _sprintingSpeed;
		}
		else if (_isChargingPunch)
		{
			move = 0;
		}
		else
		{
			_anim.ResetTrigger("CrawlingFlip");
			move = input.x;
		}

		// Move the character
		_rb.velocity = new Vector2(move * Time.deltaTime * _maxSpeed, _rb.velocity.y);

		if (IsBraking() == false && input.x < 0.0f && _isFacingLeft == false)
		{
			if (crouching)
			{
				_anim.SetTrigger("CrawlingFlip");
			}
			Flip();
		}
		else if (IsBraking() == false && input.x > 0.0f && _isFacingLeft)
		{
			if (crouching)
			{
				_anim.SetTrigger("CrawlingFlip");
			}
			Flip();
		}

		// If the player should jump...
		if (_isGrounded && jump && _anim.GetBool("IsGrounded") && crouching == false)
		{
			// Add a vertical force to the player.
			_isGrounded = false;
			_anim.SetBool("IsGrounded", false);
			_rb.AddForce(new Vector2(0f, _jumpForce));
		}
		//Helicopter
		else if (_isGrounded == false && helicopter && jump == false && IsJumping() == false && crouching == false)
		{
			_anim.SetBool("IsHelicoptering", true);
			_rb.AddForce(new Vector2(0f, - Physics2D.gravity.y));
		}
		else if (_isGrounded || helicopter == false)
		{
			_anim.SetBool("IsHelicoptering", false);
		}

		// SetAnimator Values
		_anim.SetBool("IsMoving", (input.x > _walkingDeadZone || input.x < -_walkingDeadZone));
		_anim.SetBool("IsSprinting", sprinting);
		_anim.SetFloat("CrouchAxis", input.y);
		_anim.SetFloat("vSpeed", _rb.velocity.y);

		//_jump = false;
	}

	private void FixedUpdate()
	{
		GroundedCheck();	
	}

	private void Flip()
	{
		// Switch the way the player is facing.
		_isFacingLeft = !_isFacingLeft;

		// Multiply the player's x local scale by -1.
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	private void GroundedCheck()
	{
		_isGrounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundedCheckPos.position, _groundCheckRadius, _groundMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				_isGrounded = true;
				break;
			}
		}
		_anim.SetBool("IsGrounded", _isGrounded);

		_backwardLeanCheck = Physics2D.Raycast(_backwardLeanCheckPos.position, Vector2.down, _leanCheckDistance, _groundMask);
		_forwardLeanCheck = Physics2D.Raycast(_forwardLeanCheckPos.position, Vector2.down, _leanCheckDistance, _groundMask);
		Debug.DrawLine(_backwardLeanCheckPos.position, _backwardLeanCheckPos.position + (Vector3.down * _leanCheckDistance), Color.red);
		Debug.DrawLine(_forwardLeanCheckPos.position, _forwardLeanCheckPos.position + (Vector3.down * _leanCheckDistance), Color.red);

		if (_isGrounded)
		{
			if (_backwardLeanCheck == false && _forwardLeanCheck)
			{
				_anim.SetBool("IsLeaningBackward", true);
				_anim.SetBool("IsLeaningForward", false);
			}
			else if (_backwardLeanCheck && _forwardLeanCheck == false)
			{
				_anim.SetBool("IsLeaningForward", true);
				_anim.SetBool("IsLeaningBackward", false);
			}
			else if (_backwardLeanCheck && _forwardLeanCheck)
			{
				_anim.SetBool("IsLeaningForward", false);
				_anim.SetBool("IsLeaningBackward", false);
			}
		}
	}

	/// <summary>
	/// Checks if current animator state is "Braking" of "Braking 1".
	/// </summary>
	/// <returns></returns>
	public bool IsBraking()
	{
		return _anim.GetCurrentAnimatorStateInfo(0).IsName("Braking") || _anim.GetCurrentAnimatorStateInfo(0).IsName("Braking 1");
	}

	public bool IsSprinting()
	{
		return _anim.GetCurrentAnimatorStateInfo(0).IsName("Sprinting");
	}

	public bool IsCrouching()
	{
		return _anim.GetCurrentAnimatorStateInfo(0).IsName("CrawlingIdle") ||
		       _anim.GetCurrentAnimatorStateInfo(0).IsName("GoingCrawling") ||
		       _anim.GetCurrentAnimatorStateInfo(0).IsName("Crawling") ||
		       _anim.GetCurrentAnimatorStateInfo(0).IsName("CrawlTurn");
	}

	public bool IsJumping()
	{
		return _anim.GetCurrentAnimatorStateInfo(0).IsName("Jump");
	}

	/// <summary>
	/// Changes Animation state,
	/// Toggles isKinematic,
	/// Toggles BoxCollider2D.
	/// </summary>
	public void ToggleGodMode()
	{
		_godMode = !_godMode;
		_anim.Play(_godMode ? "GodMode" : "Idle");
		_rb.isKinematic = _godMode;
		GetComponent<BoxCollider2D>().enabled = !_godMode;
	}

	#endregion

	public void ChargePunch()
	{
		if (_punchGameObject != null)
			return;

		if (IsSprinting() || IsBraking() || IsCrouching())
			return;

		_isChargingPunch = true;
		_punchTimer = 0.0f;
		_anim.SetBool("IsChargingPunch", true);
	}

	public void ReleasePunch()
	{
		if (_isChargingPunch == false)
			return;

		_isChargingPunch = false;
		//Position
		_anim.SetBool("IsChargingPunch", false);
		_punchGameObject = Instantiate(_punchPrefab);
		_punchGameObject.transform.position = transform.position;
		//Facing direction
		Vector3 scale = _punchGameObject.transform.localScale;
		scale.x *= _isFacingLeft ? -1 : 1 ;
		_punchGameObject.transform.localScale = scale;
		//Physics
		_punchGameObject.GetComponent<Rigidbody2D>().AddForce(transform.localScale.x * Vector2.right * Mathf.Clamp(_punchTimer * _punchPower, _minPower, _maxPower), ForceMode2D.Impulse);
		_punchTimer = 0.0f;
	}
}
