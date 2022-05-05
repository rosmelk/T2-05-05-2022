using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PuntajeController : MonoBehaviour
{
    
    public Text PuntajeText;//para el puntaje
    private int puntaje;
  
    public void IncremetoPuntaje(int puntos)
    {
        puntaje += puntos;
    }
    
    void Update()
    {
        //PuntajeText.text = "Puntaje:" + puntaje;
        //if (puntaje>=20)
        //{
           // SceneManager.LoadScene(1);
        //}
        
    }
}
