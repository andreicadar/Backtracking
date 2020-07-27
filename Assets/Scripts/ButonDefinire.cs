using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButonDefinire : MonoBehaviour
{
    private Transform definition;
    private void Start()
    {
        definition = gameObject.transform.Find("Definition");
    }
    public void Intrare()
    {
        definition.gameObject.SetActive(true);
    }
    public void Iesire()
    {
        definition.gameObject.SetActive(false);
    }
}
