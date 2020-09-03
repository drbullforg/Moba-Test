using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpdUp : Skill
{
    public int spdPower = 2;
    public float duration = 5;
    public Renderer render;
    private float originSpd;
    private AI_Control aiControl;
    public override void Prepare(GameObject _owner)
    {
        characterStatus = _owner.GetComponent<CharacterStatus>();
        aiControl = _owner.GetComponent<AI_Control>();
        originSpd = aiControl.atkSpeed;
    }
    public override void ActionNow()
    {
        if (!characterStatus.hasBuf)
            StartCoroutine(DamageUpNow());
        else
            Destroy(gameObject);
    }

    IEnumerator DamageUpNow()
    {
        render.enabled = true;
        aiControl.atkSpeed /= 2;
        characterStatus.hasBuf = true;
        yield return new WaitForSeconds(duration);
        aiControl.atkSpeed = originSpd;
        render.enabled = false;
        characterStatus.hasBuf = false;

        Destroy(gameObject);
    }
}
