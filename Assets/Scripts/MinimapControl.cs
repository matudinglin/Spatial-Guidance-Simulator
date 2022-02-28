using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MinimapControl : MonoBehaviour
{
    private InputDevice rightController;
    private InputDevice leftController;

    public GameObject Minimap;
    public GameObject rightHand;
    // Start is called before the first frame update
    void Start()
    {
        Minimap.SetActive(false);

        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);
        if (devices.Count > 0) rightController = devices[0];

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, devices);
        if (devices.Count > 0) leftController = devices[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!rightController.isValid || !leftController.isValid) Start();

        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonValue);
        rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool rightSecondButtonValue);
        if (rightPrimaryButtonValue)
        {
            Minimap.transform.position = rightHand.transform.position;
            Minimap.transform.rotation = Quaternion.Euler(0f, rightHand.transform.eulerAngles.y, 0f);
            Minimap.SetActive(true);
        }

        if (rightSecondButtonValue)
        {
            Minimap.SetActive(false);
        }
    }
}
