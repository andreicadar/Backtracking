using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introducere : MonoBehaviour
{
    int step = 0;
    public GameObject[] texte;
    public GameObject leftArrow;
    public GameObject rightArrow;

    void Start()
    {
        for(int i=1;i<texte.Length;i++)
        {
            texte[i].SetActive(false);
        }
    }

    public void Menu()
    {
        Application.LoadLevel("meniu");
    }

    public void RightArrow()
    {
        leftArrow.SetActive(true);
        step++;
        if (step == (texte.Length - 1))
            rightArrow.SetActive(false);
        if (step > (texte.Length - 1))

            step = texte.Length - 1;
        texte[step].SetActive(true);
    }

    public void LeftArrow()
    {
        texte[step].SetActive(false);
        rightArrow.SetActive(true);
        step--;
        if(step==0)
        {
            leftArrow.SetActive(false);
        }
        if (step < 0)
            step = 0;
        texte[step].SetActive(true);
    }
}
