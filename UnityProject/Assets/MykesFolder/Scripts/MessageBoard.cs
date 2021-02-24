using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageBoard : MonoBehaviour {

    public Text infoMessage;

    bool MagicSwordPickedUp;
    bool ThunderSwordPickedUp;
    bool IceSwordPickedUp;

    bool SpikeBootsPickedUp;
    bool SpeedBootsPickedUp;
    bool HealingBootsPickedUp;

    bool StrengthGauntletsPickedUp;
    bool PowerGauntletsPickedUp;
    bool GripGauntletsPickedUp;

    bool FlameBreastPickedUp;
    bool PhantomBreastPickedUp;
    bool FrostBreastPickedUp;

    bool HealixerSMPickedUp;
    bool HealixerMDPickedUp;
    bool HealixerLGPickedUp;

    bool PowerGemSLPickedUp;
    bool PowerGemGDPickedUp;

    bool VitalixerSMPickedUp;
    bool VitalixerMDPickedUp;
    bool VitalixerLGPickedUp;

    bool FireKeyPickedUp;
    bool ThunderKeyPickedUp;
    bool IceKeyPickedUp;
    bool BossFireKeyPickedUp;
    bool BossThunderKeyPickedUp;
    bool BossIceKeyPickedUp;



    // Use this for initialization
    void Start () {

        infoMessage.text = "";
	}
    public void Shop()
    {
        infoMessage.text = "View or purchase items or equipment for your inventory.";
    }
    public void PageOne()
    {
        infoMessage.text = "Item category one.";
    }
    public void PageTwo()
    {
        infoMessage.text = "Item category two.";
    }

    public void Inventory()
    {
        infoMessage.text = "View stored keys or items and power up your weapons.";
    }
    public void Equipment()
    {
        infoMessage.text = "View equipped weapons, armor, footwear and gauntlets.";
    }
    public void KeyItems()
    {
        infoMessage.text = "Key items, open specific entry pathways.";
    }
    public void UseableItems()
    {
        infoMessage.text = "Useable items, recover medicine, vitality and sword magic.";
    }
    public void PowerUpItems()
    {
        infoMessage.text = "Power up aquired swords with upgrade points.";
    }
    public void HealixerSmall()
    {
        HealixerSMPickedUp = HealthItems.HealixerSMPickedUp;
        if (HealixerSMPickedUp)
        {
            infoMessage.text = "Healixer Small: heals 25 Points of health.";
        }
        else
        {
            infoMessage.text = "";
        }
       
    }
    public void HealixerSmallInfo()
    {
        infoMessage.text = "Healixer Small: heals 25 Points of health.";
    }
    public void HealixerMedium()
    {
        HealixerMDPickedUp = HealthItems.HealixerMDPickedUp;
        if (HealixerMDPickedUp)
        {
            infoMessage.text = "Healixer Medium: heals 50 Points of health.";
        }
        else
        {
            infoMessage.text = "";
        }
      
    }
    public void HealixerMediumInfo()
    {
        infoMessage.text = "Healixer Medium: heals 50 Points of health.";
    }
    public void HealixerLarge()
    {
        HealixerLGPickedUp = HealthItems.HealixerLGPickedUp;
        if (HealixerLGPickedUp)
        {
            infoMessage.text = "Healixer Large: heals 100 Points of health.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void HealixerLargeInfo()
    {
        infoMessage.text = "Healixer Large: heals 100 Points of health.";
    }
    public void VitalixerSmall()
    {
        VitalixerSMPickedUp = StaminaItems.VitalixerSMPickedUp;
        if (VitalixerSMPickedUp)
        {
            infoMessage.text = "Vitalixer Small: recovers 25 points of stamina.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void VitalixerSmallInfo()
    {
        infoMessage.text = "Vitalixer Small: recovers 25 points of stamina.";
    }
    public void VitalixerMedium()
    {
        VitalixerMDPickedUp = StaminaItems.VitalixerMDPickedUp;
        if (VitalixerMDPickedUp)
        {
            infoMessage.text = "Vitalixer Medium: recovers 50 points of stamina.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void VitalixerMediumInfo()
    {
        infoMessage.text = "Vitalixer Medium: recovers 50 points of stamina.";
    }
    public void VitalixerLarge()
    {
        VitalixerLGPickedUp = StaminaItems.VitalixerLGPickedUp;
        if (VitalixerLGPickedUp)
        {
            infoMessage.text = "Vitalixer Large: recovers 100 points of stamina.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void VitalixerLargeInfo()
    {
        infoMessage.text = "Vitalixer Large: recovers 100 points of stamina.";
    }
    public void PowerGemSilver()
    {
        PowerGemSLPickedUp = PowerCounter.PowerGemSLPickedUp;
        if (PowerGemSLPickedUp)
        {
            infoMessage.text = "Power Gem Silver: fills one bar of sword magic.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void PowerGemSilverInfo()
    {
        infoMessage.text = "Power Gem Silver: fills one bar of sword magic.";
    }
    public void PowerGemGold()
    {
        PowerGemGDPickedUp = PowerCounter.PowerGemGDPickedUp;
        if (PowerGemGDPickedUp)
        {
            infoMessage.text = "Power Gem Gold: fills three bars of sword magic.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void PowerGemGoldInfo()
    {
        infoMessage.text = "Power Gem Gold: fills three bars of sword magic.";
    }
    public void UpgradeBroad()
    {
        infoMessage.text = "Upgrade Broad Sword.";
    }
    public void UpgradeMagic()
    {
        MagicSwordPickedUp = WeaponSelection.MagicSwordPickedUp;
        if (MagicSwordPickedUp)
        {
            infoMessage.text = "Upgrade Flame Sword.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void UpgradeThunder()
    {
        ThunderSwordPickedUp = WeaponSelection.ThunderSwordPickedUp;
        if (ThunderSwordPickedUp)
        {
            infoMessage.text = "Upgrade Electro Sword.";
        }
        else
        {
            infoMessage.text = "";
        }
      
    }
    public void UpgradeIce()
    {
        IceSwordPickedUp = WeaponSelection.IceSwordPickedUp;
        if (IceSwordPickedUp)
        {
            infoMessage.text = "Upgrade Glacial Sword.";
        }
        else
        {
            infoMessage.text = "";
        }
       
    }
    public void Weapons()
    {
        infoMessage.text = "View equipped swords.";
    }
    public void Armor()
    {
        infoMessage.text = "View equipped armor.";
    }
    public void Footwear()
    {
        infoMessage.text = "View equipped footwear.";
    }
    public void Gloves()
    {
        infoMessage.text = "View equipped gauntlets.";
    }
    public void BroadSword()
    {
        infoMessage.text = "Broad Sword: Capable of a heavy swing.";
    }
    public void MagicSword()
    {
        MagicSwordPickedUp = WeaponSelection.MagicSwordPickedUp;
        if (MagicSwordPickedUp)
        {
            infoMessage.text = "Flame Sword: Burns enemies away with ferocious fire.";
        }
        else
        {
            infoMessage.text = "";
        }
       
    }
    public void ThunderSword()
    {
        ThunderSwordPickedUp = WeaponSelection.ThunderSwordPickedUp;
        if (ThunderSwordPickedUp)
        {
            infoMessage.text = "Electro Sword: Electrocute enemies with a homing electroball.";
        }
        else
        {
            infoMessage.text = "";
        }
        
    }
    public void IceSword()
    {
        IceSwordPickedUp = WeaponSelection.IceSwordPickedUp;
        if (IceSwordPickedUp)
        {
            infoMessage.text = "Glacial Sword: Freezes enemies with a shield of ice.";
        }
        else
        {
            infoMessage.text = "";
        }
        
    }
    public void FireKey()
    {
        FireKeyPickedUp = TempleKeys.FireKey;
        if (FireKeyPickedUp)
        {
            infoMessage.text = "Fire Key: Opens locked doors in the flame temple.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void ThunderKey()
    {
        ThunderKeyPickedUp = TempleKeys.ThunderKey;
        if (ThunderKeyPickedUp)
        {
            infoMessage.text = "Phantom Key: Opens locked doors in the phantom temple.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void IceKey()
    {
        IceKeyPickedUp = TempleKeys.IceKey;
        if (IceKeyPickedUp)
        {
            infoMessage.text = "Ice Key: Opens locked doors in the frost temple.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void BossFireKey()
    {
       BossFireKeyPickedUp = TempleKeys.BossFireKey;
        if (BossFireKeyPickedUp)
        {
            infoMessage.text = "Flame Key: Unlocks the flame temple boss door.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void BossThunderKey()
    {
        BossThunderKeyPickedUp = TempleKeys.BossThunderKey;
        if (BossThunderKeyPickedUp)
        {
            infoMessage.text = "Shadow Key: Unlocks the phantom temple boss door.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void BossIceKey()
    {
        BossIceKeyPickedUp = TempleKeys.BossIceKey;
        if (BossIceKeyPickedUp)
        {
            infoMessage.text = "Frost Key: Unlocks the frost temple boss door.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void None()
    {
        infoMessage.text = "";
    }
    public void NormalBoots()
    {
        infoMessage.text = "Leather Boots: Light and comfortable basic boots.";
    }
    public void SpikeBoots()
    {
        SpikeBootsPickedUp = Boots.SpikeBootsPickedUp;
        if (SpikeBootsPickedUp)
        {
            infoMessage.text = "Spike Boots: Made of pure vibranium to walk on spikes.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void SpikeBootsInfo()
    {
        infoMessage.text = "Spike Boots: Made of pure vibranium to walk on spikes.";
    }
    public void SpeedBoots()
    {
        SpeedBootsPickedUp = Boots.SpeedBootsPickedUp;
        if (SpeedBootsPickedUp)
        {
            infoMessage.text = "Speed Boots: Extra light boots to run extra fast!";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void SpeedBootsInfo()
    {
        infoMessage.text = "Speed Boots: Extra light boots to run extra fast!";
    }
    public void HealingBoots()
    {
        HealingBootsPickedUp = Boots.HealingBootsPickedUp;
        if (HealingBootsPickedUp)
        {
            infoMessage.text = "Healing Boots: With powered technology, heals health with movement.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void LeatherGauntlets()
    {
        infoMessage.text = "Leather Gauntlets: Comfortable grip made with tough leather.";
    }
    public void StrengthGauntlets()
    {
        StrengthGauntletsPickedUp = Gauntlets.StrengthGauntletsPickedUp;
        if (StrengthGauntletsPickedUp)
        {
            infoMessage.text = "Strength Gauntlets: Attack Power +1, can move heavy blocks.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void PowerGauntlets()
    {
        PowerGauntletsPickedUp = Gauntlets.PowerGauntletsPickedUp;
        if (PowerGauntletsPickedUp)
        {
            infoMessage.text = "Power Gauntlets: Increases stamina X2 , can move electric blocks.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void GripGauntlets()
    {
        GripGauntletsPickedUp = Gauntlets.GripGauntletsPickedUp;
        if (GripGauntletsPickedUp)
        {
            infoMessage.text = "Grip Gauntlets: Increases sword magic X2, can move ice blocks.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void LeatherBreast()
    {
        infoMessage.text = "Leather Breast: Padded leather that protects from minimal damage";
    }
    public void FlameBreast()
    {
        FlameBreastPickedUp = Breast.FlameBreastPickedUp;
        if (FlameBreastPickedUp)
        {
            infoMessage.text = "Flame Breast: Reduces damage by 1/2, protects against extreme heat.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void PhantomBreast()
    {
        PhantomBreastPickedUp = Breast.PhantomBreastPickedUp;
        if (PhantomBreastPickedUp)
        {
            infoMessage.text = "Phantom Breast: Doubles experience gained, enemies are blind.";
        }
        else
        {
            infoMessage.text = "";
        }
    }
    public void FrostBreast()
    {
        FrostBreastPickedUp = Breast.FrostBreastPickedUp;
        if (FrostBreastPickedUp)
        {
            infoMessage.text = "Frost Breast: Adds an extra sword power up, protects against extreme freezing.";
        }
        else
        {
            infoMessage.text = "";
        }
    }

}
