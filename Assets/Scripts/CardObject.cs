using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
   public Card info;

   private SpriteRenderer spriteRenderer;
   private bool active = false;

   // Start is called before the first frame update
   void Start()
   {
       spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
   }

   // Update is called once per frame
   void Update()
   {
      if (gameObject.activeInHierarchy) {
         if (!active) {
            OnActive();
            active = true;
         }
      } else {
         active = false;
      }
   }

   void OnActive() {
      Debug.Log("card is active: " + info.toString());
      Sprite sprite = SpritePool.instance.GetSpriteByName(info.toString());
      spriteRenderer.sprite = sprite;
   }

}
