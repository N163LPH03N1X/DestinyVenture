using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTips : MonoBehaviour {

    Text tipText;
    float timer = 6f;
    bool isLoading;
    bool tipChanged;
	// Use this for initialization
	void Start () {
        tipText = GetComponent<Text>();
        UpdateTips();
    }
	
	// Update is called once per frame
	void Update () {
        isLoading = SceneLoader.isLoading;
        if (isLoading)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, Mathf.PingPong(Time.time, 3));
            }
            else if (timer < 0)
            {
                UpdateTips();
                timer = 6;
            }
        }
        if (Input.GetAxisRaw("SelectVertical") == 1  && !tipChanged || Input.GetAxisRaw("SelectVertical") == -1 && !tipChanged)
        {
            UpdateTips();
            timer = 3;
            if (timer >= 3)
            {
                timer = 3;
            }
            tipChanged = true;
        }
        else if (Input.GetAxisRaw("SelectVertical") == 0)
        {
            tipChanged = false;
        }

	}

    void UpdateTips()
    {
        int Randomize;
        Randomize = Random.Range(1, 11);
        if (Randomize == 1)
        {
            tipText.text = "Tip: At Night, Eye enemies are more difficult. Be aware!";
        }
        else if (Randomize == 2)
        {
            tipText.text = "Tip: Every four levels you gain weapon power ups for your swords.";
        }
        else if (Randomize == 3)
        {
            tipText.text = "Tip: Search areas for hidden chests which contain helpful items.";
        }
        else if (Randomize == 4)
        {
            tipText.text = "Tip: Use money to buy items from the store to aid you in your quest.";
        }
        else if (Randomize == 5)
        {
            tipText.text = "Tip: Burn enemies with the flame swords power ability.";
        }
        else if (Randomize == 6)
        {
            tipText.text = "Tip: Flame and frost breasts can allow you to walk through the hottest or the coldest areas.";
        }
        else if (Randomize == 7)
        {
            tipText.text = "Tip: You can light torches with the flame sword.";
        }
        else if (Randomize == 8)
        {
            tipText.text = "Tip: Electric pedestals can be powered with the Thunder Sword.";
        }
        else if (Randomize == 9)
        {
            tipText.text = "Tip: You can break an enemies shield by deflecting back projectiles with the ice sword";
        }
        else if (Randomize == 10)
        {
            tipText.text = "Tip: There are special hidden Boots on the fated island which can help aid you in temples.";
        }
    }
    //public IEnumerator FadeIn(Text text)
    //{
    //    Color alpha;
    //    alpha = text.color;
    //    float MaxAlpha = 1;
    //    for (alpha.a = 0f; alpha.a <= MaxAlpha; alpha.a += 0.15f)
    //    {
    //        text.color = new Color(alpha.r, alpha.g, alpha.b, alpha.a);
    //        yield return new WaitForSeconds(.1f);
    //        if (alpha.a >= 0.9)
    //        {
    //            alpha.a = MaxAlpha;
    //            text.color = new Color(alpha.r, alpha.g, alpha.b, 1);
               
    //        }
    //    }
    //}
    //public IEnumerator FadeOut(Text text)
    //{
    //    Color alpha;
    //    alpha = text.color;
    //    float MinAlpha = 0;
    //    for (alpha.a = 1f; alpha.a >= MinAlpha; alpha.a -= 0.15f)
    //    {
    //        text.color = new Color(alpha.r, alpha.g, alpha.b, alpha.a);
    //        yield return new WaitForSeconds(.1f);
    //        if (alpha.a <= 0.1)
    //        {
    //            alpha.a = MinAlpha;
    //            text.color = new Color(alpha.r, alpha.g, alpha.b, 0);
    //            UpdateTips();
    //            StartCoroutine(FadeIn(tipText));
    //        }
    //    }
    //}
}
