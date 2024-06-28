using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canasta : MonoBehaviour
{
    public GameObject canasta;
    public float horizantalInput;
    public float velocidad = 10f;

    public float limites = 10f;


    void Update()
    {
        Move();
        Limites();
    }
    public virtual void Move()
    {
        // Ejemplo de movimiento horizontal
        float movimientoHorizontal = Input.GetAxis("Horizontal") * velocidad * Time.deltaTime;
        transform.Translate(movimientoHorizontal, 0, 0);
    }

    //lIMITAR EL MOVIMIENNTO DE LA CANASTA

    public virtual void Limites()
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
