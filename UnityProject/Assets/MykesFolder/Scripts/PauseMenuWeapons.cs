using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuWeapons : MonoBehaviour {

    WeaponSelection WeapSelect;
    PowerCounter powCount;
    public GameObject Player;

   

    bool MagicSwordPickedUp;
    bool ThunderSwordPickedUp;
    bool IceSwordPickedUp;

    // Use this for initialization
    void Start () {
        WeapSelect = Player.GetComponent<WeaponSelection>();
        powCount = Player.GetComponent<PowerCounter>();

    }
	
	// Update is called once per frame
	void Update () {
        MagicSwordPickedUp = WeaponSelection.MagicSwordPickedUp;
        ThunderSwordPickedUp = WeaponSelection.ThunderSwordPickedUp;
        IceSwordPickedUp = WeaponSelection.IceSwordPickedUp;
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

}
