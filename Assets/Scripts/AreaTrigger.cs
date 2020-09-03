using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public AI_Control ai;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterStatus>())
        {
            if (!other.GetComponent<CharacterStatus>().CheckDead())
            {
                ai.OnAreaTriggerEnter(other);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (!ai.moveTarget)
    //    {
    //        ai.OnAreaTriggerStay(other);
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        ai.OnAreaTriggerExit(other);
    }
}
