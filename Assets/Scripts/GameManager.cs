using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

   public SoloCardManager soloCardManager;
   public VsCardManager vsCardManager;

   public CardManager cardManager = null;
   public HumanPlayer humanPlayer;
   public ComputerPlayer computerPlayer;
   public TMPro.TextMeshProUGUI gameOver;

   private bool start = false;
   
   void Awake()
   {
      instance = this;

      Debug.Log(MenuOptions.difficulty + ", " + MenuOptions.gamemode);
      computerPlayer.SetActive(false);
      soloCardManager.SetActive(false);
      vsCardManager.SetActive(false);

      switch (MenuOptions.gamemode) {
      case MenuOptions.GameMode.SOLO:
         cardManager = soloCardManager;
         break;
      case MenuOptions.GameMode.VS:
         cardManager = vsCardManager;
         vsCardManager.ApplyDifficulty(MenuOptions.difficulty);
         computerPlayer.SetActive(true);
         break;
      default:
         break;
      }
      cardManager.SetActive(true);
   }

   // Start is called before the first frame update
   void Start()
   {
      if (!start) {
         start = true;
         StartGame();
      }
   }

   // Update is called once per frame
   void Update()
   {
       
   }

   public void QuitGame() {
      Application.Quit();
   }

   public void GameOver()
   {
      //clear cards
      cardManager.GameOver();

      //Display message
      gameOver.gameObject.SetActive(true);
   }

   public void StartGame()
   {
      //hide game over message
      gameOver.gameObject.SetActive(false);

      //reset players
      humanPlayer.Reset();
      computerPlayer.Reset();

      //reset deck and draw cards
      cardManager.StartGame();
   }

   public void HighlightHintSet()
   {
      cardManager.HighlightHintSet();
   }

   public void GoToMenu()
   {
      MenuOptions.Reset();
      SceneManager.LoadScene("Menu");
   }
}
