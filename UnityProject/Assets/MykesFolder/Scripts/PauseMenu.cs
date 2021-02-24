using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour {
    AudioSource audioSrc;
    public AudioClip select;
    public AudioClip back;
    public AudioClip achievement;
    public GameObject Player;
    public GameObject MenuPause;
    public GameObject InfoPause;
    public GameObject ItemWindow;
    public GameObject CrystalWindow;
    public GameObject LVLUp;
    public Camera MapCamera;
    public GameObject MapCanvas;
    WeaponSelection WeapSelect;
    Interaction playInt;
    PowerCounter powCount;
    Vector3 MapCamPos;
    Quaternion MapCamRot;
    bool MagicSwordPickedUp;
    bool ThunderSwordPickedUp;
    bool IceSwordPickedUp;
    bool ItemObtained;
    bool isDisabled;
    public static bool Paused;
    bool ItemPaused;
    bool MenuPaused;
    public static bool MapPaused;
    bool EnabledMap;
    public Text LevelUPText;
    public bool mouseVisible;
    bool isLoading;
    // Use this for initialization
    void Start () {
        MapCanvas.SetActive(false);
        Paused = false;
        MapPaused = false;
        ItemPaused = false;
        MenuPause.SetActive(false);
        WeapSelect = Player.GetComponent<WeaponSelection>();
        powCount = Player.GetComponent<PowerCounter>();
        audioSrc = GetComponent<AudioSource>();
        MapCamPos = Player.transform.position;
        MapCamRot = Player.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        EnabledMap = MapSystem.EnabledMap;
        MagicSwordPickedUp = WeaponSelection.MagicSwordPickedUp;
        ThunderSwordPickedUp = WeaponSelection.ThunderSwordPickedUp;
        IceSwordPickedUp = WeaponSelection.IceSwordPickedUp;
        if (Input.GetButtonDown("Map") && Player.activeInHierarchy && !ItemPaused && !MenuPaused && EnabledMap)
        {
            isLoading = SceneLoader.isLoading;
            isDisabled = SceneLoader.isDisabled;
            if (!isLoading && !isDisabled)
            {
                
                MapPaused = !MapPaused;
                Paused = !Paused;
                Time.timeScale = Paused ? 0 : 1;
                if (MapPaused)
                {
                    MapCamPos = Player.transform.position;
                    MapCamRot = Player.transform.rotation;
                    
                    ItemPauseGame(PauseState.MapPause);
                }
                else
                {
                    MapCamera.transform.position = MapCamPos;
                    MapCamera.transform.rotation = MapCamRot;
                    ItemPauseGame(PauseState.MapUnPause);
                }
            }
        }
        if (Input.GetButtonDown("Menu") && Player.activeInHierarchy && !ItemPaused && !MapPaused)
        {
            isLoading = SceneLoader.isLoading;
            isDisabled = SceneLoader.isDisabled;
            if (!isLoading && !isDisabled)
            {
                MenuPaused = !MenuPaused;
                Paused = !Paused;
                mouseVisible = !mouseVisible;
                //Time.timeScale = Paused ? 0 : 1;
                if (Paused == true)
                {
                    ItemPauseGame(PauseState.Pause);
                }
                else
                {
                    ItemPauseGame(PauseState.UnPause);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            mouseVisible = !mouseVisible;
        }

        if (mouseVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
    public void BroadSword()
    {
        WeapSelect.EnableWeapon(1);
        powCount.EnableWeapon(1);
    }

    public void MagicSword()
    {
        if (MagicSwordPickedUp)
        {
            WeapSelect.EnableWeapon(2);
            powCount.EnableWeapon(2);
        }

    }
    public void ThunderSword()
    {
        if (ThunderSwordPickedUp)
        {
            WeapSelect.EnableWeapon(3);
            powCount.EnableWeapon(3);
        }

    }
    public void SwordofIce()
    {
        if (IceSwordPickedUp)
        {
            WeapSelect.EnableWeapon(4);
            powCount.EnableWeapon(4);
        }

    }
    public void Crossbow()
    {
        WeapSelect.EnableWeapon(0);
    }
    public void SetCursor(bool True)
    {
        if (True)
        {
            mouseVisible = true;
        }
        else
        {
            mouseVisible = false;
        }
    }
    public enum PauseState { ItemPause, ItemUnPause, CrystalPause, MapPause, MapUnPause, Pause, UnPause}
    public void ItemPauseGame(PauseState state)
    {
        audioSrc = GetComponent<AudioSource>();
        playInt = Player.GetComponent<Interaction>();
        switch (state)
        {
            case PauseState.ItemPause:
                {
                    playInt.EnableAUI(false, null);
                    playInt.EnableXUI(false);
                    audioSrc.PlayOneShot(achievement);
                    ItemPaused = true;
                    Paused = true;
                    mouseVisible = true;
                    Time.timeScale = 0;
                    InfoPause.SetActive(true);
                    ItemWindow.SetActive(true);          
                    break;
                }
            case PauseState.ItemUnPause:
                {
                    ItemPaused = false;
                    Paused = false;
                    mouseVisible = false;
                    Time.timeScale = 1;
                    ItemWindow.SetActive(false);
                    CrystalWindow.SetActive(false);
                    InfoPause.SetActive(false);
                    audioSrc.PlayOneShot(back);
                    break;
                }
            case PauseState.CrystalPause:
                {
                    if (!isLoading && !isDisabled)
                    {
                        playInt.EnableAUI(false, null);
                        playInt.EnableXUI(false);
                        audioSrc.PlayOneShot(achievement);
                        ItemPaused = true;
                        Paused = true;
                        mouseVisible = true;
                        Time.timeScale = 0;
                        InfoPause.SetActive(true);
                        CrystalWindow.SetActive(true);
                    }
                    break;
                }
            case PauseState.Pause:
                {
                    playInt.EnableAUI(false, null);
                    playInt.EnableXUI(false);
                    audioSrc.PlayOneShot(select);
                    LevelUPText.enabled = false;
                    LVLUp.SetActive(false);
                    MenuPause.SetActive(true);
                    break;
                }
            case PauseState.UnPause:
                {
                    audioSrc.PlayOneShot(back);
                    LevelUPText.enabled = true;
                    LVLUp.SetActive(true);
                    MenuPause.SetActive(false);
                    break;
                }
            case PauseState.MapPause:
                {
                    playInt.EnableAUI(false, null);
                    playInt.EnableXUI(false);
                    audioSrc.PlayOneShot(select);
                    LevelUPText.enabled = false;
                    LVLUp.SetActive(false);
                    MapCamera.enabled = true;
                    MapCanvas.SetActive(true);
                    break;
                }
            case PauseState.MapUnPause:
                {
                   
                    audioSrc.PlayOneShot(back);
                    LevelUPText.enabled = true;
                    LVLUp.SetActive(true);
                    MapCamera.enabled = false;
                    MapCanvas.SetActive(false);
                    break;
                }
        }
    }
    public void UnpauseGame()
    {
        ItemPauseGame(PauseState.ItemUnPause);
    }
}
