using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObjetos : Canasta
{
    //instancia objetos 
    public GameObject[] objetos;
    public float tiempoMin = 1.0f;
    public float tiempoMax = 2.0f;
    private float tiempoSpawn;
    public float limiteSuperior = 20f;
    public float limiteInferior = -10.0f;
    public float limiteIzquierdo = -10.0f;
    public float limiteDerecho = 10.0f;

    void Start()
    {
        StartCoroutine(SpawnObjetos());
        // Inicia la generación de objetos

    }

    IEnumerator SpawnObjetos()
    {
        while (true)
        {
            // Calcula el tiempo de espera para el próximo objeto
            tiempoSpawn = Random.Range(tiempoMin, tiempoMax);
            yield return new WaitForSeconds(tiempoSpawn);

            // Selecciona un objeto aleatorio para instanciar
            int index = Random.Range(0, objetos.Length);
            GameObject objetoSeleccionado = objetos[index];

            // Genera una posición aleatoria en la parte superior de la pantalla
            Vector3 posicionSpawn = new Vector3(Random.Range(limiteIzquierdo, limiteDerecho), transform.position.y, 0);

            // Instancia el objeto
            GameObject objetoInstanciado = Instantiate(objetoSeleccionado, posicionSpawn, Quaternion.identity);

            // Inicia una corutina para verificar y destruir el objeto si sale de los límites
            StartCoroutine(DestruirSiSaleDeLimites(objetoInstanciado));
        }
    }

    IEnumerator DestruirSiSaleDeLimites(GameObject objeto)
    {
        // Mantiene la corutina activa mientras el objeto exista
        while (objeto != null)
        {
            // Verifica si el objeto ha salido de los límites
            if (objeto.transform.position.y > limiteSuperior || objeto.transform.position.y < limiteInferior ||
                objeto.transform.position.x > limiteDerecho || objeto.transform.position.x < limiteIzquierdo)
            {
                Destroy(objeto); // Destruye el objeto
            }
            yield return null; // Espera un frame antes de continuar la verificación
        }
    }







}
