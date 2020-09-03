using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgUp : Skill
{
    public int dmgPower = 2;
    public float duration = 5;
    public Renderer render;
    private int originDmg;
    public override void Prepare(GameObject _owner)
    {
        characterStatus = _owner.GetComponent<CharacterStatus>();
        originDmg = characterStatus.GetATK();
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
        characterStatus.SetATK(originDmg + dmgPower);
        characterStatus.hasBuf = true;
        yield return new WaitForSeconds(duration);
        characterStatus.SetATK(originDmg);
        render.enabled = false;
        characterStatus.hasBuf = false;

        Destroy(gameObject);
    }
}
