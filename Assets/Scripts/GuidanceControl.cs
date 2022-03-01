using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceControl : MonoBehaviour
{
    private Outline outline;
    public bool IsInView(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);

        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            return true;
        else
            return false;
    }

    private void Start()
    {
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
    }

    private void Update()
    {
        if (IsInView(transform.position))
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }
}
