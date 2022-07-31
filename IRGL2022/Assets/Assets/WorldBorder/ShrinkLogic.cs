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

public class ShrinkLogic : MonoBehaviourPun
{
    
    public float shrinkDuration = 60f;
    public float shrinkDelay = 120f;

    private int tapeCounter = 0;
    public float totalTime = 0;

    private Vector3 currentPosition;
    private Vector3 currentScale;

    public GameLogic gamelogic;

    public airplane airplane;
    public Transform airplane_spawn_point;
    public Transform airplane_destination_point;

    public List<Transform> spawnPoints = new List<Transform>();

    [SerializeField]
    public List<WorldBorderState> shrinkTape;

    public bool can_respawn;
    bool initSpawn;

    public int TapeCounter { get => tapeCounter; set => tapeCounter = value; }

    public float minimumDist;
    int seed = 2022;


    // Start is called before the first frame updates
    void Start()
    {
        photonView.ViewID = 999;
        initSpawn = true;
        if (PhotonNetwork.IsMasterClient)
        {
            spawn_airplane();
        }      
        initSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        if(PhotonNetwork.IsMasterClient)
        {
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
                    if (tapeCounter < shrinkTape.Count - 1)
                    {
                        spawn_airplane();
                    }
                }
            }
        }        
    }

    public Vector3 GetRandomPointInEdge()
    {
        Random.InitState(seed++);
        Debug.Log("Diameter: " + transform.localScale.x / 100);
        Vector3 pos = new Vector3();
        int i = Random.Range(0, spawnPoints.Count);
        pos = spawnPoints[i].position;
        return pos;
    }

    public void spawn_airplane()
    {
        if (PhotonNetwork.IsMasterClient && (initSpawn || (can_respawn && TapeCounter < shrinkTape.Count - 1)))
        {
            currentPosition = transform.position;
            currentScale = transform.localScale;
            
            Vector3 spawnPos = GetRandomPointInEdge();
            Vector3 destinationPos = GetRandomPointInEdge();
            float distance = Vector3.Distance(spawnPos, destinationPos);
            minimumDist = ((transform.localScale.x * 2) / 100) - 75;
            while (distance < minimumDist)
            {
                destinationPos = GetRandomPointInEdge();
                distance = Vector3.Distance(spawnPos, destinationPos);
            }

            //airplane_destination_point.position = destinationPos;
            GameObject _airplane = PhotonNetwork.InstantiateRoomObject("Prefabs/Airplane", spawnPos, Quaternion.Euler(0, 0, 0));
            _airplane.transform.LookAt(destinationPos);            
            //Vector3 airplanePos = airplane.transform.position;
            _airplane.GetComponent<airplane>().destination = new Vector3(destinationPos.x, destinationPos.y, destinationPos.z);
            //airplane.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
    }

    void change_airplane_spawn_point()
    {

    }
}
