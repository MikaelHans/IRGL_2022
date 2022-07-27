using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public Team[] Teams = new Team[20];

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        for(int i = 0; i < Teams.Length; i++)
        {
            Teams[i] = new Team(i);
        }
    }

    public void update_team_score(int team_id, int score = 100)
    {
        if(team_id < Teams.Length)
        {
            Teams[team_id].score += score;
        }
    }

}
