using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public string skillName;
    public bool isWorld;
    [SerializeField]
    protected CharacterStatus characterStatus;
    public virtual void Prepare(GameObject _owner)
    {
        
    }
    public virtual void ActionNow()
    {

    }
}
