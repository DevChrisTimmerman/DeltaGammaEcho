using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

	#region EditorFields

	[SerializeField]
	private Transform _target;

	[SerializeField]
	private Vector2 _offset;

	[SerializeField]
	[Range(0,1)]
	private float _smoothSpeed;

	[SerializeField]
	private float _zoomMin;

	[SerializeField]
	private float _zoomMax;

	[SerializeField]
	private float _zoomSpeed = 0.6f;

	#endregion

	#region Fields

	private float _camZ;

	#endregion

    // Start is called before the first frame update
    private void Start()
    {
	    _camZ = transform.position.z;
    }

    // Update is called once per frame
    private void Update()
    {
	    UpdateCameraPos();

	    UpdateZoomLevel();
    }

	private void UpdateZoomLevel()
	{
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + Input.mouseScrollDelta.y * _zoomSpeed, _zoomMin, _zoomMax);
	}

	private void UpdateCameraPos()
	{
		Vector3 desiredPos = _target.position;
	    desiredPos += new Vector3(_offset.x, _offset.y, _camZ);
	    Camera.main.transform.position =Vector3.Lerp(Camera.main.transform.position,desiredPos, _smoothSpeed);
	}
}
