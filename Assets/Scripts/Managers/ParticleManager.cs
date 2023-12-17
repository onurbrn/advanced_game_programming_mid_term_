using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ParticleManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Vars
        
        #endregion
        
        #region Serializefield Variables
       
        [SerializeField] private List<ParticleEmitController> emitControllers;
        
        #endregion

        #region Private Vars
        
        #endregion
        
        #endregion

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            ParticleSignals.Instance.onPlayerDeath += OnParticleDeath;
            ParticleSignals.Instance.onParticleBurst += OnParticalBurst;
            ParticleSignals.Instance.onParticleStop += OnParticalStop;
        } 
        private void UnsubscribeEvents()
        {
            ParticleSignals.Instance.onPlayerDeath -= OnParticleDeath;
            ParticleSignals.Instance.onParticleBurst -= OnParticalBurst;
            ParticleSignals.Instance.onParticleStop -= OnParticalStop;
        } 
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private async void OnParticalBurst(Vector3 transform)
        {
            Vector3 newTransform = new Vector3(Random.Range(transform.x - 1.5f, transform.x + 1.5f),transform.y,transform.z);
            
            emitControllers[0].EmitParticle(newTransform);
            await Task.Delay(100);
            emitControllers[1].EmitParticle(newTransform + new Vector3(0,4,3));
        }
        private void OnParticleDeath(Vector3 collectedTransform)
        {
            emitControllers[1].EmitParticle(collectedTransform);
        }
        private void OnParticalStop()
        {
            for (int i = 0; i < emitControllers.Count; i++)
            {
                emitControllers[i].EmitStop();
            }
        }
    }
}