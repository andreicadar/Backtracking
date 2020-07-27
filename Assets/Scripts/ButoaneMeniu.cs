using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButoaneMeniu : MonoBehaviour
{

    public GameObject cursor;
    private void Start()
    {
        cursor = gameObject.transform.Find("Cursor").gameObject;
        cursor.SetActive(false);
    }
    public void Intrare()
    {
        gameObject.transform.rotation = Quaternion.Euler(5, 5, 5);
        cursor.SetActive(true);
        Debug.Log("aleluia");

    }
    public void Iesire()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        cursor.SetActive(false);
    }
}
