using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MultiAccountLoginCheck : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        List<Player> allplayer = new List<Player>(FindObjectsOfType<Player>());

        foreach (Player player in allplayer)
        {
            if (player.photonView.Owner.NickName == PhotonNetwork.NickName && !player.photonView.IsMine)
            {
                Debug.Log(player.photonView.Owner.NickName);
                Debug.Log(PhotonNetwork.NickName);
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene(0);
            }
        }
    }
}
