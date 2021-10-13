using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientHealth : Attributes
{
    public Gradient gradient;
    public Canvas enemyHealthDisplay;
    Transform cam;

    public void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        SetHealth();
        //face the bar to the player's camera
        enemyHealthDisplay.transform.LookAt(enemyHealthDisplay.transform.position + cam.forward);
    }

    public void SetHealth()
    {
        //works out how much health there is
        attributes[0].displayImage.fillAmount = Mathf.Clamp01(attributes[0].currentValue / attributes[0].maxValue);

        //displays it
        attributes[0].displayImage.color = gradient.Evaluate(attributes[0].displayImage.fillAmount);
    }
}
