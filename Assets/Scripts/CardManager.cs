using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
   public static CardManager instance;

   private List<Card> deck = new List<Card>();
   private List<Card> currentCards = new List<Card>();
   private List<GameObject> gameobjectCards = new List<GameObject>();

   public GameObject cardPrefab;

   public int maxCards = 16;
   public float startX = -4.0f;
   public float endX = 4.0f;
   public float incX = 1.0f;

   public float startY = 1.0f;
   public float incY = 2.0f;

   private bool start = false;

   void Awake() {
      instance = this;
   }

   // Start is called before the first frame update
   void Start()
   {
   }

   // Update is called once per frame
   void Update()
   {
      if (!start) {
         start = true;
         ResetDeck();
         DrawCards();
      }
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
               if (SetCheck.ValidateSet(found)) {
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
   }

   public void ReshuffleCards(List<Card> cards) {
      foreach (Card card in cards) {
         deck.Add(card);
      }

      deck.ShuffleList();

      Debug.Log("deck size after reshuffle: " + deck.Count);
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
      Debug.Log("deck size after draw: " + deck.Count);
      string tmp = "";
      foreach (Card card in currentCards) {
         tmp += card + ",";
      }
      Debug.Log("current cards: " + tmp);
   }

   void ArrangeCards()
   {
      //Lay out the current cards in a grid

      //clear table
      foreach (GameObject cardObj in gameobjectCards) {
         cardObj.GetComponent<CardObject>().SetActive(false);
      }
      gameobjectCards.Clear();

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
}
