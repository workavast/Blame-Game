using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Bestiary
{
    public class BestiaryInputReader : MonoBehaviour
    {
        [SerializeField] private BestiaryManager bestiaryManager;
        [SerializeField] private InputActionReference nextBtn;
        [SerializeField] private InputActionReference prevBtn;

        private void Awake()
        {
            nextBtn.action.Enable();
            prevBtn.action.Enable();
            
            nextBtn.action.performed += NextModel;
            prevBtn.action.performed += PrevModel;
        }
        
        private void OnDestroy()
        {
            nextBtn.action.performed -= NextModel;
            prevBtn.action.performed -= PrevModel;
            
            nextBtn.action.Disable();
            prevBtn.action.Disable();
        }

        private void NextModel(InputAction.CallbackContext obj) 
            => bestiaryManager.NextModel();
        
        private void PrevModel(InputAction.CallbackContext obj) 
            => bestiaryManager.PrevModel();
    }
}