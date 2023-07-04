using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System;
using TMPro;
using UnityEngine.Networking;
using System.Net.Mail;
using Unity.FPS.UI;

struct LoginReplyData
{
    public bool success;
    public string message;
    public int team_id;
    public int room_id;

    public LoginReplyData(bool success, string message, int team_id, int room_id)
    {
        this.success = success;
        this.message = message;
        this.team_id = team_id;
        this.room_id = room_id;
    }
}

struct LoginData
{
    public string email;
    public string password;

    public LoginData(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}

public class LoginFunctions : MonoBehaviour
{
    public Selectable emailInput;
    public Selectable passInput;
    public Cloud cloud;
    public TextMeshProUGUI warningDisplay;

    public Button loginButton;
    public int roomID;

    public string loginAPI = "https://irgl.petra.ac.id/2022/backend/semifinal_apis/login/index.php";

    // Start is called before the first frame update
    void Start()
    {
        cloud.teamID = -999;
        cloud.email = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClearErrorMessage()
    {
        warningDisplay.text = "";
    }

    IEnumerator APILogin(string email, string pass)
    {
        string json_data = JsonUtility.ToJson(new LoginData(email, pass));
        loginButton.GetComponent<LoadSceneButton>().LoadTargetScene(false);
        #region old
        UnityWebRequest www = new UnityWebRequest(loginAPI, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json_data);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        Debug.Log(json_data);
        Debug.Log(www.downloadHandler.text);
        // loginButton.GetComponent<LoadSceneButton>().LoadTargetScene();
        if (www.responseCode != 500)
        {
            string json_response = www.downloadHandler.text;
            try
            {
                LoginReplyData json_obj = JsonUtility.FromJson<LoginReplyData>(json_response);
                warningDisplay.text = json_obj.message;
                if (json_obj.success)
                {
                    int teamID = json_obj.team_id;
                    int _roomID = json_obj.room_id; // rooom id as roomID
                    warningDisplay.text = json_obj.message;
                    if (teamID == -1)
                    {
                        // logged as admin
                        loginButton.GetComponent<LoadSceneButton>().LoadTargetScene(true);
                        yield break;
                    }
                    else
                    {
                        if (roomID == _roomID)
                        {
                            cloud.email = email;
                            cloud.teamID = teamID;
                            warningDisplay.text = json_obj.message;

                            loginButton.GetComponent<LoadSceneButton>().LoadTargetScene(false);
                            yield break;
                        }
                        else
                        {
                            warningDisplay.text = "wrong room please contact IRGL CP";
                        }
                    }                    
                }                   
            }
            catch (System.Exception)
            {
                warningDisplay.text = "Server error, please contact Mikael Hans!";
            }
        }
        else
        {
            warningDisplay.text = "Server error, please contact Mikael Hans!";
        }
        #endregion
    }

    public void Login()
    {
        if (emailInput.GetComponent<TMP_InputField>().text == "")
        {
            warningDisplay.text = "Email cannot be empty!";
            return;
        }

        //try
        //{
        //    MailAddress m = new MailAddress(emailInput.GetComponent<TMP_InputField>().text);
        //}
        //catch (FormatException)
        //{
        //    warningDisplay.text = "Email invalid!";
        //    return;
        //}

        //if (passInput.GetComponent<TMP_InputField>().text == "")
        //{
        //    warningDisplay.text = "Password cannot be empty!";
        //    return;
        //}

        string email = emailInput.GetComponent<TMP_InputField>().text;
        string password = passInput.GetComponent<TMP_InputField>().text;

        warningDisplay.text = "Logging in...";
        loginButton.GetComponent<LoadSceneButton>().LoadTargetScene(false);
        //StartCoroutine(APILogin(email, password));
        //     UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        // }
    }
}
