using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class TweetButtonController : MonoBehaviour
{
    // Update is called once per frame
    public void OnClick()
    {
        var scoreFactory = FindObjectOfType<ScoreFactory>();
        var scores = scoreFactory.GetScores();
        var score = scores.Count != 0 ? scores[0] : 0; 
        //urlの作成
        naichilab.UnityRoomTweet.Tweet(
            "sheepincrease",
            $"めぇちゃんを必死に守り抜いて {score} 点獲得しました!",
            "unity1week", "unityroom"
            );
    }
}