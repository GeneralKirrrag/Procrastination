using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerController : MonoBehaviour {
    public bool controllable = true;
    private bool showCharacter;
    public bool ShowCharacter {
        get {
            return showCharacter;
        }
        set {
            characterModel.SetActive(value);
            showCharacter = value;
        }
    }
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject characterModel;
    [SerializeField] private float speed = 5;
    [SerializeField] private float turnSpeed = 360;
    private Vector3 input;

    private void Update() {
        Look();

        if (input != Vector3.zero) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);
    }

    private void FixedUpdate() {
        Move();
    }

    public void GatherInput(InputAction.CallbackContext context)
    {
        if(!controllable){
            input = Vector3.zero;
            return;
        } 

        Vector2 value = context.ReadValue<Vector2>();
        input = new Vector3(value.x, 0f, value.y);
    }

    private void Look() {
        if (input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
    }

    private void Move() {
        rb.MovePosition(transform.position + input.ToIso() * input.normalized.magnitude * speed * Time.deltaTime);
    }
}

public static class Helpers 
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 135, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
