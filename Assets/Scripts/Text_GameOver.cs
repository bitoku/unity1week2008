using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_GameOver : MonoBehaviour
{
    private ScoreFactory scoreFactory;

    // Start is called before the first frame update
    void Awake()
    {
        int ScoreMessagesCount = scoreFactory.ScoreMessagesCount();
        if(ScoreMessagesCount % 2 == 1)
        {
            this.gameObject.transform.position = new Vector3(Screen.width / 2, 72 * (ScoreMessagesCount + 1) /2 + Screen.height / 2, 0);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(Screen.width / 2, 72 * (ScoreMessagesCount + 2) / 2 - 36 + Screen.height / 2, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
