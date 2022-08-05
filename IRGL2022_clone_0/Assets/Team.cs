using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team
{
    [SerializeField]
    int team_id;
    public int score;

    public Team(int _team_id, int _score = 0)
    {
        team_id = _team_id;
        score = _score; 
    }

    public int Team_id { get => team_id; set => team_id = value; }
}
