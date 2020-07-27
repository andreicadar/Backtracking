using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public void prezentare_generala()
    {
        Application.LoadLevel("Prezentare Generala");
    }
    public void permutari()
    {
        Application.LoadLevel("Permutari");
    }

    public void aranjamente()
    {
        Application.LoadLevel("Aranjamente");
    }

    public void combinari()
    {
        Application.LoadLevel("Combinari");
    }

    public void teste()
    {
        Application.LoadLevel("Teste");

    }

    public void submultimi()
    {
        Application.LoadLevel("Submultimi");
    }
    public void Exit()
    {
        Application.Quit();
    }
    
    public void Info()
    {
        Application.LoadLevel("info");
    }
}
