using System;
using System.Collections;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class TellDialog : MonoBehaviour
{
    private Text dialogWind;
    
    [SerializeField] private float timeBetChars,timeBetPhrazes;
    IEnumerator dialogCoroutine(Dialog dialog,GameObject player)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < dialog.phraze.Count; i++)
        {
            dialogWind.text = dialog.speaker[i]+ " : ";
            for (int u = 0; u < dialog.phraze[i].Length; u++)
            {
                dialogWind.text += dialog.phraze[i][u];
                yield return new WaitForSeconds(timeBetChars);
            }

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
