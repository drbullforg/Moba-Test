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

            if (TCKInput.GetAction("fireBtn", EActionEvent.Press))
            {
                StartCoroutine(ResetTrigger());

                if (moveTarget)
                {
                    state = "MoveToTarget";
                }

                if(target)
                {
                    state = "Attack";
                }
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
                    if (atkCountingTime <= 0)
                    {
                        AttackNow();
                        atkCountingTime = atkSpeed;
                    }

                    if (targetStatus.CheckDead())
                    {
                        target = null;
                        //state = "Standby";
                    }
                }
            }
        }
    }
}
