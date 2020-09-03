using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Skill
{
    public string teamTarget;
    public Renderer render;
    public GameObject bulletPrefab;
    public override void Prepare(GameObject _owner)
    {
        characterStatus = _owner.GetComponent<CharacterStatus>();
        teamTarget = _owner.GetComponent<AI_Control>().enemyTeam;
    }
    public override void ActionNow()
    {
        render.enabled = true;
        Destroy(gameObject, 0.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == teamTarget && other.gameObject.layer == 8)
        {
            if (other.GetComponent<CharacterStatus>())
            {
                if (!other.GetComponent<CharacterStatus>().CheckDead())
                {
                    GameObject obj = Instantiate(bulletPrefab, characterStatus.GetComponent<AI_Control>().attackPoint.position, Quaternion.identity) as GameObject;
                    Bullet bullet = obj.GetComponent<Bullet>();
                    bullet.SetStart(characterStatus.GetATK(), other.gameObject);
                }
            }
        }
    }
}
