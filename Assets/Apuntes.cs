using UnityEngine;


public class Apuntes : MonoBehaviour
{

    public int vida { get; private set; } // 100 





    public void BajarVida(int vidaABajar) // 50
    {
        vida -= vidaABajar; // 100 - 50 = 50
    }




}
