﻿using System;
using System.Collections;
using System.Transactions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TellDialog : MonoBehaviour
{

    private Text dialogWind;
    
    [SerializeField] private float timeBetChars,timeBetPhrazes;
    IEnumerator dialogCoroutine(Dialog dialog,GameObject player)
    {
        Transform camTr = player.transform.GetChild(0);
        //rot to speaker
        Vector3 direction = transform.position - player.transform.position;
        direction.y = 0;
        Vector3 camDirection = transform.position - camTr.position;
        camDirection.x = 0;
        camDirection.y = 0;

        // Обчислення кінцевої ротації
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion camTargetRotation = Quaternion.LookRotation(camDirection);

        // Плавний перехід до кінцевої ротації
        for (float t = 0; t < 2; t += Time.deltaTime)
        {
            yield return null;
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, 7 * Time.deltaTime);
            camTr.rotation = Quaternion.Lerp(camTr.rotation, camTargetRotation, 3 * Time.deltaTime);
        }



        yield return new WaitForSeconds(1);
        for (int i = 0; i < dialog.phraze.Count; i++)
        {
            dialogWind.text = dialog.audioSources[dialog.indexesOfSpeak[i]].gameObject.name+ " : ";


            Voices voices = dialog.audioSources[dialog.indexesOfSpeak[i]].gameObject.GetComponent<Voices>();

            float randomStartTime =UnityEngine.Random.Range(0f, voices.voicesForToday[0].length);

            dialog.audioSources[dialog.indexesOfSpeak[i]].clip = voices.voicesForToday[0];
            dialog.audioSources[dialog.indexesOfSpeak[i]].time = randomStartTime;


            dialog.audioSources[dialog.indexesOfSpeak[i]].Play();

            for (int u = 0; u < dialog.phraze[i].Length; u++)
            {
                dialogWind.text += dialog.phraze[i][u];
                yield return new WaitForSeconds(timeBetChars);
            }
            dialog.audioSources[dialog.indexesOfSpeak[i]].Stop();

            yield return new WaitForSeconds(timeBetPhrazes);
        }
        //off dialog
        dialogWind.text = "";
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        player.GetComponent<PlayerMove>().enabled = true;
        Destroy(dialog);
    }
    void Start()
    {
        GameObject dialogUI = GameObject.Find("DialogText");
        dialogWind = dialogUI.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {    
        if (other.tag == "Player")
         {
             print("Player");
             Dialog dialog = GetComponent<Dialog>();
             
             StartCoroutine(dialogCoroutine(dialog,other.gameObject));
             
             other.GetComponent<PlayerMove>().enabled = false;
         }
        //throw new NotImplementedException();
    }

}
