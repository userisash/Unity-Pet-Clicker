using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject tab1Content;
    public GameObject tab2Content;
    public GameObject tab3Content;

    public void ShowTab1()
    {
        tab1Content.SetActive(true);
        tab2Content.SetActive(false);
        tab3Content.SetActive(false);
    }

    public void ShowTab2()
    {
        tab1Content.SetActive(false);
        tab2Content.SetActive(true);
        tab3Content.SetActive(false);
    }

    public void ShowTab3()
    {
        tab1Content.SetActive(false);
        tab2Content.SetActive(false);
        tab3Content.SetActive(true);
    }
}