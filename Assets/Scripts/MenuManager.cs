using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
   public static MenuManager instance;

   void Awake()
   {
      instance = this;
   }

   // Start is called before the first frame update
   void Start()
   {
       MenuOptions.Reset();
   }

   // Update is called once per frame
   void Update()
   {
       
   }

   public void StartGame()
   {
      //options are already set
      

      //switch game scene
      SceneManager.LoadScene("Game");
   }
}
