using CodeBase.UI.Money;
using UnityEngine;
using Zenject;

public class Money : MonoBehaviour, IMoney
{
    public class Factory : PlaceholderFactory<Money>
    {

    }
}
