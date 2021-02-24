using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interaction : MonoBehaviour {

    public Image AButtonUI;
    public Image BButtonUI;
    public Image XButtonUI;
    public Image YButtonUI;

    public Text AButtonMsg;
    public Text BButtonMsg;
    public Text XButtonMsg;
    public Text YButtonMsg;

    bool entered;
    // Use this for initialization
    void Start () {
        EnableAUI(false, "");
        EnableBUI(false);
        EnableXUI(false);
        EnableYUI(false);

    }

    public void EnableAUI(bool True, string String)
    {
        if (True)
        {
            entered = SceneLoader.Entered;
            if (!entered)
            {
                AButtonMsg.text = String;
            }
            else if (entered)
            {
                AButtonMsg.text = String;
            }
            AButtonMsg.enabled = true;
            AButtonUI.enabled = true;
        }
        else
        {
            AButtonMsg.enabled = false;
            AButtonUI.enabled = false;
        }
    }
    public void EnableBUI(bool True)
    {
        if (True)
        {
            BButtonMsg.enabled = true;
            BButtonUI.enabled = true;
        }
        else
        {
            BButtonMsg.enabled = false;
            BButtonUI.enabled = false;
        }
    }
    public void EnableXUI(bool True)
    {
        if (True)
        {
            XButtonMsg.enabled = true;
            XButtonUI.enabled = true;
        }
        else
        {
            XButtonMsg.enabled = false;
            XButtonUI.enabled = false;
        }
    }
    public void EnableYUI(bool True)
    {
        if (True)
        {
            YButtonMsg.enabled = true;
            YButtonUI.enabled = true;
        }
        else
        {
            YButtonMsg.enabled = false;
            YButtonUI.enabled = false;
        }
    }



}
