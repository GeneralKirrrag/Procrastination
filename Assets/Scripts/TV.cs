using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TV : MonoBehaviour
{
    public Timer timer;
    public CameraController camController;
    public VirtualCamera virtualCamera;

    private bool isInRange;
    public LayerMask playerLayer;

    public InputAction inputAction;

    private void Awake()
    {
        inputAction.performed += ToggleTV;
    }

    private void OnEnable() => inputAction.Enable();
    private void OnDisable() => inputAction.Disable();

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Debug.Log("Player In Range!");
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            isInRange = false;
        }
    }

    public void ToggleTV(InputAction.CallbackContext context)
    {
        Debug.Log("Input Accepted!");

        if (!isInRange) return;

        if (!virtualCamera.vCam.enabled)
        {
            camController.SwitchCamera(virtualCamera);
        }
        else
        {
            camController.SwitchCamera(camController.StartingCamera);
        }
        
    }
}
