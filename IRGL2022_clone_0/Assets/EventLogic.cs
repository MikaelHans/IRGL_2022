using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class EventLogic : MonoBehaviourPun
{
    public PlayerMovement playerMovement;
    public KeyCode playerMovementSprintKey = KeyCode.LeftShift;
    public KeyCode playerMovementCrouchKey = KeyCode.LeftControl;
    public KeyCode playerMovementJumpKey = KeyCode.Space;

    public ChracterPickUpWeapon playerPickUpWeapon;
    public KeyCode playerPickUpWeaponPickupKey = KeyCode.E;
    public KeyCode playerPickUpWeaponDropKey = KeyCode.Q;
    public KeyCode playerWeaponFireKey = KeyCode.Mouse0;
    public KeyCode playerWeaponReloadKey = KeyCode.R;
    public KeyCode playerWeaponADSKey = KeyCode.Mouse1;

    public CharacterPickUpItem playerPickUpItem;
    public KeyCode playerPickUpItemPickupKey = KeyCode.E;

    public PlayMinigame playerMinigame;
    public KeyCode playerMinigameKey = KeyCode.E;

    public MouseLook playerMouseLook;
    public KeyCode playerMouseLookInventoryKey = KeyCode.Tab;
    public KeyCode playerMouseLookSettingsKey = KeyCode.Escape;

    public MapEvent playerMapEvent;
    public KeyCode playerMapEventOpenMapKey = KeyCode.M;

    public FPSTPSToggle playerFPSTPSToggle;
    public KeyCode playerFPSTPSToggleKey = KeyCode.V;

    public TPSPositionToggle playerTPSPositionToggle;
    public KeyCode playerTPSPositionToggleKey = KeyCode.T;


    void Start()
    {

    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }



        if (Input.GetKey(playerMinigameKey))
        {
            playerMinigame.Minigame();
        }

        if (Input.GetKeyDown(playerMouseLookInventoryKey))
        {
            playerMouseLook.Inventory();

        }

        if (Input.GetKeyDown(playerMouseLookSettingsKey))
        {
            if (!playerMapEvent.isMapOpened && !playerMinigame.isMinigameOpened)
            {
                playerMouseLook.Settings();
            }

            playerMapEvent.CloseMap();
            playerMinigame.CloseMinigame();
        }

        if (Input.GetKeyDown(playerMapEventOpenMapKey))
        {
            if (playerMouseLook.isInventoryOpened)
            {
                playerMouseLook.Inventory();
            }

            if (playerMouseLook.isSettingsOpened)
            {
                playerMouseLook.Settings();
            }

            playerMapEvent.Map();
        }

        if (!playerMouseLook.isInventoryOpened && !playerMouseLook.isSettingsOpened && !playerMapEvent.isMapOpened && !playerMinigame.isMinigameOpened)
        {
            if (Input.GetKey(playerWeaponFireKey))
            {
                if (playerPickUpWeapon.gunEquiped != -1)
                {
                    playerPickUpWeapon.weapon[playerPickUpWeapon.gunEquiped]._gunsystem.Fire();
                }
            }

            if (Input.GetKey(playerWeaponReloadKey))
            {
                if (playerPickUpWeapon.gunEquiped != -1)
                {
                    playerPickUpWeapon.weapon[playerPickUpWeapon.gunEquiped]._gunsystem.Reloads();
                }
            }

            if (Input.GetKey(playerWeaponADSKey))
            {
                if (playerPickUpWeapon.gunEquiped != -1)
                {
                    playerPickUpWeapon.weapon[playerPickUpWeapon.gunEquiped]._gunsystem.ADSs();
                }
            }

            if (Input.GetKey(playerMovementSprintKey))
            {
                playerMovement.Sprint();
            }

            if (Input.GetKey(playerMovementCrouchKey))
            {
                playerMovement.Crouch();
            }

            if (Input.GetKey(playerMovementJumpKey))
            {
                playerMovement.Jump();
            }

            if (Input.GetKey(playerPickUpWeaponPickupKey))
            {
                playerPickUpWeapon.Pickup();
            }

            if (Input.GetKey(playerPickUpWeaponDropKey))
            {
                playerPickUpWeapon.Drop();
            }

            if (Input.GetKey(playerPickUpItemPickupKey))
            {
                playerPickUpItem.Pickup();
            }

            if (Input.GetKeyDown(playerFPSTPSToggleKey))
            {
                playerFPSTPSToggle.ToggleMode();
            }

            if (Input.GetKeyDown(playerTPSPositionToggleKey))
            {
                playerTPSPositionToggle.TogglePosition();
            }

            playerMovement.MoveHorizontal(Input.GetAxis("Horizontal"));
            playerMovement.MoveVertical(Input.GetAxis("Vertical"));
            playerPickUpWeapon.Scroll(Input.mouseScrollDelta.y);
            playerMouseLook.MouseMove(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

    }
}
