using System.Collections;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LoadingScreenPage : MonoBehaviour
    {
        [SerializeField] private Image _fillBar;
        [SerializeField] private TMP_Text _fillText;
        [SerializeField] private TMP_Text _tipText;
        [SerializeField] private bool _activateProgressLoadingText;

        private Coroutine _progressRoutine;

        private void Awake()
        {
            if(_activateProgressLoadingText && _fillText != null)
            {
                _progressRoutine = StartCoroutine(ProgressRoutine());
            }
        }

        private void SetProgress(float progress, string progressText)
        {
            if (_progressRoutine != null)
            {
                StopCoroutine(_progressRoutine);
                _progressRoutine = null;
            }

            if (progressText.HasContent())
            {
                if (_fillText)
                {
                    _fillText.text = progressText;
                }
            }

            if (_fillBar)
            {
                _fillBar.fillAmount = progress;
            }
        }

        IEnumerator ProgressRoutine()
        {
            string[] labels = new[] { $"Loading", $"Loading .", $"Loading ..", $"Loading ..." };
            int index = 0;

            while (true)
            {
                _fillText.text = labels[index];

                yield return new WaitForSeconds(1.23f);

                index++;
                if(index >= labels.Length)
                {
                    index = 0;
                }
            }
        }
    }
}