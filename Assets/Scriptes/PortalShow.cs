using UnityEngine;

public class PortalShow : MonoBehaviour
{
    public GameObject Portal;

    void Update()
    {
        if (this.gameObject == null)
        {
            Portal.SetActive(true);
        }
    }
}
