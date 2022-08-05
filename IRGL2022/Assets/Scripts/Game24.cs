using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class Game24 : Minigame
{
    private string expresion = "";
    public List<int> problems = new List<int>();   
    public TextMeshProUGUI expGUI;
    public Text[] btnAngka = new Text[4];
    public Button[] buttons = new Button[4];
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init()
    {
        base.Init();
        Debug.Log("init");
        expresion = "";
        expGUI.text = expresion;
        int start = UnityEngine.Random.Range(0, 1999);
        start /= 4;
        for (int i = start; i < start + 4; i++) 
        {
            int x = i;
            btnAngka[i].text = problems[i].ToString();
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].interactable = true;
            buttons[i].onClick.AddListener(delegate { addoperand(problems[x].ToString(), buttons[x]);});
        }
    }

    public void addExpression(string op)
    {
        if (expresion.Length > 0)
        {
            string substr = expresion.Substring(expresion.Length - 1);
            if (substr != "(" && substr != "+" && substr != "-" && substr != "*" && substr != "/" && substr != ")")
            {
                expresion += op;
                expGUI.text = expresion;
            }
        }
    }

    public void addoperand(string op, Button button)
    {
        if(expresion.Length > 0)
        {
            string substr = expresion.Substring(expresion.Length - 1);
            if(substr == "(" || substr == "+" || substr == "-" || substr == "*" || substr == "/")
            {
                expresion += op;
                expGUI.text = expresion;
                button.interactable = false;
            }
        }
        else
        {
            expresion += op;
            expGUI.text = expresion;
            button.interactable = false;
        }
    }

    public void Check()
    {
        DataTable dt = new DataTable();
        
        bool isAllnumberUsed = true;
        for(int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].interactable)
                isAllnumberUsed = false;
        }
        if(Convert.ToDecimal(dt.Compute(expresion, "")) == 24 && isAllnumberUsed)
        {
            chest.dropContent();
        }
        else
        {
            closeWindow();
        }
    }

    public void reset()
    {
        for (int i = 0; i < 4; i++)
        {
            buttons[i].interactable = true;
        }
        expresion = "";
        expGUI.text = expresion;
    }
}
