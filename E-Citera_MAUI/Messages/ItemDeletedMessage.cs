using CommunityToolkit.Mvvm.Messaging.Messages;

namespace E_Citera_MAUI.Messages
{
    public class ItemDeletedMessage : ValueChangedMessage<Title>
    {
        public ItemDeletedMessage(Title title) : base(title) { }
    }
}
