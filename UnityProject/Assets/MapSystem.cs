using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapSystem : MonoBehaviour {

    public bool StartDemo;
    public AudioSource audioSrc;
    public AudioClip mapSfx;

    public GameObject gameCtrl;
    PauseMenu itemPause;

    public GameObject InfoMenuFlameMap;
    public GameObject InfoMenuPhantMap;
    public GameObject InfoMenuFrostMap;
    public GameObject InfoMenuFinalMap;
    public GameObject InfoMenuOverMap;

    GameObject OverMap;
    GameObject FlameMap;
    GameObject PhantMap;
    GameObject FrostMap;
    GameObject FinalMap;

    public Image UIFlame;
    public Image UIPhant;
    public Image UIFrost;
    public Image UIFinal;
    public Image UIOver;

    bool FlameMapPickedUp;
    bool PhantMapPickedUp;
    bool FrostMapPickedUp;
    bool FinalMapPickedUp;
    bool OverMapPickedUp;

    bool OverMapActive;
    bool FlameMapActive;
    bool PhantMapActive;
    bool FrostMapActive;
    bool FinalMapActive;

    bool lookForMap;

    public static bool EnabledMap = false;
    
    public Text MapTitle;
    private void Start()
    {
        if (StartDemo)
        {
            FlameMapPickedUp = true;
            PhantMapPickedUp = true;
            FrostMapPickedUp = true;
            FinalMapPickedUp = true;
            OverMapPickedUp = true;
            lookForMap = true;
        }
        else
        {
            FlameMapPickedUp = false;
            PhantMapPickedUp = false;
            FrostMapPickedUp = false;
            FinalMapPickedUp = false;
            OverMapPickedUp = false;
            lookForMap = false;
        }
        
    }

    public void Update()
    {
        if (lookForMap)
        {
            OverMap = GameObject.Find("OverWorld/Static/Map");
            if (OverMap != null)
            {
                if (OverMapPickedUp)
                {
                    if (!OverMapActive)
                    {
                        SelectMap(EnableMap.OverWorld);
                    }
                }
                else
                {
                    MapTitle.text = null;
                    EnabledMap = false;
                }
            }
            else if (OverMap == null)
            {
                OverMap = null;
            }

            FlameMap = GameObject.Find("FireTemple/Static/Map");
            if (FlameMap != null)
            {
                if (FlameMapPickedUp)
                {
                    if (!FlameMapActive)
                    {
                        SelectMap(EnableMap.Flame);
                        FlameMapActive = true;
                    }
                }
                else
                {
                    MapTitle.text = null;
                    EnabledMap = false;
                }
            }
            else if (FlameMap == null)
            {
                FlameMap = null;
            }

            PhantMap = GameObject.Find("PhantomTemple/Static/Map");
            if (PhantMap != null)
            {
                if (PhantMapPickedUp)
                {
                    if (!PhantMapActive)
                    {
                        SelectMap(EnableMap.Phant);
                        PhantMapActive = true;
                    }
                }
                else
                {
                    MapTitle.text = null;
                    EnabledMap = false;
                }
            }
            else if (PhantMap == null)
            {
                PhantMap = null;
            }
            FrostMap = GameObject.Find("IceTemple/Static/Map");
            if (FrostMap != null)
            {
                if (FrostMapPickedUp)
                {
                    if (!FrostMapActive)
                    {
                        SelectMap(EnableMap.Frost);
                        FrostMapActive = true;
                    }
                }
                else
                {
                    MapTitle.text = null;
                    EnabledMap = false;
                }
            }
            else if (FrostMap == null)
            {
                FrostMap = null;
            }

            FinalMap = GameObject.Find("FinalTemple/Static/Map");
            if (FinalMap != null)
            {
                if (FinalMapPickedUp)
                {
                    if (!FinalMapActive)
                    {
                        SelectMap(EnableMap.Final);
                        FinalMapActive = true;
                    }
                }
                else
                {
                    MapTitle.text = null;
                    EnabledMap = false;
                }
            }
            else if (FinalMap == null)
            {
                FinalMap = null;
            }
            else
            {
                FlameMap = null;
                PhantMap = null;
                FrostMap = null;
                FinalMap = null;
                EnabledMap = false;
                MapTitle.text = null;
            }
        }
    }
    public enum EnableMap { OverWorld, Flame, Phant, Frost, Final }
    public void SelectMap(EnableMap type)
    {
        switch (type)
        {
            case EnableMap.OverWorld:
                {
                    OverMapActive = true;
                    FlameMapActive = false;
                    PhantMapActive = false;
                    FrostMapActive = false;
                    FinalMapActive = false;
                    lookForMap = false;
                    UIFlame.enabled = false;
                    UIPhant.enabled = false;
                    UIFrost.enabled = false;
                    UIFinal.enabled = false;
                    UIOver.enabled = true;
                    EnabledMap = true;
                    MapTitle.text = "Fated Island  Map";
                    OverMap.SetActive(true);
                    break;
                }
            case EnableMap.Flame:
                {
                    OverMapActive = false;
                    FlameMapActive = true;
                    PhantMapActive = false;
                    FrostMapActive = false;
                    FinalMapActive = false;
                    lookForMap = false;
                    UIFlame.enabled = true;
                    UIPhant.enabled = false;
                    UIFrost.enabled = false;
                    UIFinal.enabled = false;
                    UIOver.enabled = false;
                    EnabledMap = true;
                    MapTitle.text = "Flame Temple Map";
                    FlameMap.SetActive(true);
                    break;
                }
            case EnableMap.Phant:
                {
                    OverMapActive = false;
                    FlameMapActive = false;
                    PhantMapActive = true;
                    FrostMapActive = false;
                    FinalMapActive = false;
                    lookForMap = false;
                    UIFlame.enabled = false;
                    UIPhant.enabled = true;
                    UIFrost.enabled = false;
                    UIFinal.enabled = false;
                    UIOver.enabled = false;
                    EnabledMap = true;
                    MapTitle.text = "Phantom Temple Map";
                    PhantMap.SetActive(true);
                    break;
                }
            case EnableMap.Frost:
                {
                    OverMapActive = false;
                    FlameMapActive = false;
                    PhantMapActive = false;
                    FrostMapActive = true;
                    FinalMapActive = false;
                    lookForMap = false;
                    UIFlame.enabled = false;
                    UIPhant.enabled = false;
                    UIFrost.enabled = true;
                    UIFinal.enabled = false;
                    UIOver.enabled = false;
                    EnabledMap = true;
                    MapTitle.text = "Frost Temple Map";
                    FrostMap.SetActive(true);
                    break;
                }
            case EnableMap.Final:
                {
                    OverMapActive = false;
                    FlameMapActive = false;
                    PhantMapActive = false;
                    FrostMapActive = false;
                    FinalMapActive = true;
                    lookForMap = false;
                    UIFlame.enabled = false;
                    UIPhant.enabled = false;
                    UIFrost.enabled = false;
                    UIFinal.enabled = true;
                    UIOver.enabled = false;
                    EnabledMap = true;
                    MapTitle.text = "Final Temple  Map";
                    FinalMap.SetActive(true);
                    break;
                }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FlameMap"))
        {
            audioSrc.PlayOneShot(mapSfx);
            FlameMapPickedUp = true;
            itemPause = gameCtrl.GetComponent<PauseMenu>();
            itemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            InfoMenuFlameMap.SetActive(true);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PhantMap"))
        {
            audioSrc.PlayOneShot(mapSfx);
            PhantMapPickedUp = true;
            itemPause = gameCtrl.GetComponent<PauseMenu>();
            itemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            InfoMenuPhantMap.SetActive(true);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("FrostMap"))
        {
            audioSrc.PlayOneShot(mapSfx);
            FrostMapPickedUp = true;
            itemPause = gameCtrl.GetComponent<PauseMenu>();
            itemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            InfoMenuFrostMap.SetActive(true);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("OverMap"))
        {
            audioSrc.PlayOneShot(mapSfx);
            OverMapPickedUp = true;
            itemPause = gameCtrl.GetComponent<PauseMenu>();
            itemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            InfoMenuOverMap.SetActive(true);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("FinalMap"))
        {
            audioSrc.PlayOneShot(mapSfx);
            FinalMapPickedUp = true;
            itemPause = gameCtrl.GetComponent<PauseMenu>();
            itemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            InfoMenuFinalMap.SetActive(true);
            Destroy(other.gameObject);
        }
    }
    public void FindMap()
    {
        lookForMap = true;
    }
}
