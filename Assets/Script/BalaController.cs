using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    public float Velocidad2=50;
    private PuntajeController _puntajeController;//variable para acceder a CatPlayer
    private Rigidbody2D _rb;
    private BoxCollider2D _bColider;
    
    public void setCatcontroller(PuntajeController puntajeController)
    {
       _puntajeController = puntajeController;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        Destroy(this.gameObject, 3);
        
    }
    void Update()
    {
        _rb.velocity = new Vector2(Velocidad2, _rb.velocity.y);
       _puntajeController = (GameObject.Find("PuntajeG")).GetComponent<PuntajeController>(); //llamamos al puntaje

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var tag = col.gameObject.tag;
        if (tag=="Enemy")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
            _puntajeController.IncremetoPuntaje(10);


        }
    }

  
}
