using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Recepteur : MonoBehaviour
{
    public Door Door;
    public TypeObject typeObjectAccepted;
    public Transform placementPosition; // Ajout d'une position spécifique
    private bool isOccupied = false; //  Vérifie si un objet est déjà placé

    private void OnTriggerEnter(Collider other)
    {
        print("triggerENTER");
        objetSpecial obj = other.GetComponent<objetSpecial>();

        if (obj != null && obj.getTypeObject() == typeObjectAccepted && !isOccupied)
        {
            print("Objet correct détecté, placement en cours...");
            PlaceObject(obj);
            Door.addArecepteurCompleted();
            isOccupied = true; //  Empêche d'accepter d'autres objets
        }
    }

    private void PlaceObject(objetSpecial obj)
    {


        // Place l’objet à la position définie
        obj.transform.position = placementPosition.position;
        obj.transform.rotation = placementPosition.rotation;

        // Désactive le Rigidbody pour empêcher le mouvement
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.useGravity = false;
        if (rb != null)
        {
            rb.useGravity = false;
            rb.angularVelocity = Vector3.zero;
        }

        //// Désactive la possibilité de prendre l’objet (si tu as un système de grab)
        //Collider objCollider = obj.GetComponent<Collider>();
        //if (objCollider != null)
        //{
        //    objCollider.enabled = false;
        //}

        // Désactive le script de déplacement de l’objet
        obj.GetComponent<XRGrabInteractable>().enabled = false;
    }
}
