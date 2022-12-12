using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healAmount;
    public bool isFullHeal;

  
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);

            if (isFullHeal)
            {
                HealthManager.instance.ResetHealth();

            }
            else
            {
                HealthManager.instance.AddHeath(healAmount);
            }
        }
    }

}
