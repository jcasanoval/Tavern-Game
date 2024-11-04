using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using System;

public class MessageManager : MonoBehaviour
{
    [SerializeField]
    private ChatBubble chatBubble;

    private Dictionary<TutorialStep, string> tutorialDialogs = new Dictionary<TutorialStep, string>()
    {
        {
            TutorialStep.Introduction, 
            string.Join(Environment.NewLine,
                "Que andá ñery, soy tu tío.",
                "Encargate vos ahora pibe.",
                "Suerte en pila.",
                "Compras cerveza presionando E en el barril, pero de noche no podés comprar nada gato.", 
                "Presiona E para continuar."
            )
        },
        {
            TutorialStep.ExplainDoorInteraction, 
            string.Join(Environment.NewLine,
                "De noche no podés comprar nada, tan todos durmiendo boludo.",
                "La puerta queda abierta sólo de noche, al abrirla no podés comprar más."
            )
        },
        {
            TutorialStep.CustomerIsComing,
            string.Join(Environment.NewLine,
                "Ahí viene el drogadicto de mi amigo para que veas como funca y eso."
            )
        },
        {
            TutorialStep.ExplainTemper, 
            string.Join(Environment.NewLine,
                "Cada cliente tiene su temperamento.",
                "Bla bla bla."
            )
        },
        {
            TutorialStep.ExplainMovement, 
            string.Join(Environment.NewLine,
                "Te movés con WASD capo.",
                "Andá al barril a agarrar una chela."
            )
        },
        {
            TutorialStep.TakeBeerToCustomer,
            string.Join(Environment.NewLine,
                "Dale la cerveza al pibe bo, no es tan difícil."
            )
        },
        {
            TutorialStep.WaitForCustomerToLeave,
            string.Join(Environment.NewLine,
                "Joya, ahora esperá que termine de tomar y se vaya."
            )
        },
        {
            TutorialStep.Farewell, 
            string.Join(Environment.NewLine,
                "Ta pelao, encargate vos ahora."
            )
        },
    };

    public void ShowMessage(TutorialStep step)
    {
        if (tutorialDialogs.TryGetValue(step, out string message))
        {
            chatBubble.SetText(message);
        }
    }
}
