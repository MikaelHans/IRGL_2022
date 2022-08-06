using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsCloseLogic : MonoBehaviour
{
    public Button closeButton;
    public MouseLook menuHandler;
    public Canvas settingsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(CloseSettings);
    }

    void CloseSettings()
    {
        menuHandler.closeSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
