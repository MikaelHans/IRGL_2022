using System.Collections;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using Photon.Pun;

struct ScoreUpdateData
{
    public string email;
    public int delta;

    public ScoreUpdateData(string email, int delta)
    {
        this.email = email;
        this.delta = delta;
    }
}

public class ScoreUpdater : MonoBehaviourPun
{
    public string scoreAPI = "https://irgl.petra.ac.id/2022/backend/semifinal_apis/score/index.php";

    IEnumerator APIScore(string email, int delta)
    {
        string json_data = JsonUtility.ToJson(new ScoreUpdateData(email, delta));

        UnityWebRequest www = new UnityWebRequest(scoreAPI, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json_data);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();
        yield break;
    }

    public void UpdateScoreBy(int delta)
    {
        if (PhotonNetwork.IsConnected && photonView.IsMine)
        {
            StartCoroutine(APIScore(PhotonNetwork.NickName, delta));
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
