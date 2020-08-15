using System.Collections;
using System.Collections.Generic;
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
        var escText = UnityWebRequest.EscapeURL(
            $"めぇちゃんを必死に守り抜いて {score} 点獲得しました！\n" +
            "https://unityroom.com/games/straysheep"
            );
        var escTag = UnityWebRequest.EscapeURL("unity1week");
        var url = $"https://twitter.com/intent/tweet?text={escText}&hashtags={escTag}";

        //Twitter投稿画面の起動
        Application.OpenURL(url);
    }
}