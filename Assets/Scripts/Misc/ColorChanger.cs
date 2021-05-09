using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorChanger : MonoBehaviour
{
    public Material selectMaterial = null;

    [SerializeField] private Timer timer;

    private MeshRenderer meshRenderer = null;
    private XRBaseInteractable interactable = null;
    private Material originalMaterial = null;

    [SerializeField]private AudioSource src;

    [SerializeField]private AudioClip SFX;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.onHoverEnter.AddListener(SetSelectMaterial);
        interactable.onHoverExit.AddListener(SetOriginalMaterial);
    }


    private void OnDestroy()
    {
        interactable.onHoverEnter.RemoveListener(SetSelectMaterial);
        interactable.onHoverExit.RemoveListener(SetOriginalMaterial);
    }

    private void SetSelectMaterial(XRBaseInteractor interactor)
    {
        //Pressed
        GameObject.Find("Vr Rig").SendMessage("Finish");
        src.PlayOneShot(SFX);
        meshRenderer.material = selectMaterial;
    }

    private void SetOriginalMaterial(XRBaseInteractor interactor)
    {
        //Depressed
        meshRenderer.material = originalMaterial;
    }
}