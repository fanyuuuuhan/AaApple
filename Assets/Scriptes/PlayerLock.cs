using UnityEngine;

public class PlayerLock : MonoBehaviour
{
    public GameObject NeedKey;
    public static bool isUnlock = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Æ_°ÍÂê°»´ú
        if (collision.CompareTag("Player"))
        {
            if (!PlayerMovement.isKey)
            {
                NeedKey.gameObject.SetActive(true);
            }
            else
            {
                isUnlock = true;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NeedKey.gameObject.SetActive(false);

        }
    }
}
