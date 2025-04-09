using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{

    [SerializeField] private TMP_Text vidaTxt;
    [SerializeField] private GameObject spawn;
    public int health;
    
    int maxHealth = 100;

    private void Start()
    {
        health = maxHealth;
    }

    public Apuntes apuntes;

    public void GetDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            transform.position = spawn.transform.position;
            health = maxHealth;
        }

    }
    private void Update()
    {
        vidaTxt.text = "" + health;
    }

}
