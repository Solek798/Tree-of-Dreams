using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalTab : MonoBehaviour

{
    /// <summary>
    /// List of all reachable submenus
    /// </summary>
    [SerializeField] private List<Canvas> SubMenus;
    /// <summary>
    /// Checks which submenu has been opened
    /// </summary>
    /// <param name="activeSubMenu">The submenu you want to reach</param>
    public void openSubMenu(Canvas activeSubMenu)
    {
        foreach (var subMenu in SubMenus)
        {
            if (subMenu == activeSubMenu)
            {
                if (subMenu.gameObject.activeSelf == false)
                {
                    subMenu.gameObject.SetActive(true);
                }
                else
                {
                   subMenu.gameObject.SetActive(false);
                }
                subMenu.gameObject.SetActive(true);
            }
            else
            {
                subMenu.gameObject.SetActive(false);
            }
        }
    }
}
