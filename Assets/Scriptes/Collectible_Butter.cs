using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class butterCollection : MonoBehaviour
{
    //0牛奶 1水桶 2攪拌機產物 3清洗機產物 4奶油誠品 5齒輪
    public int id;
    public CollectionGet_Butter collectionGet_Butter;

    void Start()
    {
        collectionGet_Butter = GameManager.Instance.PlayerUI.GetComponentInChildren<CollectionGet_Butter>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collectionGet_Butter.Collection.Add(id);
            collectionGet_Butter.UpdateCollection();
            Destroy(gameObject);
        }
    }
}
