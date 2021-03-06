﻿using System;
using Boo.Lang;
using JetBrains.Annotations;
using Planets;
using QuadTree;

public sealed class Camera : ICamera, IAABBox
{
    private float mScale = 1f;
    [NotNull]
    private readonly List<int> mZoomValues = new List<int>(){5, 10, 100, 1000, 10000};
    private int mZoomInd = 0;

    private int mTop;
    private int mBottom;
    private int mLeft;
    private int mRight;

    private int mPlayerX;
    private int mPlayerY;
    [NotNull]
    private readonly List<ICameraListener> mListeners = new List<ICameraListener>();
    [NotNull]
    private readonly List<IBordersChangeListener> mBorderListeners = new List<IBordersChangeListener>();
    
    public Camera()
    {
        mTop = 50;
        mBottom = -50;
        mLeft = -50;
        mRight = 50;
    }

    public void IncreaseZoom()
    {
        ++mZoomInd;
        if (mZoomInd >= mZoomValues.Count)
        {
            mZoomInd = mZoomValues.Count - 1;
            return;
        }

        UpdateListeners();
        UpdatePositions();
    }

    public void DecreaseZoom()
    {
        --mZoomInd;
        if (mZoomInd < 0)
        {
            mZoomInd = 0;
            return;
        }
        UpdateListeners();
        UpdatePositions();
    }
    private void UpdateListeners()
    {
        int zoomValue = mZoomValues[mZoomInd];
        for (int i = 0; i < mListeners.Count; ++i)
        {
            // ReSharper disable once PossibleNullReferenceException
            mListeners[i].ZoomValueChanged(zoomValue);
        }
    }

    public int GetMaxZoom()
    {
        return mZoomValues[mZoomValues.Count - 1];
    }

    public int GetMinZoom()
    {
        return mZoomValues[0];
    }

    public int GetZoom()
    {
        return mZoomValues[mZoomInd];
    }

    public void AddListener(ICameraListener listener)
    {
        mListeners.Add(listener);
    }

    public void AddBorderChangeListener(IBordersChangeListener listener)
    {
        mBorderListeners.Add(listener);
    }


    public void PositionCanged(int posX, int posY)
    {
        mPlayerX = posX;
        mPlayerY = posY;

        UpdatePositions();
            
    }

    private void UpdatePositions()
    {
        var halfWidth = GetZoom() / 2;
        if (GetZoom() % 2 == 0)
        {
            mTop = mPlayerY + halfWidth;
            mBottom = mPlayerY - halfWidth + 1;
            mLeft = mPlayerX - halfWidth + 1;
            mRight = mPlayerX + halfWidth;
        }
        else
        {
            mTop = mPlayerY + halfWidth;
            mBottom = mPlayerY - halfWidth;
            mLeft = mPlayerX - halfWidth;
            mRight = mPlayerX + halfWidth;
        }

        for (int i = 0; i < mBorderListeners.Count; ++i)
        {
            // ReSharper disable once PossibleNullReferenceException
            mBorderListeners[i].NewBorders(mTop, mBottom, mLeft, mRight);
        }
    }

    public float GetX()
    {
        return mLeft + GetWidth() / 2f;
    }

    public float GetY()
    {
        return mBottom + GetHeight() / 2f;
    }

    public float GetWidth()
    {
        return mRight - mLeft;
    }

    public float GetHeight()
    {
        return mTop - mBottom;
    }

    public bool IsIntersect(IAABBox other)
    {
        return (Math.Abs(GetX() - other.GetX()) * 2 < (GetWidth() + other.GetWidth())) &&
               (Math.Abs(GetY() - other.GetY()) * 2 < ( + other.GetHeight()));
    }
}