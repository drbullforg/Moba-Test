using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTarget : Skill
{
    public int healPower = 10;
    public override void Prepare(GameObject _owner)
    {
        characterStatus = _owner.GetComponent<CharacterStatus>();
    }
    public override void ActionNow()
    {
        characterStatus.Damage(-healPower);
        Destroy(gameObject);
    }
}
