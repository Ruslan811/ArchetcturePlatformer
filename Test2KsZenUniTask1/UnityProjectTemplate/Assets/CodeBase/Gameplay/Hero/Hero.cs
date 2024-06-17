using UnityEngine;
using Zenject;

namespace Codebase.Gameplay.Hero
{
    public class Hero : MonoBehaviour, IHero
    {
        public class Factory : PlaceholderFactory<Hero>
        {

        }
    }

    public interface IHero
    {

    }
}