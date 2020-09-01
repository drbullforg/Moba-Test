using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_PC : MonoBehaviour
{
    public CharacterMovement character;
    public Transform cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterMovement>();
        cameraPos = Camera.main.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    character.Move(cameraPos.right);
        //    character.Rotate(cameraPos.forward);
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    character.Move(cameraPos.forward);
        //    character.Rotate(-cameraPos.right);
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    character.Move(-cameraPos.right);
        //    character.Rotate(-cameraPos.forward);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    character.Move(-cameraPos.forward);
        //    character.Rotate(cameraPos.right);
        //}
    }
}
