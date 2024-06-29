using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObjetos : Canasta
{
    //instancia objetos 
    public GameObject[] objetos;
    public float tiempoMin = 1.0f;
    public float tiempoMax = 1.5f;
    private float tiempoSpawn;
    public float limiteSuperior = 30f;
    public float limiteInferior = -10.0f;
    public float limiteIzquierdo = -10.0f;
    public float limiteDerecho = 10.0f;

    void Start()
    {
        StartCoroutine(SpawnObjetos());
    }

    IEnumerator SpawnObjetos()
    {
        while (true)
        {
            // timpo para el proximo spawn
            tiempoSpawn = Random.Range(tiempoMin, tiempoMax);
            yield return new WaitForSeconds(tiempoSpawn);

            int index = Random.Range(0, objetos.Length);
            GameObject objetoSeleccionado = objetos[index];

            //posicion de spawn usando herencia
            Vector3 posicionSpawn = GenerarPosicionAleatoria(limiteIzquierdo, limiteDerecho, transform.position.y, 0);

            //instanciar objeto
            GameObject objetoInstanciado = Instantiate(objetoSeleccionado, posicionSpawn, Quaternion.identity);

            //corrutina para destruir el objeto
            StartCoroutine(DestruirSiSaleDeLimites(objetoInstanciado));
        }
    }

    IEnumerator DestruirSiSaleDeLimites(GameObject objeto)
    {
        // Mantiene la corutina activa mientras el objeto exista
        while (objeto != null)
        {
            //Si el objeto sale del limite
            if (objeto.transform.position.y > limiteSuperior || objeto.transform.position.y < limiteInferior ||
                objeto.transform.position.x > limiteDerecho || objeto.transform.position.x < limiteIzquierdo)
            {
                Destroy(objeto); //destruye el objeto
            }
            yield return null; 
        }
    }







}
