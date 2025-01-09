using System.Collections;
using UnityEngine;

    public class StoryTelling : MonoBehaviour
    {
        private int storyIndex=0;
        private Vector2 tileScale;
        private Transform plTrans;
        
        [SerializeField] private float minEvDist = 10, evRadius = 10;
        IEnumerator StoryCycle()
        {
            Vector3 currentEve, nextEve = Vector3.forward*3;
            while (true)
            {
                float diff = Vector3.Distance(plTrans.position, nextEve);
                //WaitUntil(() => diff<minEvDist);
                storyIndex++;
                //event may do something
                
                float randDeg = Random.Range(0, 360), randDeg2 = Random.Range(0, 360);
                Vector2 randPoz = new Vector2(Mathf.Cos(randDeg*Mathf.Deg2Rad), Mathf.Sin(randDeg*Mathf.Deg2Rad))*evRadius;
            }
        }
        void Start()
        {
        plTrans = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StoryCycle());
        }
    }


