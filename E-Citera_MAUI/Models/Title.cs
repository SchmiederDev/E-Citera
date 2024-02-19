using System.Collections.ObjectModel;

namespace E_Citera_MAUI.Models;

public partial class Title : ObservableObject
{
    public int Title_ID { get; set; }

    public ObservableCollection<Author> Authors { get; set; }

    public string ItemTitle { get; set; }
    public string ItemType { get; set; }

    public ObservableCollection<Author> Editors { get; set; }

    public int SeriesID { get; set; }
    public string SeriesTitle { get; set; }

    // Following field could have also been a number (int)
    // but to allow roman ciphers and the like 'string' was chosen as datatype.
    public string Volume {  get; set; }

    // Following field could have also been a number (int)
    // but to allow roman ciphers and the like 'string' was chosen as datatype.
    public string Issue { get; set; }
    public string Publisher { get; set; }
    public string PlaceOfPublication { get; set; }
    public int YearOfPublication { get; set; }
    
    // Following field could have also been a number (int)
    // but to allow roman ciphers and the like 'string' was chosen as datatype.
    public string PagesBegin { get; set; }

    // Following field could have also been a number (int)
    // but to allow roman ciphers and the like 'string' was chosen as datatype.
    public string PagesEnd { get; set; }
    public string WebAdress { get; set; }

    public Title()
    {
        Authors = new ObservableCollection<Author>();
        ItemTitle = string.Empty;
        ItemType = string.Empty;
        Editors = new ObservableCollection<Author>(); 
        SeriesTitle = string.Empty;
        Volume = string.Empty;
        Issue = string.Empty;
        Publisher = string.Empty;
        PlaceOfPublication = string.Empty;
        PagesBegin = string.Empty;
        PagesEnd = string.Empty;
        WebAdress = string.Empty;
    }
}

public partial class Author : ObservableObject
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Author() 
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public Author(string firstName, string lastName)
    { FirstName = firstName; LastName = lastName; }

    public Author(int id, string firstName, string lastName)
    {  AuthorId = id; FirstName = firstName; LastName = lastName; }

}
