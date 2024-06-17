using CodeBase.Data;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.SaveLoadService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Gameplay.Hero
{
    public class HeroPositionSaver : MonoBehaviour, IProgressSaver
    {
        private void Start()
        {
            //transform.position = 
        }

        public void LoadProgress(PlayerProgress progress)
        {

            if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;

            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            if (savedPosition != null)
                Warp(to: savedPosition);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        private static string CurrentLevel() =>
      SceneManager.GetActiveScene().name;

        private void Warp(Vector3Data to)
        {
            transform.position = to.AsUnityVector();
        }
    }
}