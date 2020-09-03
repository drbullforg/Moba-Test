using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Ready,
    Start,
    End
}

public enum Team
{
    TeamA,
    TeamB
}

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    public float timeScale = 1;

    public GameState gameState;

    public List<GameObject> waypointTeamA = new List<GameObject>();
    public List<GameObject> waypointTeamB = new List<GameObject>();

    public Transform heroA_spawnPoint;
    public Transform heroB_spawnPoint;
    public float heroResetTime = 3;
    public GameObject[] minionsMeleePrefab;
    public GameObject[] minionsRangePrefab;

    public Transform[] minionsA_Pos;
    public Transform[] minionsB_Pos;

    public List<GameObject> teamA_Objs = new List<GameObject>();
    public List<GameObject> teamB_Objs = new List<GameObject>();

    public Text[] btnSkills;

    public GameObject player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetStartGame();
    }

    public void SetStartGame()
    {
        SetSkillBtn(player.GetComponent<SkillList>().skills);
        gameState = GameState.Start;
        InvokeRepeating("SpawnMinions", 1, 30);
    }

    public void SetSkillBtn(Skill[] skills)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            btnSkills[i].text = skills[i].skillName;
        }
    }

    private void Update()
    {
        Time.timeScale = timeScale;
    }

    public void ResetHero(GameObject hero)
    {
        StartCoroutine(WaitingReset(hero));
    }

    IEnumerator WaitingReset(GameObject hero)
    {
        hero.SetActive(false);
        if (hero.tag == "TeamA")
        {
            hero.transform.position = heroA_spawnPoint.position;
        }
        else
        {
            hero.transform.position = heroB_spawnPoint.position;
        }
        yield return new WaitForSeconds(heroResetTime);
        hero.GetComponent<CharacterStatus>().Reset();
        hero.SetActive(true);
    }

    void SpawnMinions()
    {
        for (int i = 0; i < minionsA_Pos.Length; i++)
        {
            if (i == 0)
            {
                GameObject teamA = Instantiate(minionsRangePrefab[0], minionsA_Pos[i].position, Quaternion.identity) as GameObject;
                GameObject teamB = Instantiate(minionsRangePrefab[1], minionsB_Pos[i].position, Quaternion.identity) as GameObject;

                teamA_Objs.Add(teamA);
                teamB_Objs.Add(teamB);
            }
            else
            {
                GameObject teamA = Instantiate(minionsMeleePrefab[0], minionsA_Pos[i].position, Quaternion.identity) as GameObject;
                GameObject teamB = Instantiate(minionsMeleePrefab[1], minionsB_Pos[i].position, Quaternion.identity) as GameObject;

                teamA_Objs.Add(teamA);
                teamB_Objs.Add(teamB);
            }
        }
    }

    public GameObject GetWaypoint(string team)
    {
        if (team == "TeamA")
        {
            return waypointTeamB.ToArray()[0];
        }
        else
        {
            return waypointTeamA.ToArray()[0];
        }
    }

    public void CheckObjectInWaypoint(string team, GameObject obj)
    {
        if (team == "TeamA")
        {
            if (waypointTeamA.Contains(obj))
            {
                waypointTeamA.Remove(obj);

                if (waypointTeamA.Count <= 0)
                {
                    GameEnd("TeamB");
                }
            }
        }
        else
        {
            if (waypointTeamB.Contains(obj))
            {
                waypointTeamB.Remove(obj);

                if (waypointTeamB.Count <= 0)
                {
                    GameEnd("TeamA");
                }
            }
        }
    }

    public void BroardcastCheck(string team, GameObject obj)
    {
        if (team == "TeamA")
        {
            foreach (GameObject item in teamB_Objs)
            {
                item.GetComponent<AI_Control>().CheckEnemyList(obj);
            }
        }
        else
        {
            foreach (GameObject item in teamA_Objs)
            {
                item.GetComponent<AI_Control>().CheckEnemyList(obj);
            }
        }
    }

    public void GameEnd(string teamWin)
    {
        gameState = GameState.End;
        CancelInvoke("SpawnMinions");
        Debug.Log(teamWin + " Win!");
    }
}
