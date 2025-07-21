using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

using ZL.Unity.Directing;

using ZL.Unity.UI;

namespace ZL.Unity.Vigil
{
    [AddComponentMenu("")]

    [DisallowMultipleComponent]

    public sealed class MainSceneDirector : SceneDirector
    {
        [Space]

        [SerializeField]

        private Transform player;

        [SerializeField]

        private Transform playerSpawnPoint;

        [Space]

        [SerializeField]

        private DigitalClock digitalClock1;

        [SerializeField]

        private DigitalClock digitalClock2;

        [Space]

        [SerializeField]

        private AnomalyManager anomalyManager;

        private int level = 0;

        private const int levelMax = 6;

        private const int minutePerLevel = 60 / levelMax;

        protected override IEnumerator Start()
        {
            anomalyManager.OccurRandomAnomaly();

            yield return base.Start();
        }

        public void LoadNextLevel(bool isThereAnomaly)
        {
            if (loadNextLevelRoutine != null)
            {
                return;
            }

            loadNextLevelRoutine = LoadNextLevelRoutine(isThereAnomaly);

            StartCoroutine(loadNextLevelRoutine);
        }

        private IEnumerator loadNextLevelRoutine = null;

        private IEnumerator LoadNextLevelRoutine(bool isThereAnomaly)
        {
            FadeOut();

            yield return WaitForSecondsCache.Get(fadeDuration);

            if (anomalyManager.GuessAnomaly(isThereAnomaly) == true)
            {
                if (++level == levelMax)
                {
                    GameClear();

                    yield break;
                }
            }

            else
            {
                level = 0;
            }

            player.SetPositionAndRotation(playerSpawnPoint);

            digitalClock1.Minute = minutePerLevel * level;

            digitalClock2.Minute = minutePerLevel * level;

            anomalyManager.DisableCurrentAnomaly();

            yield return Start();

            loadNextLevelRoutine = null;
        }

        public void GameClear()
        {
            FixedDebug.Log("Game Clear");
        }

        public void GameOver()
        {
            FixedDebug.Log("Game Over");
        }

        
    }
}