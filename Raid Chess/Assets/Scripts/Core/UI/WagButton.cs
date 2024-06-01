using UnityEngine;
using UnityEngine.EventSystems;

public class WagButton : UnityEngine.UI.Button
{
    [SerializeField] public RegularSound _clickSound;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if(_clickSound != null)
        {
            WagAudioManager._.Play(_clickSound);
        }
    }
}
