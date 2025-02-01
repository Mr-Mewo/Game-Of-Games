using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; //permite mudar o buttao
using UnityEditor.Experimental.GraphView; 
public class Moneys : MonoBehaviour
{
    public TMP_Text dinheirosText;
    private int dinheiros;

    public GameObject acessorios;
    private int[] precos;
    private bool[] equipado; //mostra se ja compramos o item
    public Button[] buttons; //mostra os buttos que estao ligados



    void Start()
    {
        precos = new int[9];
        precos[0] = 10;
        precos[1] = 20;
        precos[2] = 30;
        precos[3] = 40;
        precos[4] = 50;
        precos[5] = 60;
        precos[6] = 70;
        precos[7] = 80;
        precos[8] = 110;


        Cursor.lockState = CursorLockMode.None;


        equipado = new bool[9];
        
    }

    void Update()
    {

        for (int i = 0; i < precos.Length; i++)
        {
            if (dinheiros >= precos[i] && !equipado[i])
            {

                buttons[i].interactable = true;


            }
            else
            {

                buttons[i].interactable = false;

            }
        }
     }




        public void AddMoney()
    {
        dinheiros++;
        dinheirosText.text = "Dinheiros: " + dinheiros;


    }


    public void Comprar(int num)
    {
        dinheiros -= precos[num];
        acessorios.transform.GetChild(num).gameObject.SetActive(true);
        equipado[num] = true;
        dinheirosText.text = "Dinheiros: " + dinheiros;
            

    }
        


}
