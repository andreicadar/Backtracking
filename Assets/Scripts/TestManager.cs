using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class TestManager : MonoBehaviour
{
    public Text minute_text;
    public Text secunde_text;
    public Text seed_text;
    public Text seed_catalog_text;
    public Text timp_ramas;
    public Text numar;
    public Text test_scor;
    public Text mail_elev;
    public Text prenume;
    public Text nume;
    public GameObject intrebare_finala;
    public GameObject bara_raspunsuri;
    public GameObject invalid_email;
    public GameObject holder_configurare;
    public GameObject holder_intrebare;
    public GameObject detalii_test;
    public GameObject rezultate;
    public GameObject raspuns;
    public GameObject sageata_stanga;
    public GameObject sageata_dreapta;
    public GameObject incheiere_test;
    public Transform pozitie_input_field_raspuns;
    public bool countdown = false;
    public int intrebare_curenta;
    public Color32 portocaliu;
    public Color32 violet;
    public Color32 negru;
    public Image[] imagini_a_raspuns = new Image[10];

    public float timp;
    public int seed = 100;
    public int seed_catalog = 5;
    public int numar_intrebari = 10;

    public string[] intrebari;
    private string email_profesor = "cadarandrei2370@yahoo.com";
    public Text intrebare;
    public GameObject[] raspunsuri_elev = new GameObject[10];
    public string[] raspunsuri;

    [System.Serializable]
    public struct solutie
    {
        public int[] sol;

    }
    public solutie[] solutii;

    public int sol_curent_reala = 0;
    public int sol_curenta;
    private int numarul_de_elemente;
    public int numar_asteptat_de_solutii;
    private bool hasStarted;
    public bool lucreaza;
    private bool shuffled;
    public int[] vec_partial = new int[10];

    string classroom;
    static string rootURL = "educational.excelentasm.ro/Backtracking/";

    string seedURL =      rootURL + "seed.txt";
    string emailURL =     rootURL + "email.txt";
    string classroomURL = rootURL + "clasa.txt";
    string postURL =      rootURL + "Rezultate/results.php";
    
    public void GetSeed()
    {
        StartCoroutine(GetTheSeed());
    }
    public void GetEmail()
    {
        StartCoroutine(GetTheEmail());
    }
    public void GetClassroom()
    {
        StartCoroutine(GetTheClassroom());
    }

    IEnumerator post_results()
    {
        string URL = postURL;
        WWW www = new WWW(URL);
        yield return www;
    }

    IEnumerator GetTheSeed()
    {
        string URL = seedURL;
        WWW www = new WWW(URL);
        yield return www;
        int.TryParse(www.text, out seed);
    }
    IEnumerator GetTheEmail()
    {
        string URL = emailURL;
        WWW www = new WWW(URL);
        yield return www;
        email_profesor = www.text;
    }
    IEnumerator GetTheClassroom()
    {
        string URL = classroomURL;
        WWW www = new WWW(URL);
        yield return www;
        classroom = www.text;
    }




    void Start()
    {
        intrebare_finala.SetActive(false);
        bara_raspunsuri.SetActive(false);
        invalid_email.SetActive(false);
        hasStarted = false;
        lucreaza = false;
        shuffled = false;
        for (int i = 0; i < solutii.Length; i++)
        {
            solutii[i].sol = new int[10];
        }
        sageata_dreapta.SetActive(false);
        sageata_stanga.SetActive(false);
        holder_intrebare.SetActive(false);
        detalii_test.SetActive(false);
        holder_configurare.SetActive(true);
        
        intrebare_curenta = 1;
        numar_intrebari = 10;
        timp = 0;
        GetClassroom();

    }
    void Update()
    {
        if (countdown)
        {
            timp -= Time.deltaTime;
            timp_ramas.text = ((int)timp / 60 + ":");
            if ((int)timp % 60 < 10)
                timp_ramas.text += "0";
            timp_ramas.text += (int)timp % 60;
            if (timp < 0)
            {
                countdown = false;
                final();
            }
        }
        if (hasStarted == true)
        {
            if (intrebare_curenta <= numar_intrebari && shuffled == false)
            {
                if (lucreaza == false)
                    Generare_solutii_backtracking();
                if (sol_curenta == numar_asteptat_de_solutii)
                {
                    if (sol_curent_reala > 5)
                    {
                        lucreaza = false;
                        aflare_raspuns();
                        intrebare_curenta++;
                    }
                    else
                        Generare_solutii_backtracking();
                }
            }
            else
            {
                if (!shuffled)
                {
                    Random.seed = seed_catalog;
                    for (int t = 0; t < intrebari.Length; t++)
                    {
                        string tmp = intrebari[t];
                        int r = Random.Range(t, intrebari.Length);
                        intrebari[t] = intrebari[r];
                        intrebari[r] = tmp;

                        tmp = raspunsuri[t];
                        raspunsuri[t] = raspunsuri[r];
                        raspunsuri[r] = tmp;
                    }
                    shuffled = true;
                    intrebare_curenta = 1;
                    intrebare.text = intrebari[intrebare_curenta - 1];
                }


            }
        }
    }

    public void change_seed()
    {
        int.TryParse(seed_text.text, out seed);   
    }

    public void TextEmail_Changed()
    {
        invalid_email.SetActive(false);
    }

    public void Start_Button()
    {
        
        if (seed_text.text.Length == 0)
            GetSeed();
        Debug.Log(seed);
        Debug.Log(classroom);
        Debug.Log(email_profesor);
        int x, y;
        int.TryParse(minute_text.text, out x);
        int.TryParse(secunde_text.text, out y);
        timp = 60 * x + y;
        if (timp < 1f)
            timp = 1f;
        int.TryParse(seed_catalog_text.text, out seed_catalog);

        if (Validare_Email() == true)
        {
            bara_raspunsuri.SetActive(true);
            imagini_a_raspuns[0].color=portocaliu;
            for (int i = 1; i <= 9; i++)
                imagini_a_raspuns[i].color = negru;
            incheiere_test.SetActive(false);
            holder_configurare.SetActive(false);
            Random.seed = seed;
            holder_intrebare.SetActive(true);
            detalii_test.SetActive(true);
            sageata_dreapta.SetActive(true);
            sageata_stanga.SetActive(true);
            for (int i = 1; i <= numar_intrebari; i++)
            {
                GameObject xz = Instantiate(raspuns, raspuns.transform);
                xz.transform.parent = raspuns.transform.parent;
                raspunsuri_elev[i - 1] = xz;
            }
            for (int i = 1; i < raspunsuri_elev.Length; i++)
                raspunsuri_elev[i].SetActive(false);
            raspuns.SetActive(false);
            hasStarted = true;
            countdown = true;
            sageata_stanga.SetActive(false);
        }
        else
        {
            invalid_email.SetActive(true);
        }
    }

    public void Menu_Button()
    {
        Application.LoadLevel("meniu");
    }

    public void Dreapta()
    {
        if(raspunsuri_elev[intrebare_curenta-1].GetComponent<InputField>().text.Length!=0)
        {
            imagini_a_raspuns[intrebare_curenta - 1].color = violet;
        }
        else
        {
            imagini_a_raspuns[intrebare_curenta - 1].color = negru;
        }

        raspunsuri_elev[intrebare_curenta - 1].SetActive(false);
        intrebare_curenta++;
        imagini_a_raspuns[intrebare_curenta - 1].color = portocaliu;
        intrebare.text = intrebari[intrebare_curenta - 1];
        if (intrebare_curenta == numar_intrebari)
        {
            sageata_dreapta.SetActive(false);
            incheiere_test.SetActive(true);
        }
        if (intrebare_curenta >=1)
            sageata_stanga.SetActive(true);
        raspunsuri_elev[intrebare_curenta - 1].SetActive(true);
        numar.text = ("#"+intrebare_curenta.ToString());
    }

    public void Stanga()
    {
        if (raspunsuri_elev[intrebare_curenta - 1].GetComponent<InputField>().text.Length != 0)
        {
            imagini_a_raspuns[intrebare_curenta - 1].color = violet;
        }
        else
        {
            imagini_a_raspuns[intrebare_curenta - 1].color = negru;
        }

        raspunsuri_elev[intrebare_curenta - 1].SetActive(false);
        intrebare_curenta--;
        imagini_a_raspuns[intrebare_curenta - 1].color = portocaliu;
        intrebare.text = intrebari[intrebare_curenta - 1];
        if (intrebare_curenta == 1)
            sageata_stanga.SetActive(false);
        if (intrebare_curenta == numar_intrebari - 1)
        {
            sageata_dreapta.SetActive(true);
            incheiere_test.SetActive(false);
        }
        raspunsuri_elev[intrebare_curenta - 1].SetActive(true);
        numar.text = ("#"+intrebare_curenta.ToString());
    }

    public bool Validare_Email()
    {
        int i = 0;
        bool ok_at = false;
        bool ok_dot = false;

        for(i=0;i<mail_elev.text.Length;i++)
        {
            if (mail_elev.text[i] == '@')
                ok_at = true;
            else if (mail_elev.text[i] == '.')
                ok_dot = true;
        }
        if (ok_dot == true && ok_at == true)
            return true;
        return false;

    }

    void Generare_solutii_backtracking()
    {
        lucreaza = true;
        sol_curenta = 0;
        sol_curent_reala = 0;
        int tipul_intrbarii = Random.Range(1, 3);
        for (int i = 0; i < vec_partial.Length; i++)
            vec_partial[i] = 0;

        if (tipul_intrbarii == 1)  //Se da o multime si 3 numere din sir sa se afiseze urmatoarele (2 sau 3)(permutari sau carezian)
        {
            numarul_de_elemente = Random.Range(3, 7);
            int aranajamente_sau_permutari = Random.Range(1, 3);
           
            intrebari[intrebare_curenta - 1] = "   Se da o multime cu " + numarul_de_elemente + " elemente, M={";
            for (int j = 1; j <= numarul_de_elemente; j++)
            {
                intrebari[intrebare_curenta - 1] += (j.ToString());
                if (j != numarul_de_elemente)
                    intrebari[intrebare_curenta - 1] +=  (" ,");
                else
                    intrebari[intrebare_curenta - 1] += "}.";
            }
            intrebari[intrebare_curenta - 1] += " Folosind aceasta multime s-a generat un sir format cu numere formate din aceste cifre, 3 numere de pe pozitii consecutive din sir sunt: ";

            if (aranajamente_sau_permutari == 1) //cartezian
            {
                numar_asteptat_de_solutii = (int)Mathf.Pow(numarul_de_elemente, numarul_de_elemente);
                BK_cartezian(0, numarul_de_elemente);
            }
            else //Permutari
            {
                numar_asteptat_de_solutii = 1;
                for (int j = 2; j <= numarul_de_elemente; j++)
                {
                    numar_asteptat_de_solutii *= j;
                }
                BK_permutari(0, numarul_de_elemente);
            }
        }
        else if (tipul_intrbarii == 2) //suma cifrelor sau produsul cifrelor
        {
            
            numarul_de_elemente =  Random.Range(3, 6);
            numar_asteptat_de_solutii = (int)Mathf.Pow(10, numarul_de_elemente);
            intrebari[intrebare_curenta - 1] = ("   Utilizând metoda backtracking, se generează numerele naturale formate din " + numarul_de_elemente.ToString());
            int tip_de_intrbare = Random.Range(1, 3);
            if (tip_de_intrbare == 1) //suma si produs
            {
                intrebari[intrebare_curenta - 1] += " cifre care au";
                int suma_produs = Random.Range(1, 3);
                if (suma_produs == 1) //suma
                {
                    intrebari[intrebare_curenta - 1] += " suma cifrelor egal cu ";
                    int rez = Random.Range(numarul_de_elemente * 2, (numarul_de_elemente - 1) * 9);
                    intrebari[intrebare_curenta - 1] += (rez.ToString() + ". ");
                    intrebari[intrebare_curenta - 1] += "Un subsir al sirului ar fi: ";
                    BK_cifre_cu_connditii(0, numarul_de_elemente, 1, rez);
                }
                else //produs
                {
                    intrebari[intrebare_curenta - 1] += " produsul cifrelor egal cu ";
                    int rez = (int)Random.Range(0, Mathf.Pow(9, numarul_de_elemente - 2));
                    while (rez == 1)
                        rez = (int)Random.Range(0, Mathf.Pow(9, numarul_de_elemente - 2));
                    intrebari[intrebare_curenta - 1] += (rez.ToString() + ". ");
                    intrebari[intrebare_curenta - 1] += "Un subsir al sirului ar fi: ";
                    BK_cifre_cu_connditii(0, numarul_de_elemente, 2, rez);
                }
            }
            else // cifre alaturate
            {
                numar_asteptat_de_solutii = (int)Mathf.Pow(10, numarul_de_elemente);
                intrebari[intrebare_curenta - 1] += " cifre, ";
                int min_egal_mare = Random.Range(1, 4);
                if(min_egal_mare == 1)
                {
                    intrebari[intrebare_curenta - 1] += " cel putin o cifra repetandu-se de minim ";
                }
                else if(min_egal_mare == 2)
                {
                    intrebari[intrebare_curenta - 1] += " cel putin o cifra sa se repete exact de ";
                }
                else if(min_egal_mare == 3)
                {
                    intrebari[intrebare_curenta - 1] += " nicio cifra sa nu apara de mai mult de ";
                }
                int cate_cifre;
                if (min_egal_mare==3)
                     cate_cifre = Random.Range(2, 5);
                else
                {
                    cate_cifre = Random.Range(2, 5);
                }
                
                while (cate_cifre > numarul_de_elemente)
                    cate_cifre = Random.Range(2, 5);
                intrebari[intrebare_curenta - 1] += (cate_cifre.ToString() + " ori. ");
                BK_cifre_alaturate(0, numarul_de_elemente, cate_cifre,min_egal_mare);
            }
            
        }
        
    }
    public void aflare_raspuns()
    {

        int crescator_sau_descrescator = Random.Range(1, 3);
        int cate_numere_se_cer = Random.Range(2, 4);
        if (crescator_sau_descrescator == 1 || sol_curent_reala < 6) //crescator
        {
           
            int de_unde_sa_scrie = Random.Range(0, (sol_curent_reala - 6));
            for (int j = de_unde_sa_scrie; j <= de_unde_sa_scrie + 2; j++)
            {
                for (int z = 0; z < numarul_de_elemente; z++)
                {
                    intrebari[intrebare_curenta - 1] += (solutii[j].sol[z].ToString());
                }
                if (j != (de_unde_sa_scrie + 2))
                    intrebari[intrebare_curenta - 1] += ", ";
                else
                    intrebari[intrebare_curenta - 1] += ".";
            }
            for (int j = de_unde_sa_scrie + 3; j < ((de_unde_sa_scrie + 3) + cate_numere_se_cer); j++)
            {
                for (int z = 0; z < numarul_de_elemente; z++)
                {
                    raspunsuri[intrebare_curenta - 1] += (solutii[j].sol[z].ToString());

                }
                if (j != ((de_unde_sa_scrie + 2 + cate_numere_se_cer)))
                    raspunsuri[intrebare_curenta - 1] += " ";
            }
        }
        else //descrescator
        {
            if (crescator_sau_descrescator == 2)
            {
                int de_unde_sa_scrie = Random.Range(5, sol_curent_reala - 1);
                for (int j = de_unde_sa_scrie; j >= de_unde_sa_scrie - 2; j--)
                {
                    
                    for (int z = 0; z < numarul_de_elemente; z++)
                    {

                        intrebari[intrebare_curenta - 1] += (solutii[j].sol[z].ToString());
                    }
                    if (j != (de_unde_sa_scrie - 2))
                        intrebari[intrebare_curenta - 1] += ", ";
                    else
                        intrebari[intrebare_curenta - 1] += ".";
                }
                for (int j = de_unde_sa_scrie - 3; j > ((de_unde_sa_scrie - 3) - cate_numere_se_cer); j--)
                {
                    for (int z = 0; z < numarul_de_elemente; z++)
                    {
                        raspunsuri[intrebare_curenta - 1] += (solutii[j].sol[z].ToString());
                    }
                    if (j != ((de_unde_sa_scrie - 2) - cate_numere_se_cer))
                        raspunsuri[intrebare_curenta - 1] += " ";
                }
            }
        }
        intrebari[intrebare_curenta - 1] += (" Sa se scrie urmatoarele " + cate_numere_se_cer + " numere.");
    }

    //zona backtrackinguri

    void adaugare_solutie(int k)
    {
       
        for (int i=0;i<=k;i++)
        {
            solutii[sol_curent_reala].sol[i] = vec_partial[i];

        }
        sol_curent_reala++;
     

    }
    void verificare_cifre(int sp,int rez,int k)
    {
        int x = 0;
        if (sp == 2)
        {
            x = 1;
            for(int i=0;i<=k;i++)
            {
                x *= vec_partial[i];
            }   
        }
        else
        {
            for (int i = 0; i <= k; i++)
            {
                x += vec_partial[i];
            }
        }
        if (x == rez)
            adaugare_solutie(k);

    }

    public void BK_cifre_cu_connditii(int k,int n,int sp,int rez)
    {
        int i;
        for (i = 0; i <= 9; i++)
        {
            vec_partial[k] = i;
           
            if (k == (n - 1))
                {
                    sol_curenta++;
                if(vec_partial[0]!=0)
                    verificare_cifre(sp, rez, k);

                }
                else
                {
                    BK_cifre_cu_connditii(k + 1, n,sp,rez);
                }
        }
    }

    int verificare_alaturare(int cate,int k,int min_mare_egal)
    {
        int i,x=1;
        int[] z = new int[10];
        for (i = 0; i <= k; i++)
        {
            z[vec_partial[i]]++;
        }
        for (i = 0; i < 10 && min_mare_egal!=3; i++)
        {
            if (min_mare_egal == 1)
            {
                if (z[i] >= cate)
                    return 1;
            }
            else if (min_mare_egal == 2)
            {
                if (z[i] == cate)
                    return 1;
            }

        }
        if (min_mare_egal == 3)
        {
            bool ok = true;
            for (i = 0; i < 10 ; i++)
            {
                if(z[i] > cate)
                {
                    ok = false;
                    return 0;
                }
            }
            if (ok == true)
            {
                return 1;
            }
        }
       

            return 0;
    }

    public void BK_cifre_alaturate(int k,int n,int cate_alat,int min_mare_egal)
    {
        int i;
        for (i = 0; i <= 9; i++)
        {
            vec_partial[k] = i;

            if (k == (n-1))
            {
                sol_curenta++;
                if (vec_partial[0] != 0)
                {
                    int x = verificare_alaturare(cate_alat, k,min_mare_egal);
                    if (x == 1)
                        adaugare_solutie(k);
                }
                    

            }
            else
            {
                BK_cifre_alaturate(k + 1, n, cate_alat,min_mare_egal);
            }
        }
    }

    void BK_cartezian(int k,int n)
    {
        int i;
        for(i = 1; i <= n; i++)
        {
            vec_partial[k] = i;
            if(k==(n-1))
            {
                sol_curenta++;
                adaugare_solutie(k);
            }
            else
            {
                BK_cartezian(k + 1, n);
            }
        }
    }
    int valid(int k)
    {
        int i;
        for(i = 0; i < k; i++)
        {
            if (vec_partial[i] == vec_partial[k])
                return 0; 
        }
        return 1;
    }

    void BK_permutari(int k, int n)
    {
        int i;
        for (i = 1; i <= n; i++)
        {
            vec_partial[k] = i;
            if (valid(k) == 1)
            {
                if (k == (n-1))
                {
                    sol_curenta++;
                    adaugare_solutie(k);
                }
                else
                {
                    BK_permutari(k + 1, n);
                }
            }
            
        }
    }

    public void incercare_final()
    {
            if (raspunsuri_elev[intrebare_curenta - 1].GetComponent<InputField>().text.Length != 0)
            {
                imagini_a_raspuns[intrebare_curenta - 1].color = violet;
            }
            else
            {
                imagini_a_raspuns[intrebare_curenta - 1].color = negru;
            }

        holder_intrebare.SetActive(false);
        detalii_test.SetActive(false);
        intrebare_finala.SetActive(true);
    }

    public void razgandit()
    {
        imagini_a_raspuns[intrebare_curenta - 1].color = portocaliu;
        holder_intrebare.SetActive(true);
        detalii_test.SetActive(true);
        intrebare_finala.SetActive(false);
    }

    public void final()
    {
        intrebare_finala.SetActive(false);
        int corecte = 0;
        holder_intrebare.SetActive(false);
        detalii_test.SetActive(false);
        rezultate.SetActive(true);
        for (int i = 0; i < numar_intrebari; i++)
        {
            if (raspunsuri_elev[i].GetComponent<InputField>().text == raspunsuri[i])
                corecte++;
        }
        test_scor.text = "" + 10 * corecte;
        SendEmailSMTP(corecte * 10, 100);
        postURL += ("?nume=" + prenume.text+nume.text+"&scor="+corecte*10+ "&parola=q.d1b3A2C" + "&clasa="+classroom);
        Debug.Log(postURL);
        StartCoroutine(post_results());
    }
    
    public void SendEmailSMTP(int punctaj, int out_of)
    {
        System.Net.Mail.Attachment attachment;
       // attachment = new System.Net.Mail.Attachment("Assets/Imagini/felicitare.jpg");
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("Evaluare@gmail.com");
        mail.To.Add(mail_elev.text);
        mail.To.Add(email_profesor);
        mail.Subject = "Rezultate evaluare Backtracking";
        mail.IsBodyHtml = true;
        mail.Body = ("Ai obtinut: " + punctaj.ToString() + " din " + out_of.ToString());
        //mail.Attachments.Add(attachment);
        SmtpClient smtpServer = new SmtpClient();
        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpServer.Host = "smtp.gmail.com";
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("BacktrackingEvaluare@gmail.com",  "Permutari_159") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
       
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };

        smtpServer.Send(mail);
    }



    public void SendEmail(int punctaj,int out_of)
    {
        string subject = MyEscapeURL("Evaluare Backtracking");
        string body = MyEscapeURL( ("Ai obtinut: " + punctaj.ToString() + " din " + out_of.ToString()) );

        Application.OpenURL("mailto:" + mail_elev.text + "?subject=" + subject + "&body=" + body);
    }
    string MyEscapeURL(string URL)
    {
        return WWW.EscapeURL(URL).Replace("+", "%20");
    }


}
