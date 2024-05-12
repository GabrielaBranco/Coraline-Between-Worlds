using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Faction _faction;
    [SerializeField] private float invulnerabilityDuration;
    
    public delegate void OnVulnerabilityToggle(bool active);
    public event OnVulnerabilityToggle onInvulnerabilityToggle;
    int health;
    public event OnDeath onDeath;
    public delegate void OnDeath();
    public Faction faction => _faction;
    private float invulnerabilityTimer;

    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if(invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if(invulnerabilityTimer < 0)
            {
                if(onInvulnerabilityToggle != null) onInvulnerabilityToggle(false);
            }
        }
    }

    public bool DealDamage(int damage)
    {
        if (health <= 0) return false;
        if(invulnerabilityTimer > 0) return false;
        health -= damage;

        if( health <= 0)
        {
            onDeath?.Invoke();
        }
        else
        {
            invulnerabilityTimer = invulnerabilityDuration;
            if(onInvulnerabilityToggle != null) onInvulnerabilityToggle(true);
        }
        return true;
    }

    public int GetHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
