using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectSign { Spike, Speed, Heal, Flame, Phantom, Frost, Final, Welcome, Swords, ArmorFlame, ArmorPhant, ArmorFrost, Gauntlets, FinalBoss, Ghosts, EnemyEye, PhantEnemy}

public class InformationSign : MonoBehaviour {
    [Header("Choose Sign")]
    public SelectSign SignInfo;
    Interaction PlayerInt;
    PlayerStats PlayST;
    AudioSource audioSrc;
    GameObject SignMessage;
    GameObject GameCtrl;
    PauseMenu InfoPauseMenu;
    public AudioClip buttonUISFX;
    Text infoMenuText;
    bool inTerritory;
    bool openInfo;

	// Update is called once per frame
	void Update () {
        if (inTerritory)
        {
            if (Input.GetButtonDown("Open") && !openInfo)
            {
                openInfo = true;
                GameCtrl = GameObject.Find("GameControl");
                InfoPauseMenu = GameCtrl.GetComponent<PauseMenu>();
                InfoPauseMenu.ItemPauseGame(PauseMenu.PauseState.ItemPause);
                SignMessage = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/");
                SignMessage.SetActive(true);
                SwitchSignInfo(SignInfo);
                PlayerInt = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
                PlayerInt.EnableAUI(false, null);

            }
            else if (Input.GetButtonDown("Open") && openInfo)
            {
                openInfo = false;
                PlayerInt = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
                PlayerInt.EnableAUI(true, "Check");
            }
        }
	}
    public void SwitchSignInfo(SelectSign info)
    {
        switch (info)
        {
            case SelectSign.Spike:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "Atop the highest point of the final Temple, lies equipment that allows you to freely walk over spikes.";
                    break;
                }
            case SelectSign.Speed:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "Legends were told that there is a type of equipment that's hidden in the icy mountains, that give the ability to move faster.";
                    break;
                }
            case SelectSign.Heal:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "The ability to heal while walking is said to be locked behind phantom doors that are opened by powering certain pillars.";
                    break;
                }
            case SelectSign.Flame:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "The flame Temple is deep within the molten mountains of this island which contains the crystal of fire.";
                    break;
                }
            case SelectSign.Phantom:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "The Phantom Temple is a dark crypt that binds withered souls. Locked deep inside is the crystal of thunder.";
                    break;
                }
            case SelectSign.Frost:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "The Frost Temple is Atop the coldest part of the island deep into the mountains, Containing the Crystal of ice.";
                    break;
                }
            case SelectSign.Final:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "Legends have it, the holder of all three crystals is able to unlock the door to the final temple.";
                    break;
                }
            case SelectSign.Welcome:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "Welcome to Destiny Venture: Legend of the Crystals! " +
                        "Prologue: After following a map and crashing into an dark island from the storm, you find yourself searching for sacred crystals which hold the power of fire, lightning and ice. " +
                        "Having all three crystals grants the chosen one to beable to defeat the elemental god that wields the power to bring an apocalypse to the world " +
                        "Many dangers await on this island, your adventure begins here!";
                    break;
                }
            case SelectSign.ArmorFlame:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "In the Temple of fire, there are areas of extreme heat. Access to those areas without Heat Resistant Breast will be burned alive.";
                    break;
                }
            case SelectSign.ArmorPhant:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "In the Temple of Shadows, binding souls consume those who are unweary. It has been told that there is a breast that blinds you from shadows.";
                    break;
                }
            case SelectSign.ArmorFrost:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "In the Temple of Ice, there are areas of extreme cold. Access to those areas without freeze Resistant Breast will be turned to ice.";
                    break;
                }
            case SelectSign.Swords:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "Locked Deep inside the temples lie three legendary swords of Fire, Lightning and ice. With these swords wields the power to bring down a god of destruction.";
                    break;
                }
            case SelectSign.FinalBoss:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "Oblivious, The Elemental God Titan, Has the power to bring to crumble the world. Lands burned to ash, lightning storms tearing the skys and Oceans completely frozen over. " +
                        "The Titan can only be harmed by the opposite opposing element but resistant to all types of damage otherwise.";
                    break;
                }
            case SelectSign.Gauntlets:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "There are special types of blockes that are too heavy to push, need to be powered up to move and too slippery to pull. With powerful special gauntlets these objects can be easily moved.";
                    break;
                }
            case SelectSign.Ghosts:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "Lurking in each temple are Elemental Phantoms that are the guardians of the Crystals, each with specific powers to shoot fireballs, duplicate themselves and cast ice barriers.";
                    break;
                }
            case SelectSign.PhantEnemy:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "Becareful of spirits hiding in the shadows, with in contact the can drain energy and eventually your lifeforce.";
                    break;
                }
            case SelectSign.EnemyEye:
                {
                    infoMenuText = GameObject.Find("GameControl/Player/InGameCanvas/InfoMenu/ItemWindow/SignMessage/Info").GetComponent<Text>();
                    infoMenuText.text = "Nightime can be dangerous to the unweary traveller, some enemies change at night fall and can be more difficult to defeat.";
                    break;
                }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTerritory = true;
            PlayST = other.gameObject.GetComponent<PlayerStats>();
            PlayST.SendMainMSG(2, "Information", 3);
            PlayerInt = other.gameObject.GetComponent<Interaction>();
            audioSrc = other.gameObject.GetComponent<AudioSource>();
            audioSrc.PlayOneShot(buttonUISFX);
            PlayerInt.EnableAUI(true, "Check");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            openInfo = false;
            inTerritory = false;
            PlayerInt = other.gameObject.GetComponent<Interaction>();
            PlayerInt.EnableAUI(false, null);
        }
    }
    public void ResetSign()
    {
        openInfo = false;
    }
}
