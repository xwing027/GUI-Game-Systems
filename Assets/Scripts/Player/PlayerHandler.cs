using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : Stats
{
    [Header("Damage Flash and Death")]
    public Image damageImage;
    public Image deathImage;
    public Text deathText;
    public AudioClip deathClip;
    public AudioSource playersAudio;
    public Transform currentCheckPoint;

    public Color flashColour = new Color(1, 0, 0, 0.2f);
    public float flashSpeed = 5f;
    public static bool isDead;
    public bool isDamaged;
    public bool canHeal;
    public float healDelayTimer;

    void DeathText()
    {
        deathText.text = "casul";
    }

    void RespawnText()
    {
        deathText.text = "try easy mode this time";
    }

    void Respawn()
    {
        //reset everything
        deathText.text = "";
        
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].currentValue = attributes[i].maxValue;
        }
        
        isDead = false;

        //load pos
        this.transform.position = currentCheckPoint.position;
        this.transform.rotation = currentCheckPoint.rotation;

        //respawn
        deathImage.GetComponent<Animator>().SetTrigger("Respawn");
    }
    
    void Death()
    {
        isDead = true;
        deathText.text = "";

        playersAudio.clip = deathClip;
        playersAudio.Play();

        deathImage.GetComponent<Animator>().SetTrigger("isDead");

        Invoke("DeathText", 2f);
        Invoke("RespawnText", 6f);
        Invoke("Respawn", 9f);
    }

    public void RegenOverTime(int valueIndex)
    {
        attributes[valueIndex].currentValue += Time.deltaTime * (attributes[valueIndex].regenValue /*you can also + a multipler of a stat eg consitution or dex*/);
    }

    public void DamagePlayer(float damage)
    {
        isDamaged = true;
        
        //take damage
        attributes[0].currentValue -= damage;
        
        //delay regen
        canHeal = false;
        healDelayTimer = 0;
        if (attributes[0].currentValue <= 0 && !isDead)
        {
            Death();
        }
    }

    private void Update()
    {
        //debug
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.X))
        {
            DamagePlayer(10);
        }
#endif

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].displayImage.fillAmount = Mathf.Clamp01(attributes[i].currentValue/attributes[i].maxValue);
        }

        #region Damage Flash
        if (isDamaged&&!isDead)
        {
            damageImage.color = flashColour;
            isDamaged = false;
        }
        else if(damageImage.color.a >0)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear,flashSpeed*Time.deltaTime);
        }
        #endregion

        #region Can Heal
        if (!canHeal)
        {
            //if we can't heal, start counting up
            healDelayTimer += Time.deltaTime;
            if (healDelayTimer >= 5)
            {
                canHeal = true;
            }
        }
        if (canHeal && attributes[0].currentValue < attributes[0].maxValue && attributes[0].currentValue>0)
        {
            RegenOverTime(0);
        }
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            currentCheckPoint = other.transform;
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue += 7;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue -= 7;
            }
        }
    }
}
