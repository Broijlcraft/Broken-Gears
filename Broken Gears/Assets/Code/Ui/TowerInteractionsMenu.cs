using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInteractionsMenu : Menu {

    private TowerManager tManager;

    private void Start() {
        tManager = TowerManager.singleTM;
    }

    public override void ExtraFunctionalityOnCLose() {
        if (menuPosition == MenuState.FirstPanel && !tManager.GetSelectedTowerIsMoving()) {
            tManager.SetSelectedTower(null);
        }
    }
}
