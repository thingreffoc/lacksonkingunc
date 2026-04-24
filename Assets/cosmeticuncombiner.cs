using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class cosmeticuncombiner : MonoBehaviour
{
    [MenuItem("Tools/UncombineCosmeticz")]
    public static void UncombineCosmetic()
    {
        GameObject[] cosmetics = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in cosmetics)
        {
            // HATS
            if (obj.name == "cookiejar")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/household/cookiejar.asset");
                }
            }
            if (obj.name == "cookies")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/household/cookies.asset");
                }
            }
            if (obj.name == "SAUCEPAN HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/household/pothat.asset");
                }
            }
            if (obj.name == "PLUNGER HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/household/plunger.asset");
                }
            }
            if (obj.name == "BOX OF CHOCOLATES HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/valentines2022/boxofchocolates.asset");
                }
            }
            if (obj.name == "HEART POMPOM HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/valentines2022/pompomhatheart");
                }
            }
             if (obj.name == "BLUE POMPOM HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wintercosmetics/pompomhatblue.asset");
                }
            }
             if (obj.name == "ORANGE POMPOM HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wintercosmetics/pompomhatorange.asset");
                }
            }
            if (obj.name == "PATTERN POMPOM HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wintercosmetics/pompomhatpattern.asset");
                }
            }
            if (obj.name == "STRIPE POMPOM HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wintercosmetics/pompomhatstripe.asset");
                }
            }
            if (obj.name == "BLACK EARMUFFS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wintercosmetics/earmuffsblack.asset");
                }
            }
            if (obj.name == "GREEN EARMUFFS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wintercosmetics/earmuffsgreen.asset");
                }
            }
            if (obj.name == "PINK EARMUFFS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wintercosmetics/earmuffspink.asset");
                }
            }
            if (obj.name == "WHITE EARMUFFS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wintercosmetics/earmuffswhite.asset");
                }
            }
           if (obj.name == "HEADPHONES1")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/elfhat.asset");
                }
            }
            if (obj.name == "GIFT HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/gifthat.asset");
                }
            }
            if (obj.name == "SNOWMAN HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/snowmanhat.asset");
                }
            }
            if (obj.name == "SANTA HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/santahat.asset");
                }
            }
            if (obj.name == "ELF HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmeticselfhat_0.asset");
                }
            }
            if (obj.name == "CHEFS HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/chefshat.asset");
                }
            }
            if (obj.name == "PIRATE BANDANA")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/piratebandana.asset");
                }
            }
            if (obj.name == "STAR PRINCESS TIARA")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/princesshat.asset");
                }
            }
            if (obj.name == "VAMPIRE WIG")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/vampirewig.asset");
                }
            }
            if (obj.name == "WEREWOLF EARS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wolfears.asset");
                }
            }
            if (obj.name == "CLOWN WIG")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/clownwig.asset");
                }
            }
            if (obj.name == "PAPERBAG HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/paperbag.asset");
                }
            }
            if (obj.name == "PUMPKIN HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/pumpkinhat.asset");
                }
            }
            if (obj.name == "BANANA HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/bananahat.asset");
                }
            }
            if (obj.name == "CAT EARS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/catears.asset");
                }
            }
            if (obj.name == "PARTY HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/PARTY HAT.asset");
                }
            }
            if (obj.name == "USHANKA")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/ushanka.asset");
                }
            }
            if (obj.name == "SWEATBAND")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/sweatband.asset");
                }
            }
            if (obj.name == "BASEBALL CAP")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/baseballcap.asset");
                }
            }
             if (obj.name == "CHROME COWBOY HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/chromecowboyhat.asset");
                }
            }
            if (obj.name == "GOLDEN HEAD")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/goldenhead.asset");
                }
            }
            if (obj.name == "FOREHEAD MIRROR")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/foreheadmirror.asset");
                }
            }
            if (obj.name == "PINEAPPLE HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/pineapplehat.asset");
                }
            }
            if (obj.name == "WITCH HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/WITCH HAT.asset");
                }
            }
            if (obj.name == "COCONUT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/coconut.asset");
                }
            }
           if (obj.name == "SUNHAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/sunhat.asset");
                }
            }
            if (obj.name == "CLOCHE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/cloche.asset");
                }
            }
            if (obj.name == "COWBOY HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/cowboyhat.asset");
                }
            }
            if (obj.name == "FEZ")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/fez.asset");
                }
            }
            if (obj.name == "SUNNY SUNHAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/sunnysunhat.asset");
                }
            }
            if (obj.name == "TOP HAT")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/tophat.asset");
                }
            }
            if (obj.name == "BASIC BEANIE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/basicbeanie.asset");
                }
            }
            if (obj.name == "WHITE FEDORA")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/whitefedora.asset");
                }
            }
            if (obj.name == "FLOWER CROWN")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/flowercrown.asset");
                }
            }
        }
    }
}
#endif
