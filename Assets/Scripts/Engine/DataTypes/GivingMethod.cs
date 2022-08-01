namespace Engine
{
    public enum GivingMethod
    { 
        Any, 
        InternallyPotion,
        InternallyEntire,
        ExternallyRubbing,
        ExternallyCream,
        Inhalation,
        Internaly = InternallyPotion | InternallyEntire,
        Externally = ExternallyRubbing | ExternallyCream | Inhalation
    };
}