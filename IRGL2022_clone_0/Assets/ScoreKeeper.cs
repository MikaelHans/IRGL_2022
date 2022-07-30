using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScoreKeeper : MonoBehaviourPun
{
    public List<Team> Teams;
    public ScoreUpdater scoreupdater;

    [SerializeField]
    public Dictionary<int, int> scores = new Dictionary<int, int>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void update_team_score(int team_id, int score = 100)
    {
        scoreupdater.UpdateScoreBy(score);
        if(PhotonNetwork.IsMasterClient)
        {
            bool flag = true;
            foreach (Team team in Teams)
            {
                if (team.Team_id == team_id)
                {
                    team.score += score;
                    flag = false;
                }

            }
            if (flag)
            {
                Teams.Add(new Team(team_id, score));
            }

        }
    }

}
