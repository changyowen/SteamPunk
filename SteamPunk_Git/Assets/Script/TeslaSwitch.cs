﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaSwitch : MonoBehaviour
{
    bool enableSwitch = false;
    public bool switchOn = false;
    public float AutoSwitchOff = 20f;
    private float nextTimeToSwitch = 0f;
    Animator switchAnimator;

    Color RedColor = Color.red;
    Color BlueColor = new Color(0f, 0.188f, 1f, 1f);
    Color DullRed = new Color(.4f, 0f, 0f, 1f);
    Color BrightBlue = new Color(0f, 1f, 1f, 1f);
    Color DullBlue = new Color(0f, .5f, 1f, 1f);

    public GameObject TeslaTowerMainBody;
    public GameObject MainSphere, switchSphere;
    public GameObject ElectricSphere;
    public GameObject[] SmallSphere;
    public Light mainLight, switchLight;
    public GameObject mainTowerIcon, switchIcon;

    public AudioSource switchSound;
    public AudioSource ShootingPointAudioSource;
    AudioClip SwitchSoundOn, SwitchSoundOff, ElectricSphereSound;

    public GameObject SwitchIntruction;

    

    // Start is called before the first frame update
    void Start()
    {
        switchAnimator = GetComponent<Animator>();

        SwitchSoundOn = Resources.Load<AudioClip>("SwitchSoundOn");
        SwitchSoundOff = Resources.Load<AudioClip>("SwitchSoundOff");
        ElectricSphereSound = Resources.Load<AudioClip>("ElectricSphere");

        MainSphere.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        switchSphere.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        for(int i = 0; i < 4; i++)
        {
            SmallSphere[i].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }

        ElectricSphere.SetActive(false);

        SwitchIntruction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextTimeToSwitch)
        {
            if(switchOn == true)
            {
                TurnSwitchOff();
            }
            
            if(enableSwitch == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    nextTimeToSwitch = Time.time + AutoSwitchOff;
                    switchAnimator.SetBool("SwitchOn", true);
                    StartCoroutine(TurnSwitchOn());
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Time.time >= nextTimeToSwitch)
            {
                SwitchIntruction.SetActive(true);
            }
            enableSwitch = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SwitchIntruction.SetActive(false);
            enableSwitch = false;
        }
    }

    IEnumerator TurnSwitchOn()
    {
        switchSound.clip = SwitchSoundOn;
        switchSound.Play();
        ShootingPointAudioSource.PlayOneShot(ElectricSphereSound);
        yield return new WaitForSeconds(.9f);
        switchOn = true;
        ElectricSphere.SetActive(true);

        mainLight.intensity = 7;
        switchLight.intensity = 5;

        if (TeslaTowerMainBody.name.Contains("Tesla_Tower_A"))
        {
            MainSphere.GetComponent<Renderer>().material.SetColor("_EmissionColor", RedColor * 10f);
            switchSphere.GetComponent<Renderer>().material.SetColor("_EmissionColor", RedColor * 10f);
            for(int i = 1; i < 4; i++)
            {
                SmallSphere[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", RedColor * 10f);
            }
            mainTowerIcon.GetComponent<SpriteRenderer>().color = RedColor;
            switchIcon.GetComponent<SpriteRenderer>().color = RedColor;
        }
        else if(TeslaTowerMainBody.name.Contains("Tesla_Tower_B"))
        {
            MainSphere.GetComponent<Renderer>().material.SetColor("_EmissionColor", BlueColor * 10f);
            switchSphere.GetComponent<Renderer>().material.SetColor("_EmissionColor", BlueColor * 10f);
            for (int i = 1; i < 4; i++)
            {
                SmallSphere[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", BlueColor * 10f);
            }
            mainTowerIcon.GetComponent<SpriteRenderer>().color = BrightBlue;
            switchIcon.GetComponent<SpriteRenderer>().color = BrightBlue;
        }
    }

    void TurnSwitchOff()
    {
        switchAnimator.SetBool("SwitchOn", false);
        switchSound.clip = SwitchSoundOff;
        switchSound.Play();
        switchOn = false;
        ElectricSphere.SetActive(false);

        mainLight.intensity = 0;
        switchLight.intensity = 0;

        if (TeslaTowerMainBody.name.Contains("Tesla_Tower_A"))
        {
            MainSphere.GetComponent<Renderer>().material.SetColor("_EmissionColor", RedColor * .5f);
            switchSphere.GetComponent<Renderer>().material.SetColor("_EmissionColor", RedColor * .5f);
            for (int i = 1; i < 4; i++)
            {
                SmallSphere[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", RedColor * .5f);
            }
            mainTowerIcon.GetComponent<SpriteRenderer>().color = DullRed;
            switchIcon.GetComponent<SpriteRenderer>().color = DullRed;
        }
        else if (TeslaTowerMainBody.name.Contains("Tesla_Tower_B"))
        {
            MainSphere.GetComponent<Renderer>().material.SetColor("_EmissionColor", BlueColor * 3f);
            switchSphere.GetComponent<Renderer>().material.SetColor("_EmissionColor", BlueColor * 3f);
            for (int i = 1; i < 4; i++)
            {
                SmallSphere[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", BlueColor * 3f);
            }
            mainTowerIcon.GetComponent<SpriteRenderer>().color = DullBlue;
            switchIcon.GetComponent<SpriteRenderer>().color = DullBlue;
        }
    }
}
