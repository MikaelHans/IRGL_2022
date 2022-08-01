using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public UnlockableChest chest;
    public GameObject canvas;

    public GameObject attachedPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Play(GameObject player)
    {
        attachedPlayer = player;
        canvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public virtual void Init()
    {

    }

    public virtual void closeWindow()
    {
        attachedPlayer.GetComponent<PlayMinigame>().isMinigameOpened = false;
        canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        chest.Close();
    }

}
