using UnityEngine;
using System.Collections;
using System;

public class BoardEventHub : Singleton<BoardEventHub>
{

    #region events
    public event Action onBoardGenerated;
    public void BoardGenerated()
    {
        if (onBoardGenerated != null)
        {
            onBoardGenerated();
        }
    }

    public event Action onBoardReady;
    public void BoardReady()
    {
        if (onBoardReady != null)
        {
            onBoardReady();
        }
    }
    #endregion
}
