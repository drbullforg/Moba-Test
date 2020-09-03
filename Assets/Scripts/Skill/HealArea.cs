using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : Skill
{
    public int healPower = 10;
    public string teamTarget;
    public Animator animator;
    public Renderer render;
    public override void Prepare(GameObject _owner)
    {
        characterStatus = _owner.GetComponent<CharacterStatus>();
        teamTarget = _owner.GetComponent<AI_Control>().team;
    }
    public override void ActionNow()
    {
        animator.enabled = true;
        render.enabled = true;
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == teamTarget && other.gameObject.layer == 8)
        {
            if (other.GetComponent<CharacterStatus>())
            {
                if (!other.GetComponent<CharacterStatus>().CheckDead())
                {
                    other.GetComponent<CharacterStatus>().Damage(-healPower);
                }
            }
        }
    }
}
