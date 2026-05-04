using UnityEngine;
using System.Collections.Generic;

// --- HOW TO ADD A NEW UPGRADE ---
// 1. HIERARCHY: Duplicate an existing upgrade GameObject and rename it.
// 2. INSPECTOR: Select the UpgradeButtonController and find the 'All Upgrades' list.
// 3. LIST SETUP: Click the '+' icon to add a new slot.
// 4. ASSIGN: Drag the new GameObject into the 'Upgrade Object' slot.
// 5. RARITY: Choose Basic, Rare, Unique, or Exclusive from the dropdown.
// 6. EXCLUSIVES: If Exclusive, type the EXACT name of its counterpart in 'Exclusive Partner Name'.
// 7. BUTTON LINK: Select the new GameObject in the Hierarchy and find its Button component.
// 8. ON CLICK: Ensure the 'SelectUpgrade' function is assigned in the OnClick() event.
// 9. INDEX: Set the 'SelectUpgrade' integer to match its position in the 'All Upgrades' list.
//    (Example: If it is the 22nd item in the list, the index is 21).
// --------------------------------
public class UpgradeSelector : MonoBehaviour
{
    public enum Rarity { Basic, Rare, Unique, Exclusive }

    [System.Serializable]
    public class UpgradeData
    {
        public GameObject upgradeObject;
        public Rarity rarity;
        public bool isAcquired; // Only stays true for Unique/Exclusive
        public string exclusivePartnerName; 
    }

    public List<UpgradeData> allUpgrades; 
    private float[] xPositions = { 155f, 385f, 615f, 845f };


    //Attaches to the buttons with a number parameter
    public void SelectUpgrade(int index)
    {
        UpgradeData selected = allUpgrades[index];

        //Only mark as acquired if it's not Basic orRare
        if (selected.rarity == Rarity.Unique || selected.rarity == Rarity.Exclusive)
        {
            selected.isAcquired = true;

            //Handles Exclusive Partners
            if (!string.IsNullOrEmpty(selected.exclusivePartnerName))
            {
                foreach (var u in allUpgrades)
                {
                    if (u.upgradeObject.name == selected.exclusivePartnerName)
                    {
                        u.isAcquired = true; 
                        break;
                    }
                }
            }
        }

        Debug.Log("Selected: " + selected.upgradeObject.name);
        //Panel Closes after this
    }

    public void RandomizeUpgrades()
    {
        foreach(var u in allUpgrades) u.upgradeObject.SetActive(false);

        List<int> validIndexes = new List<int>();

        //Filters the pool
        for (int i = 0; i < allUpgrades.Count; i++)
        {
            //If it's acquired, skip it entirely
            if (allUpgrades[i].isAcquired) continue;

            //Rarity Weighting
            //Basics have a weight of 5, Rares 2, and Unique/Exclusive just 1.
            int tickets = 1;
            if (allUpgrades[i].rarity == Rarity.Basic) tickets = 5;
            else if (allUpgrades[i].rarity == Rarity.Rare) tickets = 2;

            for (int t = 0; t < tickets; t++)
            {
                validIndexes.Add(i);
            }
        }

        //Pick 4 upgrades from the pool at random
        for (int i = 0; i < 4; i++)
        {
            if (validIndexes.Count == 0) break;

            int randomIndex = Random.Range(0, validIndexes.Count);
            int upgradeIndex = validIndexes[randomIndex];
            
            GameObject obj = allUpgrades[upgradeIndex].upgradeObject;
            obj.SetActive(true);
            obj.transform.position = new Vector3(xPositions[i], obj.transform.position.y, obj.transform.position.z);

            
            //removes the selected index from the list so we don't pick the same upgrade twice in the same window.
            int selectedID = upgradeIndex;
            validIndexes.RemoveAll(id => id == selectedID);
        }
    }
}