using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private Dictionary<GameObject, Queue<GameObject>> pools = new();
    private Dictionary<PoolType, Transform> poolParents = new();

   
    private void Awake()
    {
        Instance = this;

        // We create Parents for Pool on stage
        foreach (PoolType type in System.Enum.GetValues(typeof(PoolType)))
        {
            GameObject parentObj = new GameObject(type.ToString() + "_Pool");
            poolParents[type] = parentObj.transform;
            //parentObj.transform.SetParent(parent);
           parentObj.transform.position = transform.position;
        }
    }

    public GameObject Get(GameObject prefab, PoolType type, Vector3 pos)
    {
        Transform parent = poolParents[type];

        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                if (child.name.Contains(prefab.name) && !child.gameObject.activeSelf )
                {
                    child.gameObject.SetActive(true);
                    return child.gameObject;
                }
            }
        }
       
        

        
        GameObject newSoldier = Instantiate(prefab,pos, Quaternion.identity , parent);
        return newSoldier;
    }
    
    
    public void Return(GameObject obj, PoolType type)
    {
        obj.SetActive(false);
        obj.transform.SetParent(poolParents[type]); 
        obj.transform.localPosition = Vector3.zero; 

    }
}


public enum PoolType
{
    Soldiers,
    Buildings 
}