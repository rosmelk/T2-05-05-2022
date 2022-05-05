using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;



public class CatPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float fuerzaSalto = 10;
    public float velocidad = 10;
    public AudioClip SaltoClip;//audio
    public AudioClip SonidoFinal;
    public GameObject balaPrefab;
    private PuntajeController puntajeControl;
    
    private Rigidbody2D _rb;
    private Animator _anima_personaje;
    private SpriteRenderer _renderer;
    private AudioSource _audioSource; //audio
    private GameObject camaraGo;
    
    
    private bool puedeEscalar = false;
    //constantes
    private const string _EstadoAnimacion ="Estado";
    private const int _quieto = 0;
    private const int _correr = 1;
    private const int _saltar = 2;
    private const int _deslizar = 3;
    
    //
    private const int derecha = 1;
    private const int izquierda = -1;
    private const int arriba = 1;

    

  

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anima_personaje = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        camaraGo = GameObject.Find("Main Camera");
        


    }
    // Update is called once per frame
    void Update()
    {
        
        
         AnimacionPersonaje(_quieto);
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        //Derecha
        if (Input.GetKey(KeyCode.RightArrow))
        {
             Desplazamiento(derecha);
        }
        //izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Desplazamiento(izquierda);
        }
        //disparar 
        if (Input.GetKeyUp(KeyCode.X))
        {
            Disparar(); 
        }
        
        //escalar
        if (Input.GetKey(KeyCode.UpArrow)&& puedeEscalar)
        {
          DesplazarseVertical(arriba);  
        }
        
        //Deslizar
        if (Input.GetKey(KeyCode.C))
        {
            Deslizarse();
        }
          //saltar
        if (Input.GetKeyUp(KeyCode.Space))//Cuando suelto la tecla
        {
            _audioSource.PlayOneShot(SaltoClip);
            _rb.AddForce(Vector2.up * fuerzaSalto , ForceMode2D.Impulse);
            AnimacionPersonaje(_saltar);
        }
        
    }

    //colisiones


    private void OnTriggerEnter2D(Collider2D col)
    {
        var tag = col.gameObject.tag;
        if (tag=="Obstaculo")
        {
            var audioSource = camaraGo.GetComponent<AudioSource>();
            audioSource.clip = SonidoFinal;
            audioSource.Play();
          
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        var tag = col.gameObject.tag;
        if (tag=="Obstaculo")
        {
            Debug.Log("Colisionado"+tag);
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Escalable")
        {
            puedeEscalar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Escalable")
        {
            puedeEscalar = false;
        }
    }

    private void Disparar()
    {
        var x = transform.position.x;
        var y = transform.position.y-2;
        
        var bulletGo =Instantiate(balaPrefab, new Vector2(x,y), quaternion.identity);
        var controller = bulletGo.GetComponent<BalaController>();//para acceder al scrip de Bala Controller
        if (_renderer.flipX){
            
            controller.Velocidad2 = controller.Velocidad2 * -1;
        }
        
    }



    private void Deslizarse()
    {
       AnimacionPersonaje(_deslizar);
        
    }

    //metodo para desplazarse
    private void Desplazamiento(int position)
    {
        _rb.velocity = new Vector2(velocidad * position, _rb.velocity.y);
        _renderer.flipX = position == izquierda;
        AnimacionPersonaje(_correr);
    }

    //metodo para la animacion
    private void AnimacionPersonaje(int animation)
    {
        _anima_personaje.SetInteger(_EstadoAnimacion, animation);
    }
    
    //meotod para desplazar hacia arriba

    private void DesplazarseVertical(int position)
    {
        _rb.velocity = new Vector2(_rb.velocity.x, velocidad * position);
    }
}
