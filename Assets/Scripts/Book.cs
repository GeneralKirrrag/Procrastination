using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Book : MonoBehaviour
{
    public InputAction inputAction;
    public PC pc;

    bool isInRange;
    public LayerMask playerLayer;

    private void Awake()
    {
        inputAction.performed += PickUp;
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

    public void PickUp(InputAction.CallbackContext context) {
        if(!isInRange) return;

        pc.value++;
        Destroy(gameObject);
    }
}
