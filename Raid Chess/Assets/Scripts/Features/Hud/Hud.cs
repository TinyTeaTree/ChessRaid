using System.Threading.Tasks;
using Agents;
using Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game
{
    public class Hud : BaseVisualFeature<HudVisual>, IHud, IAppLaunchAgent
    {
        public bool IsReady { get; private set; }
        public Camera HudCamera => _visual?.HudCamera;
        public Transform HudRoot => _visual?.HudRoot;

        public void SetCanvas(Canvas visualCanvas)
        {
            if (!IsReady)
            {
                Notebook.NoteError("Can't call Hud while its not ready");
                return;
            }

            visualCanvas.worldCamera = HudCamera;
            visualCanvas.transform.SetParent(HudRoot);
        }

        public async Task AppLaunch()
        {
            await SetupVisual();
        }

        public async Task SetupVisual()
        {
            await CreateVisual();
            Camera.main.GetUniversalAdditionalCameraData().cameraStack.Add(HudCamera);
            IsReady = true;
        }
    }
}