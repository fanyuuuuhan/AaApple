using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CollectionGet_Butter : MonoBehaviour
{
    public List<int> Collection;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void UpdateCollection()
    {   
        GameObject child;
        int index = 0;
        for (int i = 0; i < 6; i++)
        {
            child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        GameObject milk = gameObject.transform.GetChild(0).gameObject;
        foreach (int id in Collection)
        {
            child = gameObject.transform.GetChild(id).gameObject;
            child.GetComponent<RectTransform>().SetPositionAndRotation(
                new Vector3(
                    milk.GetComponent<RectTransform>().position.x - index * 135f,
                    milk.GetComponent<RectTransform>().position.y,
                    milk.GetComponent<RectTransform>().position.z    
                ), milk.GetComponent<RectTransform>().rotation
            );
            child.SetActive(true);
            index++;
        }
    }
}
