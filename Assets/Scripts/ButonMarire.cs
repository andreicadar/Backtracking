using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButonMarire : MonoBehaviour
{
    public GameObject pozaTest;
    void Start()
    {
        pozaTest = gameObject.transform.Find("test").gameObject;
        pozaTest.SetActive(false);
    }

    public void marire_intrare()
    {
        gameObject.transform.localScale = new Vector3(2.25f, 2.25f);
        pozaTest.SetActive(true);

    }
    public void marire_iesire()
    {
        gameObject.transform.localScale = new Vector3(2f, 2f);
        pozaTest.SetActive(false);
    }
}
