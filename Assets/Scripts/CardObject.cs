using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
   public Card info;

   private SpriteRenderer spriteRenderer;
   private bool selected = false;

   // Start is called before the first frame update
   void Start()
   {
       spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
   }

   // Update is called once per frame
   void Update()
   {
   }

   public void SetActive(bool active) {
      gameObject.SetActive(active);
      if (active) {
         Sprite sprite = SpritePool.instance.GetSpriteByName(info.toString());
         if (spriteRenderer == null) {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
         }
         spriteRenderer.sprite = sprite;
         
         //reset
         selected = false;
      }
   }

   void OnMouseDown() {
      if (!selected) {
         Player.instance.SelectCard(this);
      } else {
         Player.instance.DeselectCard(this);
      }
      selected = !selected;
   }

   public void TakeCard() {
      CardManager.instance.TakeCard(info);
      SetActive(false);
   }
}
