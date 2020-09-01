using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{
    public bool isHero;

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _level;
    [SerializeField]
    private int _maxHP;
    [SerializeField]
    private int _currentHP;
    [SerializeField]
    private int _atk;

    private bool isDead;

    public bool useHud;
    public GameObject hudPrefab;
    public GameObject hudPoint;
    public Slider hpbar;

    private void Start()
    {
        if(useHud)
        {
            GameObject hud = Instantiate(hudPrefab, hudPoint.transform.position, Quaternion.identity) as GameObject;
            hpbar = hud.GetComponentInChildren<Slider>();
            hud.GetComponent<Canvas>().worldCamera = Camera.main;
            hud.GetComponent<CameraFollow>().target = hudPoint;
        }
    }

    public string GetName()
    {
        return _name;
    }
    public void SetName(string str)
    {
        _name = str;
    }

    public int GetHP()
    {
        return _currentHP;
    }
    public void SetHP(int value)
    {
        _currentHP = value;
        if(hpbar)
        {
            float hp = (_currentHP * 1.0f) / (_maxHP * 1.0f);
            hpbar.value = hp;
        }

        if(_currentHP <= 0)
        {
            isDead = true;

            GameSystem.instance.CheckObjectInWaypoint(gameObject.tag, gameObject);

            if(isHero)
            {

            }
            else
            {
                if (hpbar)
                {
                    Destroy(hpbar.transform.parent.gameObject);
                }
                Destroy(gameObject, 0.1f);
            }
        }
    }

    //public int GetMP()
    //{
    //    return _mp;
    //}
    //public void SetMP(int value)
    //{
    //    _mp = value;
    //}

    public int GetLevel()
    {
        return _level;
    }
    public void SetLevel(int value)
    {
        _level = value;
    }

    public int GetATK()
    {
        return _atk;
    }
    public void SetATK(int value)
    {
        _atk = value;
    }

    public void Damage(int value)
    {
        //GameSystem.instance.BroardcastCheck(gameObject.tag, gameObject);
        SetHP(GetHP() - value);
    }

    public bool CheckDead()
    {
        return isDead;
    }
}
