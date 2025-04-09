using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField] private float disparosPorSegundo = 4f;
    [SerializeField] private float rango = 100f;
    [SerializeField] private Transform puntoDeDisparo;
    [SerializeField] private LayerMask capaDeImpacto;

    private float tiempoEntreDisparos;
    private float proximoTiempoDeDisparo = 0f;
    private Camera camaraJugador;

    void Start()
    {
        tiempoEntreDisparos = disparosPorSegundo > 0 ? 1f / disparosPorSegundo : 0.1f;
        camaraJugador = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= proximoTiempoDeDisparo)
        {
            proximoTiempoDeDisparo = Time.time + tiempoEntreDisparos;
            Disparar();
        }
    }

    void Disparar()
    {
        Debug.Log("Pium");
        RaycastHit hit;
        Vector3 origenRayo = puntoDeDisparo.position;
        Vector3 direccionRayo = puntoDeDisparo.forward;

        if (camaraJugador != null)
        {
            origenRayo = camaraJugador.transform.position;
            direccionRayo = camaraJugador.transform.forward;
        }


        if (Physics.Raycast(origenRayo, direccionRayo, out hit, rango, capaDeImpacto))
        {

            Enemigo salud = hit.transform.GetComponent<Enemigo>();
            if (salud != null)
            {
                salud.GetDamage(25);
            }
        }
    }
}
