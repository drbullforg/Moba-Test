using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Control : MonoBehaviour
{
    public string team;
    public GameObject moveWaypoint;
    public GameObject moveTarget;
    public GameObject target;
    protected CharacterStatus targetStatus;
    protected CharacterMovement characterMovement;

    public bool isHero;
    public bool isMovement;
    public string state = "Standby";
    public Transform attackPoint;
    public GameObject bulletPrefab;

    public string enemyTeam;
    public CharacterStatus status;

    public float atkSpeed = 10;
    protected float atkCountingTime = 0;
    [SerializeField]
    protected Collider areaTrigger;
    [SerializeField]
    protected Collider attackTrigger;

    private bool isSkillInUsed;

    //public List<GameObject> enemyList = new List<GameObject>();

    protected void Start()
    {
        status = GetComponent<CharacterStatus>();
        areaTrigger = GetComponentInChildren<AreaTrigger>().GetComponent<Collider>();
        attackTrigger = GetComponentInChildren<AttackTrigger>().GetComponent<Collider>();

        if(isMovement)
        {
            characterMovement = GetComponent<CharacterMovement>();
        }
        
        if(team == "TeamA")
        {
            enemyTeam = "TeamB";
        }
        else
        {
            enemyTeam = "TeamA";
        }
    }

    public virtual void OnAreaTriggerEnter(Collider col)
    {
        Debug.Log(gameObject.name + " AreaTriggerEnter " + col.name);
        if (col.gameObject.tag == enemyTeam && col.gameObject.layer == 8 && !isHero)
        {
            if(!moveTarget)
                moveTarget = col.gameObject;
            //enemyList.Add(col.gameObject);
            state = "MoveToTarget";
        }
    }
    //public void OnAreaTriggerStay(Collider col)
    //{
    //    if(!moveTarget)
    //    {
    //        Debug.Log(gameObject.name + " AreaTriggerStay " + col.name);
    //        if (col.gameObject.tag == enemyTeam && col.gameObject.layer == 8)
    //        {
    //            moveTarget = col.gameObject;
    //            state = "MoveToTarget";
    //        }
    //    }
    //}
    public void OnAreaTriggerExit(Collider col)
    {
        if(col.gameObject == moveTarget)
        {
            moveTarget = null;
        }

        //enemyList.Remove(col.gameObject);
    }

    public virtual void OnAttackTriggerEnter(Collider col)
    {
        if (!target)
        {
            if (col.gameObject.tag == enemyTeam && col.gameObject.layer == 8 && !isHero)
            {
                moveTarget = col.gameObject;
                target = col.gameObject;
                targetStatus = target.GetComponent<CharacterStatus>();
                state = "Attack";
            }
        }
    }
    public virtual void OnAttackTriggerStay(Collider col)
    {
        if (!target)
        {
            if (col.gameObject.tag == enemyTeam && col.gameObject.layer == 8 && !isHero)
            {
                target = col.gameObject;
                targetStatus = target.GetComponent<CharacterStatus>();
                state = "Attack";
            }
        }
    }
    public void OnAttackTriggerExit(Collider col)
    {
        if (col.gameObject == target)
        {
            target = null;
        }
    }

    protected IEnumerator ResetTrigger()
    {
        areaTrigger.enabled = false;
        attackTrigger.enabled = false;

        yield return new WaitForSeconds(0.1f);

        areaTrigger.enabled = true;
        attackTrigger.enabled = true;
    }

    protected virtual void Update()
    {
        if (GameSystem.instance.gameState == GameState.Start)
        {
            if (atkCountingTime > 0)
            {
                atkCountingTime -= Time.deltaTime;
            }

            if (state == "Standby")
            {
                if (isMovement)
                {
                    if (moveTarget)
                    {
                        state = "MoveToTarget";
                    }
                    else
                    {
                        StartCoroutine(ResetTrigger());

                        moveWaypoint = CheckWaypoint();
                        state = "MoveToWaypoint";
                    }
                }

                if (target)
                {
                    state = "Attack";
                }
            }
            else if (state == "MoveToWaypoint")
            {
                if (isMovement)
                {
                    if (moveWaypoint)
                    {
                        transform.LookAt(new Vector3(moveWaypoint.transform.position.x, transform.position.y, moveWaypoint.transform.position.z), Vector3.up);
                        //transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, characterMovement.playerSpeed);
                        Vector3 move = (moveWaypoint.transform.position - transform.position).normalized;
                        characterMovement.controller.Move(move * Time.deltaTime * characterMovement.playerSpeed);
                    }
                    else
                    {
                        state = "Standby";
                    }
                }
                else
                {
                    state = "Standby";
                }
            }
            else if (state == "MoveToTarget")
            {
                if (isMovement)
                {
                    if (moveTarget)
                    {
                        transform.LookAt(new Vector3(moveTarget.transform.position.x, transform.position.y, moveTarget.transform.position.z), Vector3.up);
                        //transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, characterMovement.playerSpeed);
                        Vector3 move = (moveTarget.transform.position - transform.position).normalized;
                        characterMovement.controller.Move(move * Time.deltaTime * characterMovement.playerSpeed);
                    }
                    else
                    {
                        state = "Standby";
                    }
                }
                else
                {
                    state = "Standby";
                }
            }
            else if (state == "Attack")
            {
                if (target)
                {
                    transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), Vector3.up);

                    if (isSkillInUsed)
                    {
                        // use skill type selection
                    }
                    else
                    {
                        if (atkCountingTime <= 0)
                        {
                            AttackNow();
                            atkCountingTime = atkSpeed;
                        }

                        if (targetStatus.CheckDead())
                        {
                            target = null;
                            state = "Standby";
                        }
                    }
                }
                else
                {
                    state = "Standby";
                }
            }
        }
    }

    protected void AttackNow()
    {
        GameObject obj = Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity) as GameObject;
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.SetStart(status.GetATK(), target);
    }

    protected GameObject CheckWaypoint()
    {
        return GameSystem.instance.GetWaypoint(team);
    }

    public void CheckEnemyList(GameObject enemy)
    {
        //if (enemyList.Contains(enemy))
        //{
        //    Debug.Log(gameObject.name + "Has Enemy " + enemy.name);
        //    enemyList.Remove(enemy);
        //}
    }
}
