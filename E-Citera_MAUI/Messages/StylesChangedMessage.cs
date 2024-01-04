using CommunityToolkit.Mvvm.Messaging.Messages;

namespace E_Citera_MAUI.Messages;

public class StylesChangedMessage : ValueChangedMessage<bool>
{
    public StylesChangedMessage(bool value) : base(value)
    { 
    }
}
