using System.Collections;
using UnityEngine;

public class Boulder : MonoBehaviour
{

	#region EditorFields


	#endregion

	#region Fields

	/// <summary>
	/// Checking neighbours.
	/// </summary>
	private bool _checkingNeighbors = true;
	
	#endregion
	
	#region Methods

	/// <summary>
	/// Called every frame.
	/// </summary>
	private void Update()
	{
		if (_checkingNeighbors)
		{
			//Right side check
			if (Physics2D.OverlapCircle(transform.position - (Vector3.right * 0.12f), 0.12f) == false)
			{
				//Right lower check
				if (Physics2D.OverlapCircle(transform.position - (Vector3.right + Vector3.down * 0.12f), 0.12f) == false)
				{
					_checkingNeighbors = false;
					Move(new Vector2(1, -1));
				}
			}
		
			//Left side check
			if (Physics2D.OverlapCircle(transform.position - (Vector3.left * 0.12f), 0.12f) == false)
			{
			
			}
		}
	}

	/// <summary>
	/// Move boulder according to given delta.
	/// </summary>
	/// <param name="delta">Vector2</param>
	private void Move(Vector2 delta)
	{
		
	}

	#endregion
}
