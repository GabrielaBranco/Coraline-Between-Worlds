using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private Faction _faction;
    [SerializeField] private bool destroyOnDamage = false;
    public Faction faction => _faction;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        HealthSystem hs = collider.GetComponent<HealthSystem>();
        if (hs == null) hs = collider.GetComponentInParent<HealthSystem>();

        if(hs != null)
        {
            if (faction.IsHostile(hs.faction))
            {
                if (hs.DealDamage(damage))
                {
                    Debug.Log($"Collided with health system object - {collider.name}");
                    if(destroyOnDamage)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
