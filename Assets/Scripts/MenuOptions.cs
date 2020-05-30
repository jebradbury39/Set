public static class MenuOptions
{
   public enum Difficulty {
      INVALID,
      EASY,
      MEDIUM,
      HARD
   }

   public enum GameMode {
      INVALID,
      SOLO,
      VS
   }

   public static Difficulty difficulty = Difficulty.EASY;
   public static GameMode gamemode = GameMode.SOLO;

   public static void Reset()
   {
      difficulty = Difficulty.EASY;
      gamemode = GameMode.SOLO;
   }
}
