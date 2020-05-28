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

   void Awake() {
      instance = this;
   }

   // Start is called before the first frame update
   void Start()
   {
      ResetDeck();
      DrawCards();
   }

   // Update is called once per frame
   void Update()
   {
       
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

   public void DrawCards()
   {
      //draw cards until we have the max or until deck is empty
      for (int i = currentCards.Count; i < maxCards && deck.Count > 0; i += 1) {
         currentCards.Add(deck[0]);
         deck.RemoveAt(0);
      }

      ArrangeCards();
   }

   void ArrangeCards()
   {
      //Lay out the current cards in a grid

      //clear table
      foreach (GameObject cardObj in gameobjectCards) {
         cardObj.SetActive(false);
      }
      gameobjectCards.Clear();

      //reset card game objects
      
      for (float y = startY; gameobjectCards.Count < currentCards.Count; y += incY) {
         for (float x = startX; x < endX && gameobjectCards.Count < currentCards.Count; x += incX) {
            GameObject clone = ObjectPool.instance.GetPooledObject();
            clone.GetComponent<CardObject>().info = currentCards[gameobjectCards.Count];
            clone.transform.position = new Vector3(x, y, 0);
            clone.SetActive(true);
            gameobjectCards.Add(clone);
         }
      }
   }
}
