using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour,IClickable
{
   private Vector2Int _position;
   private bool _isFull;
   private IClickable _clickableImplementation;

   public void SetIndex( Vector2Int position)
    {
        _position = position;
    }

    public Vector2Int GetIndex()
    {
       return _position;
    }

    public void SetIsFull(bool isFull)
    {
        _isFull = isFull;
    }
    
    public bool GetFull()
    {
        return _isFull;
    }

    public void OnClick()
    {
        SoldierController.Instance.MovePosition(_position);
    }
}