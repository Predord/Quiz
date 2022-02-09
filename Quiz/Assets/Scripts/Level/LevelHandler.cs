using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Quiz.UI;

namespace Quiz.Level
{
    public class LevelHandler : MonoBehaviour, IGridData, ILevel
    {
        public event Action OnLevelChange;
        public event Action OnNewGridCreate;

        [SerializeField]
        private RectTransform restartPanel;

        [SerializeField]
        private RectTransform loadingPanel;

        private IFade loadingPanelFade;
        private Coroutine restartGame;

        public int RowCount
        {
            get
            {
                return levelBundleData.LevelDatas[CurrentLevel].RowCount;
            }
        }

        public int ColumnCount
        {
            get
            {
                return levelBundleData.LevelDatas[CurrentLevel].ColumnCount;
            }
        }

        public int CurrentLevel
        {
            get
            {
                return currentLevel;
            }
            private set
            {
                currentLevel = value;

                if (currentLevel >= levelBundleData.LevelDatas.Length)
                {
                    restartPanel.gameObject.SetActive(true);
                }
                else
                {
                    DOTween.KillAll();

                    OnLevelChange?.Invoke();
                    OnNewGridCreate?.Invoke();
                }
            }
        }

        private int currentLevel;

        [SerializeField]
        private LevelBundleData levelBundleData;

        private void Awake()
        {
            loadingPanelFade = loadingPanel.GetComponent<IFade>();
        }

        public void ChangeLevel()
        {
            CurrentLevel++;
        }

        public void RestartGame()
        {
            if (restartGame == null)
            {
                loadingPanel.gameObject.SetActive(true);
                loadingPanelFade.FadeIn();

                restartGame = StartCoroutine(RestartGame(loadingPanelFade.FadeDuration));
            }
        }

        private IEnumerator RestartGame(float waitDuration)
        {
            yield return new WaitForSeconds(waitDuration);

            restartPanel.gameObject.SetActive(false);

            CurrentLevel = 0;

            loadingPanelFade.FadeOut();

            yield return new WaitForSeconds(waitDuration);

            loadingPanel.gameObject.SetActive(false);

            restartGame = null;
        }
    }
}
