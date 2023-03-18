using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] Mode mode;

    private Camera _camera;
    private Vector3 _directionFromCamera;

    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(_camera.transform);
                break;

            case Mode.LookAtInverted:
                LookAtInverted();
                break;
            case Mode.CameraForward:
                transform.forward = _camera.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -_camera.transform.forward;
                break;
        }
    }

    private void LookAtInverted()
    {
        _directionFromCamera = transform.position - _camera.transform.position;
        transform.LookAt(transform.position + _directionFromCamera);
    }
}
