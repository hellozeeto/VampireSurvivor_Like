using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //  Ãß°¡


public class healthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHelath = 100f;
    public float health;
   
    // Start is called before the first frame update
    void Start()
    {
        health = maxHelath;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10);
        }

        
    }

    void takeDamage(float damage)
    {
        health -= damage;
    }
}
