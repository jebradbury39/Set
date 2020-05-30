using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   private List<CardObject> selectedCards = new List<CardObject>();
   public List<Card> sets = new List<Card>(); //multiple of 3

   public TMPro.TextMeshProUGUI setScore;

   protected string scorePrefix = "";

   // Start is called before the first frame update
   void Start()
   {
       
   }

   // Update is called once per frame
   void Update()
   {
       
   }

   public virtual void SetActive(bool val)
   {
      setScore.gameObject.SetActive(val);
      gameObject.SetActive(val);
   }

   protected void SetScore() {
      setScore.text = scorePrefix + (sets.Count / 3);
   }

   public virtual void SetMsg(string msg)
   {
   }

   public virtual void Reset()
   {
      selectedCards.Clear();
      sets.Clear();
      SetScore();
   }

   public void SelectCard(CardObject card)
   {
      selectedCards.Add(card);
      if (selectedCards.Count == 3) {
         string err = SetCheck.ValidateSet(selectedCards);
         if (err == "") {
            err = "Set!";
            CollectSet(selectedCards);
            GameManager.instance.computerPlayer.ResetTimer();
         } else {
            err = "Not a set!\n" + err;
            if (sets.Count > 0) {
               List<Card> giveBack = new List<Card>();
               for (int i = 0; i < 3; i++) {
                  giveBack.Add(sets[0]);
                  sets.RemoveAt(0);
               }
               GameManager.instance.cardManager.ReshuffleCards(giveBack);
            }
         }
         foreach (CardObject selected in selectedCards) {
            selected.Release();
         }
         selectedCards.Clear();

         //update set msg
         SetMsg(err);

         //update score
         SetScore();
      }
   }

   public void DeselectCard(CardObject card)
   {
      //find card index, then remove
      int cardIdx = -1;
      int i = 0;
      foreach (CardObject selected in selectedCards) {
         if (selected.info.ToString() == card.info.ToString()) {
            cardIdx = i;
            break;
         }
         i += 1;
      }
      if (cardIdx != -1) {
         selectedCards.RemoveAt(cardIdx);
      }
   }

   protected void CollectSet(List<CardObject> cards)
   {
      foreach (CardObject card in cards) {
         card.TakeCard();
         sets.Add(card.info);
      }

      GameManager.instance.cardManager.SetCollected();
   }
}
