using UnityEngine;
using UnityEngine.UI;

public class ComputerMenu : MonoBehaviour
{
    [SerializeField] private float startY, offset;
    [SerializeField] private RectTransform rect;
    private int currentChoise;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)&& currentChoise<3){currentChoise ++;UpdateChoise();}
        if (Input.GetKeyDown(KeyCode.UpArrow)&& currentChoise>0){currentChoise --;UpdateChoise();}
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.KeypadEnter))UseOption();
    }

    void UpdateChoise()
    {
                // ++ sound
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x,startY-offset*currentChoise);
    }

    void UseOption()
    {
        switch (currentChoise)
        {
            case 0:
                //enter the game
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<PlayerMove>().enabled = true;
                player.GetComponent<Rigidbody>().useGravity = true;
                // someone speacks to you + cutscene
                break;
            case 1:
                //settings
                break;
            case 2:
                // nuh uh sound
                break;
            case 3:
                Application.Quit();
                break;
        }
    }
}
