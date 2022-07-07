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
    public int id_team;

    public LoginReplyData(bool success, string message, int id_team)
    {
        this.success = success;
        this.message = message;
        this.id_team = id_team;
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

    public string loginAPI;

    // Start is called before the first frame update
    void Start()
    {
        cloud.teamID = "";
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
        string json_data = JsonUtility.ToJson(new LoginData(email, pass), true);
        UnityWebRequest www = UnityWebRequest.Post(loginAPI, json_data);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        if (www.responseCode != 500)
        {
            string json_response = www.downloadHandler.text;
            LoginReplyData json_obj = JsonUtility.FromJson<LoginReplyData>(json_response);

            if (json_obj.success)
            {
                string teamID = json_obj.id_team.ToString();
                cloud.email = email;
                cloud.teamID = teamID;
                warningDisplay.text = "Login Success!";

                loginButton.GetComponent<LoadSceneButton>().LoadTargetScene();

                yield break;
            }
            else
            {
                warningDisplay.text = "Invalid email or password!";
            }
        }
        else
        {
            warningDisplay.text = "Server error, please contact Mikael Hans!";
        }
    }

    public void Login()
    {
        if (emailInput.GetComponent<TMP_InputField>().text == "")
        {
            warningDisplay.text = "Email cannot be empty!";
            return;
        }

        try
        {
            MailAddress m = new MailAddress(emailInput.GetComponent<TMP_InputField>().text);
        }
        catch (FormatException)
        {
            warningDisplay.text = "Email invalid!";
            return;
        }

        if (passInput.GetComponent<TMP_InputField>().text == "")
        {
            warningDisplay.text = "Password cannot be empty!";
            return;
        }

        string email = emailInput.GetComponent<TMP_InputField>().text;
        string password = passInput.GetComponent<TMP_InputField>().text;

        warningDisplay.text = "Logging in...";
        StartCoroutine(APILogin(email, password));
        //     UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        // }
    }
}
