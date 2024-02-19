using CommunityToolkit.Mvvm.Messaging.Messages;

namespace E_Citera_MAUI.Messages;

public class AddItemMessage : ValueChangedMessage<Title>
{
    public AddItemMessage(Title title) : base(title)
    { 
    }
}
