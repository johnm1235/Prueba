using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canasta : MonoBehaviour
{
    [SerializeField]
    private GameObject canasta;
    private float horizantalInput;
    private float velocidad = 10f;
    private float limites = 10f;

    public float Velocidad
    {
        get { return velocidad; }
        set { velocidad = value; }
    }

    public float Limites
    {
        get { return limites; }
        set{ limites = Mathf.Abs(value);}
    }

    void Update()
    {
        Move();
        AplicarLimites();
    }
    protected  void Move()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal") * velocidad * Time.deltaTime;
        transform.Translate(movimientoHorizontal, 0, 0);
    }


    //Limites para la canasta
    protected  void AplicarLimites()
    {
        if (transform.position.x > limites)
        {
            transform.position = new Vector3(limites, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -limites)
        {
            transform.position = new Vector3(-limites, transform.position.y, transform.position.z);
        }
    }
}
