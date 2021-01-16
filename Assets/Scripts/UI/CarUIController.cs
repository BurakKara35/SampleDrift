using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUIController : MonoBehaviour
{
    private string playerName;
    private Sprite flag;
    private string[] feedbacks;
    private Sprite emoji;

    public Text nameText;
    public Image flagImage;
    public Text feedbackText;
    public Image emojiImage;
    public Sprite[] goodEmojiImages;
    public Sprite[] badEmojiImages;

    private void Awake()
    {
        feedbacks = new string[3];

        feedbacks[0] = "WOW";
        feedbacks[1] = "PERFECT";
        feedbacks[2] = "GREAT";

        flagImage.color = new Color(255, 255, 255, 255);
    }

    public string PlayerName
    {
        get{ return playerName; }
        set{ playerName = value; }
    }

    public Sprite Flag
    {
        get { return flag; }
        set { flag = value; }
    }

    public void SetPlayerInformations()
    {
        nameText.text = playerName;
        flagImage.sprite = flag;
    }

    public string Feedbacks
    {
        get { return feedbacks[Random.Range(0, feedbacks.Length)]; }
    }

    public void GiveFeedbackToPlayer()
    {
        feedbackText.text = Feedbacks;
        Invoke("ClearFeedback", 1f);
    }

    void ClearFeedback()
    {
        feedbackText.text = "";
    }

    public void GiveGoodEmojiToPlayer()
    {
        emojiImage.sprite = goodEmojiImages[Random.Range(0,goodEmojiImages.Length)];
        emojiImage.color = new Color(255, 255, 255, 255);
        Invoke("ClearEmoji", 1f);
    }

    public void GiveBadEmojiToPlayer()
    {
        emojiImage.sprite = badEmojiImages[Random.Range(0, badEmojiImages.Length)];
        emojiImage.color = new Color(255, 255, 255, 255);
        Invoke("ClearEmoji", 1f);
    }

    void ClearEmoji()
    {
        emojiImage.color = new Color(255, 255, 255, 0);
        emojiImage.sprite = null;
    }
}
