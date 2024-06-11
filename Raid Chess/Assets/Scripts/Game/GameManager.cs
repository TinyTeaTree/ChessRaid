using UnityEngine;

namespace ChessRaid
{
    public class GameManager : BaseContext
    {
        protected override void CreateControllers()
        {
            _controllerGroup.Add(DataWarehouse._);
            _controllerGroup.Add(TurnModel._);
            _controllerGroup.Add(RulesManager._);
            _controllerGroup.Add(TurnManager._);
        }

        protected override void PostStart()
        {
            GridManager._.SetUp();
            Squad._.SetUp();
        }
    }
}