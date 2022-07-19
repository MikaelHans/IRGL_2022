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

    public float minimumDist;
    int seed = 2022;

    // Start is called before the first frame updates
    void Start()
    {        
        if(PhotonNetwork.IsMasterClient)
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

            float headingAngle = Vector3.Angle(spawnPos, destinationPos);

            GameObject _airplane = PhotonNetwork.InstantiateRoomObject("Prefabs/Airplane", spawnPos, Quaternion.Euler(0, headingAngle, 0));
            //Vector3 airplanePos = airplane.transform.position;
            _airplane.GetComponent<airplane>().destination = destinationPos;
            //airplane.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            change_airplane_spawn_point();
        }      
        
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

    public Vector3 GetRandomPointInEdge()
    {
        Random.InitState(seed++);
        Vector3 pos = new Vector3();
        float x = Random.Range(-(transform.localScale.x / 100) + transform.position.x, transform.localScale.x / 100 + transform.position.y);
        float z = Mathf.Sqrt(Mathf.Pow(transform.localScale.x / 100, 2) - Mathf.Pow(x, 2));
        pos.x = x;
        pos.z = z;
        pos.y = airplane_spawn_point.transform.position.y;
        Debug.Log("X: " + x + "; Y: " + z);
        return pos;
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
