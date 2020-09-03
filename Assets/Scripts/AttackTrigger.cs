using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public AI_Control ai;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject.name + " AttackTriggerEnter " + other.name);
        ai.OnAttackTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!ai.target)
        {
            if(other.GetComponent<CharacterStatus>())
            {
                if(!other.GetComponent<CharacterStatus>().CheckDead())
                {
                    ai.OnAttackTriggerStay(other);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ai.OnAttackTriggerExit(other);
    }
}
