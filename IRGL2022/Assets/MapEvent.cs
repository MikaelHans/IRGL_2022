using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mapCamera;

    public bool isMapOpened = false;
    public bool isMapEnabledScene = false;

    public float sceneFog = 0.0f;
    public float sceneFogMax = 5000f;
    public float fogLerp = 0.0f;

    float diff = 0;

    public GameObject crosshairGroup;
    public GameObject mapMarker;

    public Player thisPlayer;

    void Start()
    {
        mapCamera = GameObject.Find("Map Camera");

        sceneFog = RenderSettings.fogEndDistance;

        if (mapCamera == null)
        {
            isMapEnabledScene = false;

        }
        else
        {
            isMapEnabledScene = true;
        }
    }

    public void Map()
    {
        if (!isMapEnabledScene)
        {
            return;
        }
        if (!isMapOpened)
        {
            OpenMap();
        }
        else
        {
            CloseMap();
        }
    }

    public void OpenMap()
    {
        mapCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 100;
        crosshairGroup.SetActive(false);
        isMapOpened = true;
    }

    public void CloseMap()
    {
        mapCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = -1;
        crosshairGroup.SetActive(true);
        isMapOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        float target = 0;
        if (isMapOpened)
        {
            fogLerp += Time.deltaTime;
            target = 1;
        }
        else
        {
            fogLerp -= Time.deltaTime;
        }


        diff += (target - diff) * Time.deltaTime * 10f;
        if (Mathf.Abs(diff) > 0.001)
        {
            List<Player> allPlayers = new List<Player>(FindObjectsOfType<Player>());
            foreach (var player in allPlayers)
            {
                if (player.Team_id == thisPlayer.Team_id)
                {
                    player.mapEvent.mapMarker.transform.localScale = new Vector3(diff, diff, diff);
                }
            }
        }


        fogLerp = Mathf.Clamp(fogLerp, 0.0f, 1.0f);

        RenderSettings.fogStartDistance = Mathf.Lerp(0.0f, sceneFogMax / 10f, fogLerp);
        RenderSettings.fogEndDistance = Mathf.Lerp(sceneFog, sceneFogMax, fogLerp);
    }
}
