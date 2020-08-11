using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInteractionsMenu : Menu {

    public override void ExtraFunctionalityOnCLose() {
        if (menuPosition == MenuManager.MenuState.FirstPanel && !TowerManager.tm_Single.selectedTowerIsMoving) {
            TowerManager.tm_Single.selectedTower = null;
        }
    }
}
