using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class EventBroker
    {

        public static event Action<int, Vector2> MakeBlackHole;

        public static void CallMakeBlackHole(int id, Vector2 position)
        {
            MakeBlackHole?.Invoke(id, position);
        }

        public static event Action<int> DestroyBlackHole;

        public static void CallDestroyBlackHole(int id)
        {
            DestroyBlackHole?.Invoke(id);
        }

        public static event Action DestroySpaceShip;

        public static void CallDestroySpaceShip()
        {
            DestroySpaceShip?.Invoke();
        }

        public static event Action PauseGame;

        public static void CallPauseGame()
        {
            PauseGame?.Invoke();
        }

        public static event Action UnPauseGame;

        public static void CallUnPauseGame()
        {
            UnPauseGame?.Invoke();
        }

        public static event Action CollectStar;

        public static void CallCollectStar()
        {
            CollectStar?.Invoke();
        }

        public static event Action<Vector2> WinGame;

        public static void CallWinGame(Vector2 endPos)
        {
            WinGame?.Invoke(endPos);
        }

        public static event Action<string> StopTimeCounter;

        public static void CallStopTimeCounter(string endTime)
        {
            StopTimeCounter?.Invoke(endTime);
        }

        public static event Action LoseGame;

        public static void CallLoseGame()
        {
            LoseGame?.Invoke();
        }
    }
}