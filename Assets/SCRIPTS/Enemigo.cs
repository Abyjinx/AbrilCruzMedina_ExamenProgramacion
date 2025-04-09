using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour, IDamageable
{
    [SerializeField] private float rangoDeteccion = 15.0f;
    [SerializeField] private float distanciaParaExplotar = 2.0f;
    [SerializeField] private float radioExplosion = 5.0f;
    [SerializeField] private GameObject prefabEfectoExplosion;
    [SerializeField] private float duracionEfectoExplosion = 2f;
    [SerializeField] private int vidaInicial = 50; 

    private NavMeshAgent navAgent;
    private Transform objetivoJugador;
    private bool haExplotado = false;
    public int vidaActual;

    public bool enemigoFijo = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        vidaActual = vidaInicial;

        GameObject jugadorObj = GameObject.FindGameObjectWithTag("Player");
        if (jugadorObj != null)
        {
            objetivoJugador = jugadorObj.transform;
        }
    }

    void Update()
    {
        if (vidaActual <= 0 && !haExplotado)
        {
            Destroy(gameObject); 
            return;
        }

        if (enemigoFijo)
        {
            navAgent.isStopped = true;
        }

        if (objetivoJugador == null || haExplotado)
        {
            if (navAgent != null && navAgent.isOnNavMesh && !navAgent.isStopped)
            {
                navAgent.isStopped = true;
            }
            return;
        }

        
        if (navAgent == null || !navAgent.isOnNavMesh)
        {
            return;
        }


        float distanciaActual = Vector3.Distance(transform.position, objetivoJugador.position);

        if (distanciaActual <= rangoDeteccion)
        {
            if (distanciaActual <= distanciaParaExplotar)
            {
                Explotar();
            }
            else
            {
                navAgent.isStopped = false;
                navAgent.SetDestination(objetivoJugador.position);
            }
        }
        else
        {
            if (!navAgent.isStopped)
            {
                navAgent.isStopped = true;
            }
        }
    }

    void Explotar()
    {
        if (haExplotado) return;
        haExplotado = true;
        if (navAgent != null && navAgent.isOnNavMesh) navAgent.isStopped = true;

        if (prefabEfectoExplosion != null)
        {
            GameObject efecto = Instantiate(prefabEfectoExplosion, transform.position, Quaternion.identity);
            Destroy(efecto, duracionEfectoExplosion);
        }

        Collider[] collidersAfectados = Physics.OverlapSphere(transform.position, radioExplosion);

        foreach (Collider hitCollider in collidersAfectados)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Health salud = hitCollider.GetComponent<Health>();
                if (salud != null)
                {
                    salud.GetDamage(10);
                }
            }

        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaParaExplotar);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioExplosion);
    }

    public void GetDamage(int damage)
    {
        vidaActual -= damage;
    }
}