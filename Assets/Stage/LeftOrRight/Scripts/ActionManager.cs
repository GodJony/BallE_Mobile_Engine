using System;
using UnityEngine;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class ActionManager : MonoBehaviour
    {
        public static Action onGameStarted;
    
        public static Action onPlayerFirstMoved;
        public static Action onPlayerMoved;
        public static Action<int> onPlayerGetItem;
        public static Action onPlayerOut;
        public static Action onPlayerExploded;

        public static Action onCountdownEnded;
        public static Action onGameEnded;

    }
}
