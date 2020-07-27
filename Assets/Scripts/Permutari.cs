using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Permutari : MonoBehaviour
{
    public GameObject Casuta;
    public GameObject debText;
    public GameObject textStiva;
    public GameObject rama_cod;
    public GameObject buton_oprire_simulare;
    public GameObject canv_setari;
    public Text ktext;
    public Image Img_curenta;
    public Text Txt_curent;
    public GameObject[] cifre_din_vector;
    public string nume_scena;
 

    [System.Serializable]
    public struct regizare
    {
        public GameObject texte;
        public int step;

    }
    public regizare[] reg;
    public Text[] texte = new Text[5];
    
    public GameObject vector;
    public Transform inceputStiva;
    public bool canPressArrows = true;
    public Text debug_step;
    public Slider viteza;
    public bool flag = false;
    public int poz_in_stiva = 0;
    public int step = 0;
    private float time_to_fade = 0.3f;
    private float current_time_to_fade;
    public Transform arrow_poz_initial;
    private int n;
    public char[] numbers;
    private int text_number = 0;
    public int[] stack = new int[10];
    public bool pune_numere;
    public float time_between_numbers = 5f;
    public float current_time_between_numbers;
    private bool has_to_press_arrow = false;
    private bool has_pressed_arrow = false;
    public bool has_pressed_little_arrow = false;
    private bool simulare_oprita = false;
    public GameObject val_x;
    public bool needs_x_input;
    public GameObject sageata_dreapta;
    public GameObject sageata_stanga;
    public Text valx;
    public int elX;
    public Color32 culoare;

    public float timp_de_marire;
    public float timp_ramas_de_marire;
    private bool marire_flag;
    public Button setari_viteza;
    public Button buton_cod;
    public bool slider_mare = false;
    private Vector3 pozitie_initiala;

    public GameObject bara_neagra;
    public bool cod_bara_lungire_flag;
    public bool bara_e_mare;
    public float timp_de_marire_cod;
    public float timp_ramas_de_marire_cod;

    /// <summary>
    /// butoanele ajung de pe -47 pe -350 deci se misca 303 pixeli
    /// </summary>

    public GameObject buton_meniu;
    public GameObject buton_pentru_cod;
    public GameObject buton_pentru_setari;
    public GameObject text;

    public Vector3 poz_initiala_buton_meniu;
    public Vector3 poz_initiala_buton_pentru_cod;
    public Vector3 poz_initiala_buton_pentru_setari;
    public Vector3 poz_initiala_text;

    public bool bara_trebuie_sa_mai;
    private float coef1, coef2;

    public GameObject[] imagini_highlight_text;
    public Text x_nu_ebun;
    public Text DimensiuenaStivei;
   


    void Start()
    {
        int i;
        x_nu_ebun.text = "";
        sageata_stanga.SetActive(false);
        // poz_initiala_buton_meniu = buton_meniu.transform.position;
        // poz_initiala_buton_pentru_cod = buton_cod.transform.position;
        // poz_initiala_buton_pentru_setari = buton_pentru_setari.transform.position;
        for (i=0;i<imagini_highlight_text.Length;i++)
        {
            imagini_highlight_text[i].SetActive(false);
        }
        buton_pentru_setari.SetActive(false);
        buton_pentru_cod.SetActive(false);
        bara_neagra.SetActive(false);
        elX = 5;
        nume_scena = SceneManager.GetActiveScene().name;
        if (nume_scena == "Submultimi")
        {
            elX = 1;
            DimensiuenaStivei.text = "Dimensiunea stivei: " +  elX.ToString();
        }
        needs_x_input = false;
        culoare = new Color32(245, 158, 31, 255);
        if (nume_scena == "Aranjamente" || nume_scena == "Combinari")
        {
            needs_x_input = true;   

        }
        val_x.SetActive(false);
       
        viteza.value = 50f;
        marire_flag = false;
       // Debug.Log(time_between_numbers);
       
        vector.SetActive(false);
        buton_oprire_simulare.SetActive(false);
        debText.SetActive(false);
        textStiva.SetActive(false);
        rama_cod.SetActive(false);
        for(i=0;i<cifre_din_vector.Length;i++)
        {
            cifre_din_vector[i].SetActive(false);
        }
        for (i = 0; i < reg.Length; i++)
            reg[i].texte.SetActive(false);
        current_time_to_fade = time_to_fade;
        current_time_between_numbers = time_between_numbers;
        timp_ramas_de_marire = timp_de_marire;
        timp_ramas_de_marire_cod = timp_de_marire_cod;
        step = 0;
        imagini_highlight_text[0].SetActive(true);
    }

    
    void Update()
    {
        if (marire_flag == true)
        {
            setari_viteza.interactable= false;
            viteza.interactable = false;
            buton_cod.interactable = false;
            if (timp_ramas_de_marire > 0f)
            {
                if (slider_mare == false)
                {
                    viteza.transform.localScale = new Vector3((timp_de_marire - timp_ramas_de_marire) / timp_de_marire, viteza.transform.localScale.y, viteza.transform.localScale.z);
                   // viteza.transform.localPosition = new Vector3(pozitie_initiala.x - (212 / timp_de_marire) * (timp_de_marire - timp_ramas_de_marire), viteza.transform.localPosition.y, viteza.transform.localPosition.z);
                }
                else
                {
                    viteza.transform.localScale = new Vector3(timp_ramas_de_marire/timp_de_marire, viteza.transform.localScale.y, viteza.transform.localScale.z);
                   // viteza.transform.localPosition = new Vector3(pozitie_initiala.x + (212 / timp_de_marire) * (timp_de_marire - timp_ramas_de_marire), viteza.transform.localPosition.y, viteza.transform.localPosition.z);
                }
                timp_ramas_de_marire -= Time.deltaTime;
            }
            else
            {
                if(slider_mare==true)
                    viteza.transform.localScale = new Vector3(0,viteza.transform.localScale.y,viteza.transform.localScale.z);
                slider_mare = !slider_mare;
                timp_ramas_de_marire = timp_de_marire;
                marire_flag = false;
                setari_viteza.interactable = true;
                viteza.interactable = true;
                buton_cod.interactable = true;
            }
        }
        if(cod_bara_lungire_flag==true)
        {
            setari_viteza.interactable = false;
            viteza.interactable = false;
            buton_cod.interactable = false;
            if (timp_ramas_de_marire_cod>0f)
            {
                if(bara_e_mare == false)
                {
                    bara_neagra.transform.localScale  = new Vector3( ((timp_de_marire_cod - timp_ramas_de_marire_cod) / timp_de_marire_cod * coef1), bara_neagra.transform.localScale.y, bara_neagra.transform.localScale.z);
                    move_object(buton_pentru_setari, 303, true, timp_de_marire_cod, timp_ramas_de_marire_cod, poz_initiala_buton_pentru_setari);
                    move_object(buton_pentru_cod, 303, true, timp_de_marire_cod, timp_ramas_de_marire_cod, poz_initiala_buton_pentru_cod);
                    move_object(buton_meniu, 303, true, timp_de_marire_cod, timp_ramas_de_marire_cod, poz_initiala_buton_meniu);
                    move_object(viteza.gameObject, 303, true, timp_de_marire_cod, timp_ramas_de_marire_cod, pozitie_initiala);
                    move_object(text, 195, true, timp_de_marire_cod, timp_ramas_de_marire_cod, poz_initiala_text);
                }
                else
                {
                    bara_neagra.transform.localScale = new Vector3(coef1-( ( (timp_de_marire_cod-timp_ramas_de_marire_cod)/timp_de_marire_cod) * coef1), bara_neagra.transform.localScale.y, bara_neagra.transform.localScale.z);
                    if (bara_trebuie_sa_mai == false)
                    {
                        move_object(buton_pentru_setari, 303, false, timp_de_marire_cod, timp_ramas_de_marire_cod, poz_initiala_buton_pentru_setari);
                        move_object(buton_pentru_cod, 303, false, timp_de_marire_cod, timp_ramas_de_marire_cod, poz_initiala_buton_pentru_cod);
                        move_object(buton_meniu, 303, false, timp_de_marire_cod, timp_ramas_de_marire_cod, poz_initiala_buton_meniu);
                        move_object(viteza.gameObject, 303, false, timp_de_marire_cod, timp_ramas_de_marire_cod, pozitie_initiala);
                        move_object(text, 195, false, timp_de_marire_cod, timp_ramas_de_marire_cod, poz_initiala_text);
                    }
                }
                timp_ramas_de_marire_cod -= Time.deltaTime;
            }
            else
            {
                
                    bara_e_mare = !bara_e_mare;
                    if(bara_e_mare==false)
                    {
                        bara_neagra.transform.localScale = new Vector3(0f, bara_neagra.transform.localScale.y, bara_neagra.transform.localScale.z);
                    }
                    cod_bara_lungire_flag = false;
                    timp_ramas_de_marire_cod = timp_de_marire_cod;
                    setari_viteza.interactable = true;
                    viteza.interactable = true;
                    buton_cod.interactable = true;
                //}

                
            }
        }
        if (needs_x_input)
        {
            if (step == 1)
            {
                int y;
                int.TryParse(valx.text, out y);
                if (nume_scena == "Aranjamente")
                {
                    if (y > 2 && y < 6)
                    {
                        sageata_dreapta.SetActive(true);
                        elX = y;
                        x_nu_ebun.text = "";
                    }
                    else
                    {
                        if (valx.text.Length != 0)
                            x_nu_ebun.text = "Alege o valoare potrivita";
                        else
                        {
                            x_nu_ebun.text = "";
                        }
                        sageata_dreapta.SetActive(false);
                    }
                }
                else if (nume_scena == "Combinari")
                {
                    if (y > 2 && y < 5)
                    {
                        sageata_dreapta.SetActive(true);
                        x_nu_ebun.text = "";
                        elX = y;
                    }
                    else
                    {
                        if(valx.text.Length!=0)
                            x_nu_ebun.text = "Alege o valoare potrivita";
                        else
                        {
                            x_nu_ebun.text = "";
                        }
                        sageata_dreapta.SetActive(false);
                    }
                }  
            }
        }
        if (poz_in_stiva < elX)
        {
            if (poz_in_stiva < elX - 1)
            {
                if (flag == true)
                {
                    if (current_time_to_fade > 0f)
                    {
                        float x = (255 - ((255 / time_to_fade) * current_time_to_fade));
                       // Img_curenta.color = new Color(Img_curenta.color.r, Img_curenta.color.g, Img_curenta.color.b, x);
                       // Txt_curent.color = new Color(Txt_curent.color.r, Txt_curent.color.g, Txt_curent.color.b, x);

                        current_time_to_fade -= Time.deltaTime;
                    }
                    else
                    {
                        poz_in_stiva++;
                        current_time_to_fade = time_to_fade;
                        Draw_Level(poz_in_stiva);
                    }
                }
            }
            else
            {
                flag = false;
                canPressArrows = true;
                if(!pune_numere)
                poz_in_stiva = 0;
                current_time_to_fade = time_to_fade;
            }
            if (pune_numere)
            {
                canPressArrows = false;
                if(current_time_between_numbers >0f)
                {
                    ktext.text = "K (nivelul din stiva pe care lucram) : " + (poz_in_stiva+1).ToString();
                    //Debug.Log("pozitie: " + poz_in_stiva.ToString() + " elem: " + stack[poz_in_stiva]);
                    cifre_din_vector[stack[poz_in_stiva]-1].SetActive(false);
                   
                    texte[poz_in_stiva].text = "" + stack[poz_in_stiva];
                    current_time_between_numbers -= Time.deltaTime;
                }
                else
                {
                    if(poz_in_stiva<elX-1)
                   // Debug.Log(arrow.transform.position.y);
                    current_time_between_numbers = time_between_numbers;
                    poz_in_stiva++;
                }
            }
        }
        else
        {
           
            pune_numere = false;
            canPressArrows = true;
            current_time_between_numbers = time_between_numbers;
            poz_in_stiva = 0;
        }
       

    }

    public void move_object(GameObject obiect,float move_distance, bool left,float total_time,float left_time,Vector3 initial_position)
    {
        if (left == true)
            obiect.transform.localPosition = new Vector3(initial_position.x - (move_distance / total_time) * (total_time - left_time), initial_position.y, initial_position.z);
        else
            obiect.transform.localPosition = new Vector3(initial_position.x + (move_distance / total_time) * (total_time - left_time), initial_position.y, initial_position.z);

    }
    public void cod()
    {
        imagini_highlight_text[imagini_highlight_text.Length - 1].SetActive(false);
        cod_bara_lungire_flag = true ;
        bara_neagra.SetActive(true);
        poz_initiala_buton_meniu = buton_meniu.transform.localPosition;
        poz_initiala_buton_pentru_cod = buton_pentru_cod.transform.localPosition;
        poz_initiala_buton_pentru_setari = buton_pentru_setari.transform.localPosition;
        poz_initiala_text = text.transform.localPosition;
        pozitie_initiala = new Vector3(viteza.transform.localPosition.x, viteza.transform.localPosition.y, viteza.transform.localPosition.z);
        bara_trebuie_sa_mai = false;
        coef1 = 3f;
        coef2 = coef1-1;
    }

    public void verificare_pas()
    {
        char[] v = new char[] {' ',' ',' ',' ',' '};
        if(step==0)
        {
            sageata_dreapta.SetActive(true);
        }
        if(step==1)
        {
            sageata_stanga.SetActive(true);

            if (needs_x_input == true)
            {
                val_x.SetActive(true);
                sageata_dreapta.SetActive(false);
            }
        }
        if (step==2)
        {
            if (needs_x_input == true)
                val_x.SetActive(false);
            textStiva.SetActive(true);
            numbers = v;
            Draw_Stack();
        }
        else if(step==3)
        {
            vector.SetActive(true);
            for (int i = 0; i < cifre_din_vector.Length; i++)
            {
                cifre_din_vector[i].SetActive(true);
            }
        }
        else if(step==4)
        {
            buton_pentru_cod.SetActive(true);
        }
        else if(step==5)
        {
            
            for(int i = 1;i<=elX;i++)
            {
                if (nume_scena == "Permutari" || nume_scena == "Aranjamente" || nume_scena == "Combinari" || nume_scena == "Submultimi")
                {
                    texte[i - 1].text = i.ToString();
                    texte[i - 1].color = culoare;
                }
            }
        }
        else if(step==6)
        {
            debText.SetActive(true);
            imagini_highlight_text[imagini_highlight_text.Length - 1].SetActive(false);
            GameObject[] x = GameObject.FindGameObjectsWithTag("Casuta");
            for (int i = 0; i < x.Length; i++)
            {
                x[i].SetActive(true);
            }
            if(nume_scena=="Permutari" || nume_scena=="Aranjamente" || nume_scena == "Combinari" || nume_scena == "Submultimi")
            StartCoroutine(BK_nerecursiv());
            buton_oprire_simulare.SetActive(true);
            buton_pentru_setari.SetActive(true);
        }
    }
    public void setari()
    {
        marire_flag = true;
        pozitie_initiala = new Vector3(viteza.transform.localPosition.x, viteza.transform.localPosition.y, viteza.transform.localPosition.z);
    }
    public void update_speed()
    {

        time_between_numbers = 1 + ((100 - viteza.value) / (100 / 9));
        current_time_between_numbers = time_between_numbers;

    }
    public void Menu()
    {
        Application.LoadLevel("meniu");
    }

    public void oprire_simulare()
    {
        debText.SetActive(false);
        Delete_Stack();
        vector.SetActive(false);
        for(int i=0;i<texte.Length;i++)
        {
            cifre_din_vector[i].SetActive(false);
        }
        simulare_oprita = true;
        pune_numere = false;
        buton_oprire_simulare.SetActive(false);
        textStiva.SetActive(false);
        canPressArrows = true;
        StopAllCoroutines();
    }

    public void Dreapta()
    {
        
        if (canPressArrows )
        {
            step++;
            debug_step.text="Pas: "+step;
            verificare_pas();
                if(step==imagini_highlight_text.Length && (nume_scena == "Permutari" ||nume_scena=="Submultimi" ))
                {
                    imagini_highlight_text[step - 1].SetActive(true);
                    imagini_highlight_text[step - 2].SetActive(false);
                }
                if(step<imagini_highlight_text.Length)
                {
                    if((nume_scena == "Permutari" || nume_scena == "Submultimi") && step > 1)
                        imagini_highlight_text[step-1].SetActive(true);
                    else
                    {
                        imagini_highlight_text[step].SetActive(true);
                    }
                    if (step > 0)
                    {
                        
                        if ((nume_scena == "Permutari" || nume_scena == "Submultimi") && step > 1)
                            imagini_highlight_text[step - 2].SetActive(false);
                        else
                        imagini_highlight_text[step - 1].SetActive(false);
                    }
                }
           
            if (text_number < reg.Length - 1)
                text_number++;
            if(has_to_press_arrow==true)
            {
                has_pressed_arrow = true;
            }
            if ((nume_scena == "Permutari" || nume_scena == "Submultimi") && step==1)
            {
                Dreapta();
            }
        }
    }

    public void Delete_Stack()
    {
        GameObject[] x = GameObject.FindGameObjectsWithTag("Casuta");
        int i;
        for(i=0;i<x.Length;i++)
        {
            x[i].SetActive(false);
        }
    }

    public void Stanga()
    {
        if (canPressArrows)
        {
            step--;
            if (step < 0)
                step = 0;
            if (step < imagini_highlight_text.Length)
                if ((nume_scena == "Permutari" || nume_scena == "Submultimi"))
                {
                    if(step>0)
                    imagini_highlight_text[step - 1].SetActive(true);
                }
                else
                {
                    imagini_highlight_text[step].SetActive(true);
                }
            if(step<imagini_highlight_text.Length)
                if((nume_scena == "Permutari" || nume_scena == "Submultimi"))
                {
                    imagini_highlight_text[step].SetActive(false);
                }
            else
                {
                    if((step +1) < imagini_highlight_text.Length)
                    imagini_highlight_text[step + 1].SetActive(false);
                }
                

            debug_step.text = "Pas: " + step;
            if(step==5)
            {
                debText.SetActive(false);
                buton_oprire_simulare.SetActive(false);
                simulare_oprita = false;
                buton_pentru_setari.SetActive(false);
            }
            else if(step==4)
            {
                empty_stack();
            }
            else if (step==3)
            {
                buton_pentru_cod.SetActive(false);
            }
            else if(step==2)
            {
                vector.SetActive(false);
                for (int i = 0; i < cifre_din_vector.Length; i++)
                {
                    cifre_din_vector[i].SetActive(false);
                }
                sageata_dreapta.SetActive(true);

            }
            else if(step==1)
            {



                if (needs_x_input == true)
                {
                    val_x.SetActive(true);
                    valx.text = " ";

                    sageata_dreapta.SetActive(false);
                }
                Delete_Stack();
                textStiva.SetActive(false);
            }
            else
                if(step == 0)
                {
                    sageata_stanga.SetActive(false);
                    if (needs_x_input == true)
                    {
                        valx.text = " ";
                        val_x.SetActive(false);
                    }
                    sageata_dreapta.SetActive(true);
                }
            
            if (text_number > 0)
                text_number--;
        }
    }

    /*public void Draw_Stack(int levels, int []numbers,int i)
    {
          //  StartCoroutine(Wait(5.0f,i));
            GameObject x=Instantiate(Casuta, inceputStiva);
            x.transform.position = new Vector3(inceputStiva.position.x, inceputStiva.position.y + 70 * (i - 1));
            Text numar;
            numar=x.transform.Find("Text").GetComponent<Text>();
            Img_curenta = x.transform.Find("Imagine").GetComponent<Image>();
            numar.text = numbers[i - 1].ToString();
    }*/
    public void Draw_Stack()
    {
        Draw_Level(0);
        flag = true;
        canPressArrows = false;
    }
    public void Draw_Level(int i)
    {
        GameObject x = Instantiate(Casuta, inceputStiva);
        x.transform.position = new Vector3(inceputStiva.position.x, inceputStiva.position.y + 60* (i - 1));
        Text numar_txt;
        numar_txt = x.transform.Find("Text").GetComponent<Text>();
        texte[i] = numar_txt;
        Img_curenta = x.transform.Find("Imagine").GetComponent<Image>();
        Txt_curent = numar_txt;
        numar_txt.text = numbers[i].ToString();
    }

    private IEnumerator Wait()
    {
        while (true)
        {
            while (has_pressed_arrow == false)
            {
                yield return null;
            }

            //  print("WaitAndPrint " + Time.time);
        }
    }


    //BACKTRACKING
    public void little_arrow()
    {
        has_pressed_little_arrow = true;
    }

    int valid(int k)
    {
        int i;
        for(i=0;i<k;i++)
        {
            if (stack[i] == stack[k])
                return 0;
           
        }
        return 1;
    }

    int solutie(int k)
    {
        if (k == (texte.Length-1))
            return 1;
        return 0;
    }

    void empty_stack()
    {
        for (int i = 0; i < elX; i++)
        {
            texte[i].text = "";
        }
    }
    void schimbare_numere()
    {
        empty_stack();
        pune_numere = true;

    }

    int validare_combinari(int x)
    {
        int i;
        for (i = 0; i < x; i++)

        {
            if (stack[i] > stack[i + 1])
                return 0;
        }
        return 1;
    }

    private void BK(int k)
    {
        int i;
        for(i=1;i<=texte.Length;i++)
        {
            stack[k] = i;
            if (valid(k) == 1)
            {
                if (solutie(k) == 1)
                {
                    has_to_press_arrow = true;
                    
                    schimbare_numere();
              
                   
                    has_to_press_arrow = false;
                    has_pressed_arrow = false;
                    break;
                }
                else
                {
                    BK(k + 1);
                }
            }
        }
    }

    public IEnumerator BK_nerecursiv()
    {
        int k = 0;
        for(int i=0;i<texte.Length;i++)
        {
            stack[i] = 0;
        }
        while(k>-1)
        {
            if(k==elX && !simulare_oprita )
            {
               
                    has_to_press_arrow = true;

                    Debug.Log("okkkk");
                    schimbare_numere();
                    yield return new WaitUntil(() => has_pressed_arrow == true);
                

                    if (!simulare_oprita)
                    {
                        for (int i = 0; i < texte.Length; i++)
                            cifre_din_vector[i].SetActive(true);
                        empty_stack();
                        has_to_press_arrow = false;
                        has_pressed_arrow = false;
                        k--;
                    }
                
            }
            else
                if(stack[k]<texte.Length  )
                {
                     stack[k]++;

                    if (nume_scena == "Permutari" || nume_scena == "Combinari" || nume_scena == "Aranjamente" || nume_scena =="Submultimi")
                    {
                        if (valid(k) == 1)
                        {

                            if (nume_scena != "Combinari" && nume_scena!="Submultimi")
                                k++;
                            else if (validare_combinari(k) == 1)
                                k++;
                        }
                    }
                    has_pressed_little_arrow = false;
                }
                else
                {
                    stack[k] = 0;
                    k--;
                }
        }
        if(elX<4 && nume_scena == "Submultimi")
        {
            elX++;
            DimensiuenaStivei.text = "Dimensiunea stivei: " + elX.ToString();
            Draw_Level(elX - 1);
            StartCoroutine(BK_nerecursiv());
        }
    }


}
