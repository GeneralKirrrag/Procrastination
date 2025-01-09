using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlappyController : MonoBehaviour
{
    [SerializeField] private float velocity = 1.5f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private InputAction flapAction;
    [SerializeField] private CinemachineVirtualCamera vCam;

    private Rigidbody2D rb;

    private Vector3 DefaultPosition;
    private Quaternion DefaultRotation;

    [HideInInspector] public bool started = false;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;


    private void Awake() => flapAction.started += Flap;

    private void OnEnable() => flapAction.Enable();
    private void OnDisable() => flapAction.Disable();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        started = false;

        DefaultPosition = transform.position;
        DefaultRotation = transform.rotation;

        // Give Values To GameManager From Flappy Scene
        GameManager.Instance.scoreText = scoreText;
        GameManager.Instance.highScoreText = highScoreText;
    }

    private void Flap(InputAction.CallbackContext context)
    {
        if (!vCam.enabled) return;

        if (!started) {
            started = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        rb.velocity = Vector2.up * velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Die();
    }

    private void Die() {
        started = false;
        rb.bodyType = RigidbodyType2D.Static;
        transform.SetPositionAndRotation(DefaultPosition, DefaultRotation);
        GameManager.Instance.FlappyCurrentScore = 0;
    }

    private void FixedUpdate() => transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * rotationSpeed);
}
