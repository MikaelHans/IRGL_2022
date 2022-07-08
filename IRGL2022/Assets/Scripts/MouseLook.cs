using Photon.Pun;
using UnityEngine;


public class MouseLook : MonoBehaviourPun
{
    public float mouseSensitivity = 100.0f;
    public float mouseSensitivityMultiplier = 1f;
    public Transform playerBody;
    float xRotation = 0f;
    public GameObject inventoryUI;
    public GameObject SettingsUI;
    public Transform bone;

    public bool isInventoryOpened;
    public bool isSettingsOpened;
    // Start is called before the first frame update
    void Start()
    {
        closeInventory();
        closeSettings();
    }

    // Update is called once per frame
    private void Update()
    {

    }
    void LateUpdate()
    {
        if (photonView.IsMine)
        {
            if (!isInventoryOpened && !isSettingsOpened)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * mouseSensitivityMultiplier * Time.deltaTime;
                float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity * mouseSensitivityMultiplier * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.parent.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                //transform.rot
                bone.localRotation = Quaternion.Euler(-180f, 0f, xRotation);
                playerBody.Rotate(Vector3.up * mouseX);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (isSettingsOpened)
                {
                    closeSettings();
                }
                if (isInventoryOpened)
                {
                    closeInventory();
                }
                else
                {
                    openInventory();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isInventoryOpened)
                {
                    closeInventory();
                }
                if (isSettingsOpened)
                {
                    closeSettings();
                }
                else
                {
                    openSettings();
                }
            }
        }
    }

    void openInventory()
    {
        isInventoryOpened = true;
        inventoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void closeInventory()
    {
        isInventoryOpened = false;
        inventoryUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void openSettings()
    {
        isSettingsOpened = true;
        SettingsUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void closeSettings()
    {
        isSettingsOpened = false;
        SettingsUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
