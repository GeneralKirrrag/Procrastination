using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PC : MonoBehaviour
{
    public InputAction inputAction;
    public List<Slider> Sliders;
    public int value = 0;

    public TextMeshProUGUI essayTitle;
    public string essayTitleText;

    public CinemachineVirtualCamera vCam;

    public int typeValue = 5;

    private float timer;
    public float timerCooldown = 0.5f;

    private void Awake()
    {
        inputAction.performed += Typing;
    }

    private void OnEnable() => inputAction.Enable();
    private void OnDisable() => inputAction.Disable();

    public void Update(){
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Typing(InputAction.CallbackContext context) {
        if (timer > 0f || vCam.enabled == false) return;

        for (int i = 0; i < Sliders.Count; i++)
        {
            if (Sliders[i].value < Sliders[i].maxValue && i <= value && value != 0)
            {
                Sliders[i].value += typeValue;
                timer = timerCooldown;
                return;
            }

            if (i == Sliders.Count - 1 && Sliders[i].value == Sliders[i].maxValue) essayTitle.text = essayTitleText;
        }
    }
}
