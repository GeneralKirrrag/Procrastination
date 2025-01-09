using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TV : MonoBehaviour
{
    public Timer timer;
    public CameraController camController;
    public VirtualCamera virtualCamera;
    public PlayerController player;
    public Image interactImage;
    public GameObject screen;

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
            screen.SetActive(true);
            interactImage.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            screen.SetActive(false);
            interactImage.enabled = false;
            isInRange = false;
        }
    }

    public void ToggleTV(InputAction.CallbackContext context)
    {
        Debug.Log("Input Accepted!");

        if (!virtualCamera.vCam.enabled)
        {
            if (!isInRange) return;
            player.controllable = false;
            player.ShowCharacter = false;
            camController.SwitchCamera(virtualCamera);
        }
        else
        {
            player.controllable = true;
            player.ShowCharacter = true;
            camController.SwitchCamera(camController.StartingCamera);
        }
        
    }
}
