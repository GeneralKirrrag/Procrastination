using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollow : MonoBehaviour
{
    public RectTransform thisObject;
    public RectTransform canvasRect;
    public Transform target;

    public Vector2 offset;

    public void Start() {
        canvasRect = GetComponentInParent<Canvas>().transform as RectTransform;

        thisObject = transform as RectTransform;
    }

    public void Update() {
         // Offset position above object bbox (in world space)
        Vector3 offsetPos = target.transform.position + (Vector3)offset;

        // Final position of marker above GO in world space
        Vector3 newOffsetPos = new Vector3(target.transform.position.x, offsetPos.y, target.transform.position.z);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(newOffsetPos);

        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        // Set
        thisObject.localPosition = canvasPos;
    }
}
