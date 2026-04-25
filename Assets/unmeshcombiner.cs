using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
// using pp truast

public class unmeshcombiner : MonoBehaviour //MADE BY ROASTY DONT REMOVE MY COMMENTS
{
    [MenuItem("Tools/UncombineCosmeticz")]
    public static void UncombineCosmetic()
    {
        GameObject[] cosmetics = GameObject.FindObjectsOfType<GameObject>();
	GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            if (meshCollider != null && meshFilter != null && meshCollider.sharedMesh != null)
            {
                meshFilter.sharedMesh = meshCollider.sharedMesh;
            }
	#if UNITY_EDITOR
            else if (meshFilter != null && meshCollider == null)
            {
                GameObjectUtility.SetStaticEditorFlags(obj, 0);
            }
	#endif
        }
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
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/gifthat_0.asset");
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
            // BADGES
             if (obj.name == "GOLD WRENCH")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/household/wrenchgold.asset");
                }
            }
            if (obj.name == "REGULAR WRENCH")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/household/wrench.asset");
                }
            }
            if (obj.name == "GT1 BADGE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/1stanniversary/gt1badge.asset");
                }
            }
            if (obj.name == "RED ROSE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/valentines2022/redrose.asset");
                }
            }
            if (obj.name == "PINK ROSE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/valentines2022/pinkrose.asset");
                }
            }
            if (obj.name == "GOLD ROSE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/valentines2022/goldrose.asset");
                }
            }
            if (obj.name == "BLACK ROSE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/valentines2022/blackrose.asset");
                }
            }
            if (obj.name == "CHEST HEART")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/valentines2022/chestheart.asset");
                }
            }
            if (obj.name == "ICICLE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wintercosmetics/icicle.asset");
                }
            }
            if (obj.name == "SPARKLER")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/sparkler.asset");
                }
            }
            if (obj.name == "CANDY CANE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/candycane.asset");
                }
            }
            if (obj.name == "TURKEY LEG")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/turkeyleg.asset");
                }
            }
            if (obj.name == "STAR PRINCESS WAND")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/princessbadge.asset");
                }
            }
            if (obj.name == "VAMPIRE COLLAR")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/vampirecollar.asset");
                }
            }
            // CHILDREN BADGES
            if (obj.name == "TURKEY FINGER PUPPET")
            {
                if (obj != null)
                {
                    if (obj.transform.childCount >= 5)
                    {
                        GameObject child = obj.transform.GetChild(0).gameObject;
                        GameObject child1 = obj.transform.GetChild(1).gameObject;
                        GameObject child2 = obj.transform.GetChild(2).gameObject;
                        GameObject child3 = obj.transform.GetChild(3).gameObject;
                        GameObject child4 = obj.transform.GetChild(4).gameObject;
                        child.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/turkeyfingerpuppethead.asset");
                        child1.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/turkeyfingerpuppetindexfeathers.asset");
                        child2.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/turkeyfingerpuppetmiddlefeathers.asset");
                        child3.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/turkeyfingerpuppetpalm.asset");
                        child4.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/turkeyfingerpuppetthumbfeathers.asset");
                    }
                }
            }
            if (obj.name == "REGULAR FORK AND KNIFE")
            {
                if (obj != null)
                {
                    if (obj.transform.childCount >= 3)
                    {
                        GameObject child = obj.transform.GetChild(0).gameObject;
                        GameObject child1 = obj.transform.GetChild(1).gameObject;
                        child.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/household/knife.asset");
                        child1.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/household/fork.asset");
                    }
                }
            }
            if (obj.name == "GOLD FORK AND KNIFE")
            {
                if (obj != null)
                {
                    if (obj.transform.childCount >= 3)
                    {
                        GameObject child = obj.transform.GetChild(0).gameObject;
                        GameObject child1 = obj.transform.GetChild(1).gameObject;
                        child.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/household/knifegold.asset");
                        child1.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/household/forkgold.asset");
                    }
                }
            }
            if (obj.name == "THUMB PARTYHATS")
            {
                if (obj != null)
                {
                    if (obj.transform.childCount >= 3)
                    {
                        GameObject child = obj.transform.GetChild(0).gameObject;
                        GameObject child1 = obj.transform.GetChild(1).gameObject;
                        child.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/1stanniversary/thumbpartyhatleft.asset");
                        child1.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/1stanniversary/thumbpartyhatright.asset");
                    }
                }
            }
            if (obj.name == "CITY PIN")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/citypin.asset");
                }
            }
            if (obj.name == "WEREWOLF CLAWS")
            {
                if (obj != null)
                {
                    if (obj.transform.childCount >= 6)
                    {
                        GameObject child = obj.transform.GetChild(0).gameObject;
                        GameObject child1 = obj.transform.GetChild(1).gameObject;
                        GameObject child2 = obj.transform.GetChild(2).gameObject;
                        GameObject child3 = obj.transform.GetChild(3).gameObject;
                        GameObject child4 = obj.transform.GetChild(4).gameObject;
                        GameObject child5 = obj.transform.GetChild(5).gameObject;
                        child.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wolfclawindexleft.asset");
                        child1.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wolfclawindexright.asset");
                        child2.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wolfclawmiddleleft.asset");
                        child3.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wolfclawmiddleright.asset");
                        child4.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wolfclawthumbleft.asset");
                        child5.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wolfclawthumbright.asset");
                    }
                }
            }
            if (obj.name == "CLOWN FRILL")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/clownfrill.asset");
                }
            }
            if (obj.name == "TREE PIN")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/treepin.asset");
                }
            }
            if (obj.name == "CANYON PIN")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/canyonpin.asset");
                }
            }
            if (obj.name == "BOWTIE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/bowtie.asset");
                }
            }
            if (obj.name == "BASIC SCARF")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/basicscarf.asset");
                }
            }
            if (obj.name == "ADMINISTRATOR BADGE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/earlyaccessbadge_0.asset");
                }
            }
            if (obj.name == "EARLY ACCESS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/earlyaccessbadge.asset");
                }
            }
            if (obj.name == "CRYSTALS PIN")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/crystalspin.asset");
                }
            }
            if (obj.name == "NECK SCARF")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/neckscarf.asset");
                }
            }
            if (obj.name == "MOD STICK")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/stick.asset");
                }
            }
            // FACES
            if (obj.name == "HEART GLASSES")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/valentine2022/heartglasses.asset");
                }
            }
            if (obj.name == "BOXY SUNGLASSES")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/boxyglasses.asset");
                }
            }
            if (obj.name == "ROSY CHEEKS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wintercosmetics/rosycheeks.asset");
                }
            }
            if (obj.name == "NOSE SNOWFLAKE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/nosesnowflake.asset");
                }
            }
            if (obj.name == "2022 GLASSES")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/2022glasses.asset");
                }
            }
            if (obj.name == "ORNAMENT EARRINGS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/ornamentearrings.asset");
                }
            }
            if (obj.name == "SANTA BEARD")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/santabeard.asset");
                }
            }
            if (obj.name == "FACE SCARF")
            {
                if (obj != null)
                {
                    if (obj.transform.childCount >= 3)
                    {
                        GameObject child = obj.transform.GetChild(0).gameObject;
                        GameObject child1 = obj.transform.GetChild(1).gameObject;
                        child.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/facescarfchest.asset");
                        child1.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/facescarfface.asset");
                    }
                }
            }
            if (obj.name == "MAPLE LEAF")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmeticsmapleleaf.asset");
                }
            }
            if (obj.name == "STAR PRINCESS GLASSES")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/princessface.asset");
                }
            }
            if (obj.name == "VAMPIRE FANGS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/vampirefangs.asset");
                }
            }
            if (obj.name == "WEREWOLF FACE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/wolfface.asset");
                }
            }
            if (obj.name == "GORILLA PIN")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/gorillapin.asset");
                }
            }
            if (obj.name == "CLOWN NOSE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/clownnose.asset");
                }
            }
            if (obj.name == "WITCH NOSE")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/witchnose.asset");
                }
            }
            if (obj.name == "MUMMY WRAP")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/mummywrap.asset");
                }
            }
            if (obj.name == "BIG EYEBROWS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/bigeyebrows.asset");
                }
            }
            if (obj.name == "NOSE RING")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/nosering.asset");
                }
            }
            if (obj.name == "BASIC EARRINGS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/basicearrings.asset");
                }
            }
            if (obj.name == "TRIPLE EARRINGS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/tripleearrings.asset");
                }
            }
            if (obj.name == "EYEBROW STUD")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/eyebrowstud.asset");
                }
            }
            if (obj.name == "TRIANGLE SUNGLASSES")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/trianglesunglasses.asset");
                }
            }
            if (obj.name == "SKULL MASK")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/skullmask.asset");
                }
            }
            if (obj.name == "RIGHT EYEPATCH")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/eyepatchright.asset");
                }
            }
            if (obj.name == "LEFT EYEPATCH")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/eyepatchleft.asset");
                }
            }
            if (obj.name == "DOUBLE EYEPATCH")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/eyepatchdouble.asset");
                }
            }
            if (obj.name == "GOGGLES")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/goggles.asset");
                }
            }
            if (obj.name == "SURGICAL MASK")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/SURGICAL MASK.asset");
                }
            }
            if (obj.name == "TORTOISESHELL SUNGLASSES")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/sunglasses.asset");
                }
            }
            if (obj.name == "AVIATORS")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/sunglasses_0.asset");
                }
            }
            if (obj.name == "ROUND SUNGLASSES")
            {
                if (obj != null)
                {
                    obj.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Resources/objects/cosmetics/roundsunglasses.asset");
                }
            }
            Debug.Log("wowie all ur cosmmetics r uncombined uhh yar");
            Debug.Log("Thank u for using roastys cosmeticuncombiner, join the devhub for more coolio scripts n assets");
            Debug.Log("https://discord.gg/HccMTkJR2C");
        }
    }
}
#endif
//SCRIPT MADE BY ROASTY DONT REMOVE THESE COMMENTS U BUM 