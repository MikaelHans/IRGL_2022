using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GenericNetworkController : MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    public bool rotation, position;
    [SerializeField]
    private Transform ThisObjTransform;

    Vector3 incomingPosContainer = new Vector3();

    [SerializeField]
    private float lerp_speed;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (position)
            {
                stream.SendNext(this.ThisObjTransform.position.x);
                stream.SendNext(this.ThisObjTransform.position.y);
                stream.SendNext(this.ThisObjTransform.position.z);
            }
            if(rotation)
            {
                stream.SendNext(this.ThisObjTransform.rotation.x);
                stream.SendNext(this.ThisObjTransform.rotation.y);
                stream.SendNext(this.ThisObjTransform.rotation.z);
            }           
        }
        else
        {
            if (position)
            {
                incomingPosContainer = ThisObjTransform.position;
                float x = (float)stream.ReceiveNext();
                float y = (float)stream.ReceiveNext();
                float z = (float)stream.ReceiveNext();
                incomingPosContainer = new Vector3(x, y, z);
            }
            if(rotation)
            {
                float rotX = (float)stream.ReceiveNext();
                float rotY = (float)stream.ReceiveNext();
                float rotZ = (float)stream.ReceiveNext();
                ThisObjTransform.localEulerAngles = new Vector3(rotX, rotY, rotZ);
            }                     
        }
    }

    private void Awake()
    {
        ThisObjTransform = gameObject.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        //Debug.Log("tes");
        if (!this.photonView.IsMine)//update postion through the network
        {
            this.ThisObjTransform.position = Vector3.Lerp(this.ThisObjTransform.position, this.incomingPosContainer, Time.deltaTime * lerp_speed);
            //this.ThisObjTransform.position = incomingPosContainer;
            //controller.Move(this.ThisObjTransform.position, this.incomingPosContainer);
            //this.ThisObjTransform.position = Vector2.MoveTowards(this.ThisObjTransform.position, this.incomingPosContainer, 1f * (1.0f / PhotonNetwork.SerializationRate));
        }
    }
}
