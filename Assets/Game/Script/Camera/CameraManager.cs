using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachinePanTilt _pantilt;

    [SerializeField]
    private CinemachineInputAxisController _cameraInput;

    public float PanAxis => _pantilt.PanAxis.Value;

    public void SetCameraInputEnable(bool isActive)
    {
        _cameraInput.enabled = isActive;
    }

    public void ResetCameraRotation()
    {
        _pantilt.PanAxis.Value = 0;
        _pantilt.TiltAxis.Value = 0;
    }

    public void SetPanAxisValue(float panValue)
    {
        _pantilt.PanAxis.Value = panValue;
    }

    public void SetTiltAxisValue(float tiltValue)
    {
        _pantilt.TiltAxis.Value = tiltValue;
    }
}
