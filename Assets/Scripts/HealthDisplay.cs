using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private HealthSystem player;
    [SerializeField] private Sprite heart;
    [SerializeField] private Sprite empty;
    [SerializeField] private Image[] hearts;

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.GetHealth())
            {
                hearts[i].sprite = heart;
            }
            else
            {
                hearts[i].sprite = empty;
            }

            if (i < player.GetMaxHealth())
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }    
    }

    public void DestroyHearts()
    {
        foreach(Image img in hearts)
        {
            Destroy(img);
        }
    }
}
