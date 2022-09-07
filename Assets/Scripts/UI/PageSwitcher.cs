using UnityEngine;

namespace Menu {
    public class PageSwitcher : MonoBehaviour {

        public PageController pageController;

#if UNITY_EDITOR
    public void SwitchToInGameUI() {
        pageController.TurnPageOff(PageType.Settings, PageType.InGame);
    }

    public void SwitchToSettingsUI() {
        pageController.TurnPageOff(PageType.InGame, PageType.Settings);
    }

#endif

    }

}
