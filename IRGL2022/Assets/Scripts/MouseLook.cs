using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;
    float xRotation = 0f;
    public GameObject inventoryUI;

    public bool isInventoryOpened;
    // Start is called before the first frame update
    void Start()
    {
        closeInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInventoryOpened)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }        

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(isInventoryOpened)
            {
                closeInventory();
            }
            else
            {
                openInventory();
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
}
