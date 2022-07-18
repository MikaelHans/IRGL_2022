using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class WorldBorderState
{
    public Transform position;
    public float ratio;

    WorldBorderState(Transform position, float ratio)
    {
        this.position = position;
        this.ratio = ratio;
    }
}

public class ShrinkLogic : MonoBehaviour
{
    public float shrinkDuration = 60f;
    public float shrinkDelay = 120f;

    private int tapeCounter = 0;
    private float totalTime = 0;

    private Vector3 currentPosition;
    private Vector3 currentScale;

    public GameLogic gamelogic;

    public airplane airplane;
    public Transform airplane_spawn_point;

    [SerializeField]
    public List<WorldBorderState> shrinkTape;

    public bool can_respawn;

    public int TapeCounter { get => tapeCounter; set => tapeCounter = value; }

    // Start is called before the first frame updates
    void Start()
    {
        currentPosition = transform.position;
        currentScale = transform.localScale;
        float angle = Random.Range(0,360) * Mathf.PI * 2;
        float x = Mathf.Cos(angle) * transform.localScale.x;
        float z = Mathf.Cos(angle) * transform.localScale.x;
        Vector3 spawnPos = new Vector3(x, airplane_spawn_point.position.y, z);
        GameObject airplane = PhotonNetwork.InstantiateRoomObject("Prefabs/Airplane", spawnPos, airplane_spawn_point.rotation);
        //Vector3 airplanePos = airplane.transform.position;

        airplane.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

        change_airplane_spawn_point();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        if (totalTime > shrinkDelay && tapeCounter < shrinkTape.Count)
        {
            if (totalTime <= shrinkDelay + shrinkDuration)
            {
                transform.position = currentPosition - ((currentPosition - shrinkTape[tapeCounter].position.localPosition) * ((totalTime - shrinkDelay) / (shrinkDuration)));

                float scaler = (1 - (shrinkTape[tapeCounter].ratio * ((totalTime - shrinkDelay) / shrinkDuration)));

                Vector3 newScale = currentScale;
                newScale.x = currentScale.x + ((currentScale.x * scaler) - currentScale.x);
                newScale.y = currentScale.y + ((currentScale.y * scaler) - currentScale.y);
                newScale.z = currentScale.z + ((currentScale.z * scaler) - currentScale.z);
                transform.localScale = newScale;
            }
            else
            {
                tapeCounter++;
                currentPosition = transform.position;
                currentScale = transform.localScale;
                totalTime = 0;
                if(tapeCounter < shrinkTape.Count-1)
                {
                    spawn_airplane();
                }                
            }
        }
    }

    public void spawn_airplane()
    {
        if (PhotonNetwork.IsMasterClient && can_respawn)
        {
            GameObject airplane =  PhotonNetwork.InstantiateRoomObject("Prefabs/Airplane", airplane_spawn_point.position, airplane_spawn_point.rotation);
            airplane.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            change_airplane_spawn_point();
        }
    }

    void change_airplane_spawn_point()
    {

    }
}
