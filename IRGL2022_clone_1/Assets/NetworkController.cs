using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private Transform ThisObjTransform;

    Vector3 incomingPosContainer = new Vector3();
    Vector3 incomingRotContainer = new Vector3();

    [SerializeField]
    private float lerp_speed;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.ThisObjTransform.position.x);
            stream.SendNext(this.ThisObjTransform.position.y);
            stream.SendNext(this.ThisObjTransform.position.z);

            stream.SendNext(this.ThisObjTransform.rotation.y);
        }
        else
        {
            incomingPosContainer = ThisObjTransform.position;
            float x = (float)stream.ReceiveNext();
            float y = (float)stream.ReceiveNext();
            float z = (float)stream.ReceiveNext();
            incomingPosContainer = new Vector3(x, y, z);


            float rotY = (float)stream.ReceiveNext();
            incomingRotContainer = new Vector3(0, rotY, 0);
        }
        
    }

    private void Awake()
    {
        ThisObjTransform = gameObject.GetComponent<Transform>();
        gameObject.GetComponent<PhotonView>().ObservedComponents.Add(this);
    }


    private void FixedUpdate()
    {
        //Debug.Log("tes");
        if (!this.photonView.IsMine)//update postion through the network
        {
            ThisObjTransform.position = Vector3.Lerp(ThisObjTransform.position, incomingPosContainer, Time.deltaTime * lerp_speed);
            ThisObjTransform.eulerAngles = Vector3.Lerp(ThisObjTransform.eulerAngles, incomingRotContainer, 10);
            //controller.Move(this.ThisObjTransform.position, this.incomingPosContainer);
            //this.ThisObjTransform.position = Vector2.MoveTowards(this.ThisObjTransform.position, this.incomingPosContainer, 1f * (1.0f / PhotonNetwork.SerializationRate));
        }
    }
}
