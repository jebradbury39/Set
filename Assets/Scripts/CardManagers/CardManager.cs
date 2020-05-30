using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
   private List<Card> deck = new List<Card>();
   private List<Card> currentCards = new List<Card>();
   private List<GameObject> gameobjectCards = new List<GameObject>();

   public GameObject cardPrefab;
   public TextMesh deckCount;

   public int maxCards = 16;
   public float startX = -4.0f;
   public float endX = 4.0f;
   public float incX = 1.0f;

   public float startY = 0.0f;
   public float incY = 1.5f;

   private List<Card> hintSet = new List<Card>();

   public void SetActive(bool val)
   {
      gameObject.SetActive(val);
   }

   void SetDeckCount()
   {
      deckCount.text = "" + deck.Count;
   }

   public void GameOver()
   {
      //clear the rest of the cards
      deck.Clear();
      SetDeckCount();
      currentCards.Clear();
      ClearTable();
   }

   public void StartGame()
   {
      //reset deck and draw cards
      ResetDeck();
      DrawCards();
      SetDeckCount();
   }

   public List<Card> FindSet()
   {
      List<Card> found = new List<Card>();
      found.Add(null);
      found.Add(null);
      found.Add(null);

      /*
       Pick any two cards, then see if we have the third card we need
       */
      foreach (Card card1 in currentCards) {
         found[0] = card1;
         foreach (Card card2 in currentCards) {
            if (card2 == card1) {
               continue;
            }
            found[1] = card2;
            foreach (Card card3 in currentCards) {
               if (card3 == card2 || card3 == card1) {
                  continue;
               }
               found[2] = card3;
               //check if this is a set
               if (SetCheck.ValidateSet(found) == "") {
                  Debug.Log("At least one set is available: " + card1 + ", " + card2 + ", " + card3);
                  return found;
               }
            }
         }
      }

      Debug.Log("No sets left");
      found.Clear();
      return found;
   }

   public void ResetDeck()
   {
      deck.Clear();

      //create cards and shuffle deck
      foreach (Card.Shape shape in System.Enum.GetValues(typeof(Card.Shape))) {
         foreach (Card.Number number in System.Enum.GetValues(typeof(Card.Number))) {
            foreach (Card.Color color in System.Enum.GetValues(typeof(Card.Color))) {
               foreach (Card.Fill fill in System.Enum.GetValues(typeof(Card.Fill))) {
                  deck.Add(new Card(shape, number, color, fill));
               }
            }
         }
      }

      //now shuffle
      deck.ShuffleList();
      SetDeckCount();
   }

   public void ReshuffleCards(List<Card> cards) {
      foreach (Card card in cards) {
         deck.Add(card);
      }

      deck.ShuffleList();
      SetDeckCount();
   }

   public void TakeCard(Card card)
   {
      int cardIdx = -1;
      int i = 0;
      foreach (Card current in currentCards) {
         if (current.ToString() == card.ToString()) {
            cardIdx = i;
            break;
         }
         i += 1;
      }
      if (cardIdx != -1) {
         currentCards.RemoveAt(cardIdx);
      }
   }

   public void DrawCards()
   {
      //draw cards until we have the max or until deck is empty
      for (int i = currentCards.Count; i < maxCards && deck.Count > 0; i += 1) {
         currentCards.Add(deck[0]);
         deck.RemoveAt(0);
      }

      ArrangeCards();
      string tmp = "";
      foreach (Card card in currentCards) {
         tmp += card + ",";
      }
      SetCollected();
      SetDeckCount();
   }

   void ClearTable() {
      foreach (GameObject cardObj in gameobjectCards) {
         cardObj.GetComponent<CardObject>().SetActive(false);
      }
      gameobjectCards.Clear();
   }

   void ArrangeCards()
   {
      //Lay out the current cards in a grid

      //clear table
      ClearTable();

      //reset card game objects
      
      for (float y = startY; gameobjectCards.Count < currentCards.Count; y += incY) {
         for (float x = startX; x < endX && gameobjectCards.Count < currentCards.Count; x += incX) {
            GameObject clone = ObjectPool.instance.GetPooledObject();
            clone.GetComponent<CardObject>().info = currentCards[gameobjectCards.Count];
            clone.transform.position = new Vector3(x, y, 0);
            clone.GetComponent<CardObject>().SetActive(true);
            gameobjectCards.Add(clone);
         }
      }
   }

   public void SetCollected()
   {
      //check if we can stil get a set
      hintSet = FindSet();
      if (hintSet.Count == 0) {
         if (deck.Count == 0) {
            GameManager.instance.GameOver();
         } else {
            DrawCards();
         }
      }
   }

   public List<CardObject> GetHintSetObj()
   {
      List<CardObject> hintobj = new List<CardObject>();
      foreach (Card card in hintSet) {
         foreach (GameObject obj in gameobjectCards) {
            if (obj.GetComponent<CardObject>().info.ToString() == card.ToString()) {
               hintobj.Add(obj.GetComponent<CardObject>());
            }
         }
      }
      return hintobj;
   }

   public void HighlightHintSet()
   {
      if (hintSet.Count == 0) {
         return;
      }

      //find each card's gameobject and trigger the hint hightlight
      foreach (CardObject card in GetHintSetObj()) {
         card.SetHintHighlight(true);
      }
   }
}
