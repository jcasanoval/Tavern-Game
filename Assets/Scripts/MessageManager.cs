using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
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
                "Buenas, sobrino, hace tiempo que no te veía. Te he dicho que vinieras porque quería dejarte el negocio, esta taberna será tuya. ",
                "Déjame explicarte cómo funciona: en el día deberás comprar el inventario, y en la noche comienza la fiesta."
            )
        },
        {
            TutorialStep.ExplainDoorInteraction, 
            string.Join(Environment.NewLine,
                "Antes que nada, déjame decirte que cada vez que abras la puerta, se hará de noche.", 
                "Un mago una vez maldijo la taberna, por lo que siempre que abras la puerta el cielo se oscurece, y una vez eso ocurre, todos se irán a dormir y no podrás comprar bebidas."
            )
        },
        {
            TutorialStep.ExplainTemper, 
            string.Join(Environment.NewLine,
                "Supongo que lo entiendes, ahora regresemos al servicio. En la noche es cuando la gente comienza a llegar, ya sabes, cuando se abre el bar y la fiesta comienza. ",
                "Una vez esto ocurra, deberás estar atento a los estados de tus clientes. Ellos comenzarán a enojarse si tardas en darle una bebida, trata de que no se vayan enojados. El poder de una mala o buena reseña puede llevarte lejos o hundirte."
            )
        },
        {
            TutorialStep.CustomerIsComing,
            string.Join(Environment.NewLine,
                "Le he pedido a un amigo que venga así aprendes a servir. ",
                "Justo está llegando."
            )
        },
        {
            TutorialStep.ExplainMovement, 
            string.Join(Environment.NewLine,
                "Bien, muévete con las teclas WASD hasta la barra. ",
                "Allí podrás retirar la cerveza, y para comprar más deberás interactuar con el barril."
            )
        },
        {
            TutorialStep.TakeBeerToCustomer,
            string.Join(Environment.NewLine,
                "Ahora llévale la cerveza."
            )
        },
        {
            TutorialStep.WaitForCustomerToLeave,
            string.Join(Environment.NewLine,
                "Bien hecho!"
            )
        },
        {
            TutorialStep.Farewell, 
            string.Join(Environment.NewLine,
                "Bueno, espero que puedas hacerte con una taberna cada vez más grande y que este lugar vuelva a ver los días de gloria que alguna vez tuvo.",
                "Hasta tuve que pedirle una mano a unos amigos para poder servir todo antes que los clientes se enfadaran y rompieran el bar. ¡Qué buenos tiempos!",
                "Bueno, yo te dejaré a ti la taberna, debo ir a ver a mi esposa, hace como 30 años que me dice que no paso un solo día con ella..."
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
