using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceControl : MonoBehaviour
{

    private ParticleSystem particleSystem;
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
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }

    private void Update()
    {
        if (IsInView(transform.position))
        {
            Debug.Log("In view.");
            particleSystem.Emit(5);
        }
        else
        {
            Debug.Log("Not in view.");
            particleSystem.Stop();
        }
    }
}
