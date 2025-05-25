using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject HTPPannel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHTPPanel()
    {
        if (HTPPannel != null)
        {
            HTPPannel.SetActive(true);
        }
    }
    public void HideHTPPanel()
    {
        if (HTPPannel != null)
        {
            HTPPannel.SetActive(false);
        }
    }
}
