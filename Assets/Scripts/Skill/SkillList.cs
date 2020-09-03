using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    public Skill[] skills;

    public void UseSkill(int id)
    {
        GameObject skillObj;
        
        if(skills[id].isWorld)
        {
            skillObj = Instantiate(skills[id].gameObject, GetComponent<AI_Control>().attackPoint.position, Quaternion.identity)as GameObject;
        }
        else
        {
            skillObj = Instantiate(skills[id].gameObject, GetComponent<AI_Control>().attackPoint)as GameObject;
        }
        
        Skill skill = skillObj.GetComponent<Skill>();

        skill.Prepare(gameObject);
        skill.ActionNow();
    }
}
