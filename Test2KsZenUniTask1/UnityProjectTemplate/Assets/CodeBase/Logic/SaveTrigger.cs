using CodeBase.Infrastructure;
using CodeBase.Services.SaveLoadService;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
  public class SaveTrigger : MonoBehaviour
  {
        

    public BoxCollider Collider;

    private void Awake()
    {
      //_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }
        
       /* private void Construct(SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }*/

    private void OnTriggerEnter(Collider other)
    {
      //_saveLoad.SaveProgress();
      Debug.Log("Progress saved!");
      gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
      if(!Collider) return;
      
      Gizmos.color = new Color32(30, 200, 30, 130);
      Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
    }
  }

   
}
namespace CodeBase.Infrastructure
{
    public class SaveLoadSingle
    {
        private ISaveLoadService _saveLoadService;

        public SaveLoadSingle(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void SaveProgress()
        {
            _saveLoadService.SaveProgress();
        }
    }
}