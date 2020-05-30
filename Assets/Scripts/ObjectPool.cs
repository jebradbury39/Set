using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
   public static ObjectPool instance;
   public GameObject pooledObject;
   public int pooledAmount = 81;
   public bool willGrow = true;

   List<GameObject> pooledObjects = new List<GameObject>();

   void Awake() {
      instance = this;
   }

   public GameObject GetPooledObject() {
      if (pooledObjects.Count == 0) {
         for (int i = 0; i < pooledAmount; i++) {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
         }
      }

      for (int i = 0; i < pooledObjects.Count; i++) {
         if (!pooledObjects[i].activeInHierarchy) {
            return pooledObjects[i];
         }
      }

      if (willGrow) {
         GameObject obj = (GameObject)Instantiate(pooledObject);
         obj.SetActive(false);
         pooledObjects.Add(obj);
      }

      return null;
   }

}
