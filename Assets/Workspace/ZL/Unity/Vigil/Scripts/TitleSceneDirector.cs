using System.Collections;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Directing;

using ZL.Unity.IO;

namespace ZL.Unity.Vigil
{
    [AddComponentMenu("")]

    [DisallowMultipleComponent]

    public sealed class TitleSceneDirector : SceneDirector
    {
        [Space]

        [SerializeField]

        private Button startGameButton;

        [Space]

        [SerializeField]

        private BoolPref skipTutorialFlagPref = new("Tutorial Completed", false);

        protected override void Awake()
        {
            base.Awake();

            skipTutorialFlagPref.TryLoadValue();
        }

        protected override IEnumerator Start()
        {
            startGameButton.interactable = skipTutorialFlagPref.Value;

            yield return base.Start();
        }

        public override void LoadScene(string sceneName)
        {
            if (sceneName == "Tutorial Scene")
            {
                skipTutorialFlagPref.SaveValue(true);
            }

            base.LoadScene(sceneName);
        }
    }
}