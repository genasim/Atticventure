using UnityEngine;
using UnityEngine.InputSystem;

public class ResetAllBindings : MonoBehaviour
{
    [SerializeField] private InputActionAsset asset;

    public void ResetBindings() {
        foreach (InputActionMap actionMaps in asset.actionMaps) {
            actionMaps.RemoveAllBindingOverrides();
        }
    }
}
