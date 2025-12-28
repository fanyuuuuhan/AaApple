using UnityEngine;

public class Vanilla : MonoBehaviour
{
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.goldSugar == 3)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }
}
