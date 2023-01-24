using System.Text.RegularExpressions;

namespace WebApi.Halper;

public enum CardType
{
    MasterCard, Visa, AmericanExpress, Discover, JCB
}
public class Card
{
    public static CardType FindType(string cardNumber)
    {
        //https://www.regular-expressions.info/creditcard.html
        if (Regex.Match(cardNumber, @"^4[0-9]{12}(?:[0-9]{3})?$").Success)
        {
            return CardType.Visa;
        }

        if (Regex.Match(cardNumber, @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$").Success)
        {
            return CardType.MasterCard;
        }

        if (Regex.Match(cardNumber, @"^3[47][0-9]{13}$").Success)
        {
            return CardType.AmericanExpress;
        }

        if (Regex.Match(cardNumber, @"^6(?:011|5[0-9]{2})[0-9]{12}$").Success)
        {
            return CardType.Discover;
        }

        if (Regex.Match(cardNumber, @"^(?:2131|1800|35\d{3})\d{11}$").Success)
        {
            return CardType.JCB;
        }

        throw new Exception("Unknown card.");
    }
}
/*
     //test validation
    //https://www.getcreditcardnumbers.com/
    Validate("4169773331987017");//visa
    Validate("4658958254583145");//visa
    Validate("4771320594033780");//visa

    Validate("5410710000901089");//mc
    Validate("5289675573349651");//mc
    Validate("5582128534772839");//mc

    Validate("349101032764066");//ae
    Validate("343042534582349");//ae
    Validate("371305972529535");//ae

    Validate("6011683204539909");//discover
    Validate("6011488563514596");//discover
    Validate("6011465836488204");//discover

    Validate("3529908921371639");//jcb
    Validate("3589295535870728");//jcb
    Validate("3569239206830557");//jcb
 */