using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public int atk;
    public GameObject target;

    private Collider col;
    private Renderer render;

    private bool isStart;

    private void Awake()
    {
        col = GetComponent<Collider>();
        render = GetComponent<Renderer>();
        Destroy(gameObject, 1f);
    }

    public void SetStart(int _atk, GameObject _target)
    {
        atk = _atk;
        target = _target;
        isStart = true;
        col.enabled = true;
        render.enabled = true;
    }

    private void Update()
    {
        if (isStart)
        {
            if (target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target)
        {
            target.GetComponent<CharacterStatus>().Damage(atk);
            Destroy(gameObject);
        }
    }
}
