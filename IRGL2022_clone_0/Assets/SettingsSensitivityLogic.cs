using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSensitivityLogic : MonoBehaviour
{
    public Slider sensitivitySlider;
    public InputField sensitivitySliderInputField;
    public MouseLook mouseLook;

    public float minValue;
    public float maxValue;

    // Start is called before the first frame update
    void Start()
    {
        sensitivitySlider.minValue = minValue;
        sensitivitySlider.maxValue = maxValue;

        SetValue(mouseLook.mouseSensitivityMultiplier);

    }

    public void SetValue(float value)
    {
        value = Mathf.Clamp(value, minValue, maxValue);
        sensitivitySlider.value = value;
        sensitivitySliderInputField.text = value.ToString();
        mouseLook.mouseSensitivityMultiplier = value;
    }

    public void SetValue(string value)
    {
        float newValue = float.Parse(value);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
