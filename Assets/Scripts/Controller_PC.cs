using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class Controller_PC : AI_Control
{
    public override void OnAreaTriggerEnter(Collider col)
    {
        Debug.Log(gameObject.name + " AreaTriggerEnter " + col.name);
        if (col.gameObject.tag == enemyTeam && col.gameObject.layer == 8)
        {
            if (!moveTarget)
                moveTarget = col.gameObject;
            //enemyList.Add(col.gameObject);
            //state = "MoveToTarget";
        }
    }

    public override void OnAttackTriggerEnter(Collider col)
    {
        if (!target)
        {
            if (col.gameObject.tag == enemyTeam && col.gameObject.layer == 8)
            {
                moveTarget = col.gameObject;
                target = col.gameObject;
                targetStatus = target.GetComponent<CharacterStatus>();
                state = "Attack";
            }
        }
    }

    public override void OnAttackTriggerStay(Collider col)
    {
        if (!target)
        {
            if (col.gameObject.tag == enemyTeam && col.gameObject.layer == 8)
            {
                moveTarget = col.gameObject;
                target = col.gameObject;
                targetStatus = target.GetComponent<CharacterStatus>();
                state = "Attack";
            }
        }
    }

    protected override void Update()
    {
        if (GameSystem.instance.gameState == GameState.Start)
        {
            if (atkCountingTime > 0)
            {
                atkCountingTime -= Time.deltaTime;
            }

            if (TCKInput.GetAction("fireBtn", EActionEvent.Click))
            {
                if (target)
                {
                    state = "Attack";
                }
                else if (moveTarget)
                {
                    state = "MoveToTarget";
                }
                else
                {
                    StartCoroutine(ResetTrigger());
                }
            }

            if(TCKInput.GetAction("skillBtn-1", EActionEvent.Click))
            {
                GetComponent<SkillList>().UseSkill(0);
            }

            if (TCKInput.GetAction("skillBtn-2", EActionEvent.Click))
            {
                GetComponent<SkillList>().UseSkill(1);
            }

            if (TCKInput.GetAction("skillBtn-3", EActionEvent.Click))
            {
                GetComponent<SkillList>().UseSkill(2);
            }

            if (state == "Standby")
            {
                if (isMovement)
                {
                    if (moveTarget)
                    {
                        state = "MoveToTarget";
                    }

                    if (target)
                    {
                        state = "Attack";
                    }
                }
            }
            else if(state == "Control")
            {

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
                }
            }
            else if (state == "Attack")
            {
                if (target)
                {
                    transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), Vector3.up);

                    if (atkCountingTime <= 0)
                    {
                        AttackNow();
                        atkCountingTime = atkSpeed;
                    }

                    if (targetStatus.CheckDead())
                    {
                        target = null;
                        //state = "Standby";
                        StartCoroutine(ResetTrigger());
                    }
                }
            }
        }
    }
}
