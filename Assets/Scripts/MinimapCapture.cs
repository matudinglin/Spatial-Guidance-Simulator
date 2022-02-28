using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class MinimapCapture : MonoBehaviour
{
    public Transform player;
    private Camera cam;

    private InputDevice rightController;
    private InputDevice leftController;

    private float mapResizeRate = 5f;

    private void Start()
    {
        cam = GetComponent<Camera>();

        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);
        if (devices.Count > 0) rightController = devices[0];

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, devices);
        if (devices.Count > 0) leftController = devices[0];
    }


    private void Update()
    {
        if (!rightController.isValid || !leftController.isValid) Start();

        rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightTriggerButtonValue);
        leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftTriggerButtonValue);

        if (rightTriggerButtonValue)
        {
            cam.orthographicSize += mapResizeRate;
        }

        if (leftTriggerButtonValue)
        {
            cam.orthographicSize -= mapResizeRate;
        }
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }


}
