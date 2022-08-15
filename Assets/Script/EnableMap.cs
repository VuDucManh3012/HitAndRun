using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMap : MonoBehaviour
{
    private Vector3 myPosition;
    public GameObject Character;
    private Vector3 CharacterPosition;
    // Start is called before the first frame update
    void Start()
    {
        GetCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckPositionZToEnable();
    }
    void CheckPositionZToEnable()
    {
        //Get my Position
        myPosition = transform.position;

        //Get character position
        CharacterPosition = Character.transform.position;

        //CheckDistanceToEnable
        if (myPosition.z - CharacterPosition.z <= 170)
        {
            transform.gameObject.SetActive(true);
        }
        else
        {
            transform.gameObject.SetActive(false);
        }
        
    }
    void GetCharacter()
    {
        Character = transform.parent.Find("Character").gameObject;
        Debug.Log(Character);
    }
}
