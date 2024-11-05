using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScoriaScenario : Scenario
{
    public bool MarineFlashbackDone = false;
    public bool ZekeFlashbackDone = false;

    public int PowerCellConnectionCount;
    public bool AreFuelLinesFixed;

    public bool IsScoriaWorking()
    {
        return PowerCellConnectionCount == 2
            && AreFuelLinesFixed;
    }

}
