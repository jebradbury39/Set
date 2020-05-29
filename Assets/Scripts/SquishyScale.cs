using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishyScale : MonoBehaviour
{
   public bool run = false;

   // Start is called before the first frame update
   void Start()
   {
     
   }

   // Update is called once per frame
   void Update()
   {
      float scale = 1.0f;
      if (run) {
         scale = Mathf.Sin(2 * Mathf.PI * Time.time * 0.5f) * 0.05f + 0.95f;
      }
      gameObject.transform.localScale = new Vector3(scale, scale, 1);
   }
}
