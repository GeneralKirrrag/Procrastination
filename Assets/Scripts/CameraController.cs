using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public List<VirtualCamera> VirtualCameras = new List<VirtualCamera>();

    public VirtualCamera CurrentCamera;
    public VirtualCamera StartingCamera;

    public void Start() {
        if (StartingCamera == null) StartingCamera = VirtualCameras.First();
        SwitchCamera(StartingCamera);
    }

    public void SwitchCamera(VirtualCamera camera) {
        VirtualCameras.ForEach(x => x.vCam.enabled = false);
        camera.vCam.enabled = true;
        CurrentCamera = camera;
    }

    public void SwitchCameraInput(InputAction.CallbackContext context) {
        if (CurrentCamera.camType == VirtualCamera.CamType.ISO) {
            SwitchCamera(VirtualCameras.Find(x => x.camType == VirtualCamera.CamType.ISOTD));
        }
        else if (CurrentCamera.camType == VirtualCamera.CamType.ISOTD) {
            SwitchCamera(VirtualCameras.Find(x => x.camType == VirtualCamera.CamType.ISO));
        }
    }    
}

[System.Serializable]
    public class VirtualCamera
    {
        public enum CamType{ISO, ISOTD, SCREEN, OTHER}
        public CamType camType;
        public CinemachineVirtualCamera vCam;
    }