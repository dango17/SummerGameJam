using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int maxHealth;
    public int currentHealth;

    public void SetHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, 50);
        currentHealth = maxHealth = amount;
    }

    public IEnumerator DelayDamage(int amount,int delay)
    {
        yield return new WaitForSeconds(delay);
        //never go below 0
        currentHealth = Mathf.Max(currentHealth - amount, 0);
    }

    public IEnumerator Regen(int amount, int delay)
    {
        yield return new WaitForSeconds(delay);
        //never go above maxHealth
        currentHealth = Mathf.Max(currentHealth + amount, maxHealth);
    }
}
