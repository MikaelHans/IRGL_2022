using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public List<Team> Teams;

    [SerializeField]
    public Dictionary<int, int> scores = new Dictionary<int, int>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void update_team_score(int team_id, int score = 100)
    {
        bool flag = true;
        foreach(Team team in Teams)
        {
            if(team.Team_id == team_id)
            {
                team.score += score;
                flag = false;
            }
        }
        if(flag)
        {
            Teams.Add(new Team(team_id, score));
        }
        Teams.Sort();
    }

}
