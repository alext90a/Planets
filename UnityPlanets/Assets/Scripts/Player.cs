﻿using Boo.Lang;
using JetBrains.Annotations;

public sealed class Player : IPlayer
{
    private int mX = 0;
    private int mY = 0;
    [NotNull]
    private List<IPlayerListener> mPlayerListeners = new List<IPlayerListener>();
    [NotNull]
    private readonly IConstants mConstants;

    public Player([NotNull] IConstants constants)
    {
        mConstants = constants;
    }

    public int Score => mConstants.GetPlayerScore();

    public void MoveLeft()
    {
        mX -= 1;
        UpdateListeners();
    }

    public void MoveRight()
    {
        mX += 1;
        UpdateListeners();
    }

    public void MoveTop()
    {
        mY += 1;
        UpdateListeners();
    }

    public void MoveBottom()
    {
        mY -= 1;
        UpdateListeners();
    }


    public void AddListener(IPlayerListener listener)
    {
        mPlayerListeners.Add(listener);
    }

    public int GetX()
    {
        return mX;
    }

    public int GetY()
    {
        return mY;
    }

    private void UpdateListeners()
    {
        for (int i = 0; i < mPlayerListeners.Count; ++i)
        {
            // ReSharper disable once PossibleNullReferenceException
            mPlayerListeners[i].PositionCanged(mX, mY);
        }
    }
}