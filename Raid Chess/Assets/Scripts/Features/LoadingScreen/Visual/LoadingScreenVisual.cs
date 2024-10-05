using System;
using Core;
using UnityEngine;

namespace Game
{
    public class LoadingScreenVisual : BaseVisual<LoadingScreen>
    {
        [SerializeField] private Canvas _canvas;
        public Canvas Canvas => _canvas;

        private LoadingScreenPage _currentPage;

         

        public void Close()
        {
            Destroy(_currentPage.gameObject);
        }

        public void UpdateProgress()
        {

        }

        //internal void ShowWarmUp()
        //{
        //    throw new NotImplementedException();
        //}

        internal void ShowPage(LoadingScreenPage page)
        {
            _currentPage = Instantiate(page, _canvas.transform);
        }
    }
}