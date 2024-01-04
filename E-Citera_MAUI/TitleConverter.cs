using CommunityToolkit.Maui.Core.Extensions;

namespace E_Citera_MAUI;

public static class TitleConverter
{
    public static AuthorTableObj ConvertToAuthorTableObject(Author author)
    {
        AuthorTableObj authorTableObj = new AuthorTableObj();

        authorTableObj.ID = author.AuthorId;
        
        authorTableObj.FirstName = author.FirstName;
        authorTableObj.LastName = author.LastName;

        return authorTableObj;
    }

    public static AuthorTableObj ConvertToAuthorTableObjectRawWithoutID(Author author)
    {
        AuthorTableObj authorTableObj = new AuthorTableObj();
        
        authorTableObj.FirstName = author.FirstName;
        authorTableObj.LastName = author.LastName;

        return authorTableObj;
    }

    public static TitleTableObj ConvertToTitleTableObject(Title title)
    {
        TitleTableObj titleTableObj = new TitleTableObj();
        
        titleTableObj.ID = title.Title_ID;
        
        titleTableObj.ItemTitle = title.ItemTitle;
        titleTableObj.ItemType = title.ItemType;
        titleTableObj.SeriesID = title.SeriesID;
        titleTableObj.Publisher = title.Publisher;
        titleTableObj.PlaceOfPublication = title.PlaceOfPublication;
        titleTableObj.YearOfPublication = title.YearOfPublication;
        titleTableObj.PagesBegin = title.PagesBegin;
        titleTableObj.PagesEnd = title.PagesEnd;
        titleTableObj.WebAdress = title.WebAdress;

        return titleTableObj;
    }

    public static AuthorTitleLinkTableObj ConvertToAuthorTitleLinkTableObject(int titleId, int authorId, string role, int position, int isActive)
    {
        AuthorTitleLinkTableObj authorTitleLinkTableObj = new AuthorTitleLinkTableObj();
        authorTitleLinkTableObj.Title_ID = titleId;
        authorTitleLinkTableObj.Author_ID = authorId;
        authorTitleLinkTableObj.Role = role;
        authorTitleLinkTableObj.Position = position;

        /* 'IsActive' represents a boolean value.
        /* As the on SQLITE documentation states:
        /* "SQLite recognizes the keywords "TRUE" and "FALSE", as of version 3.23.0 (2018-04-02)
        /* but those keywords are really just alternative spellings for the integer literals 1 and 0 respectively."
        /* Therefore those are directly in use here.*/

        if (isActive == 0 || isActive == 1)
            authorTitleLinkTableObj.IsActive = isActive;
        else
            authorTitleLinkTableObj.IsActive = 0;

        return authorTitleLinkTableObj;
    }

    public static TitleTableObj ConvertToTitleTableObjectWithoutTitleID(Title title)
    {
        TitleTableObj titleTableObj = new TitleTableObj();
        
        titleTableObj.ItemTitle = title.ItemTitle;
        titleTableObj.ItemType = title.ItemType;
        titleTableObj.SeriesID = title.SeriesID;
        titleTableObj.Volume = title.Volume;
        titleTableObj.Issue = title.Issue;
        titleTableObj.Publisher = title.Publisher;
        titleTableObj.PlaceOfPublication = title.PlaceOfPublication;
        titleTableObj.YearOfPublication = title.YearOfPublication;
        titleTableObj.PagesBegin = title.PagesBegin;
        titleTableObj.PagesEnd = title.PagesEnd;
        titleTableObj.WebAdress = title.WebAdress;

        return titleTableObj;
    }

    public static TitleTableObj ConvertToTitleTableObjectRawWithoutIDs(Title title)
    {
        TitleTableObj titleTableObj = new TitleTableObj();
        titleTableObj.ItemTitle = title.ItemTitle;
        titleTableObj.ItemType = title.ItemType;
        titleTableObj.Volume = title.Volume;
        titleTableObj.Issue = title.Issue;
        titleTableObj.Publisher = title.Publisher;
        titleTableObj.PlaceOfPublication = title.PlaceOfPublication;
        titleTableObj.YearOfPublication = title.YearOfPublication;
        titleTableObj.PagesBegin = title.PagesBegin;
        titleTableObj.PagesEnd = title.PagesEnd;
        titleTableObj.WebAdress = title.WebAdress;

        return titleTableObj;
    }

    public static Title ConvertToTitle(TitleTableObj titleTableObj)
    {
        Title title = new Title();
        
        title.Title_ID = titleTableObj.ID;
        title.ItemTitle = titleTableObj.ItemTitle;
        title.ItemType = titleTableObj.ItemType;
        title.SeriesID = titleTableObj.SeriesID;
        title.SeriesTitle = GetSeriesTitle(title.SeriesID);
        title.Volume = titleTableObj.Volume;
        title.Issue = titleTableObj.Issue;
        title.Publisher = titleTableObj.Publisher;
        title.PlaceOfPublication = titleTableObj.PlaceOfPublication;
        title.YearOfPublication = titleTableObj.YearOfPublication;
        title.PagesBegin = titleTableObj.PagesBegin;
        title.PagesEnd = titleTableObj.PagesEnd;
        title.WebAdress = titleTableObj.WebAdress;

        List<Author> linkedAuthors = GetAuthorList(title.Title_ID, "author");
        if(linkedAuthors.Count > 0)
        {
            title.Authors = linkedAuthors.ToObservableCollection();
        }

        List<Author> linkedEditors = GetAuthorList(title.Title_ID, "editor");
        if (linkedEditors.Count > 0)
        {
            title.Editors = linkedEditors.ToObservableCollection();
        }

        return title;
    }

    public static string GetSeriesTitle(int seriesID)
    {
        string seriesTitle = string.Empty;
        SeriesTableObj seriesTableObj = DB_Handler.GetSeriesById(seriesID);
        if(seriesTableObj != null)
            seriesTitle = seriesTableObj.SeriesTitle;
        return seriesTitle;
    }

    public static List<Author> GetAuthorList(int titleID, string role)
    {
        List<Author> authors = new List<Author>();
        List<AuthorTitleLinkTableObj> authorLinks = DB_Handler.GetTitleAuthorLinks_FromTitleID_and_AuthorRole(titleID, role);
        if (authorLinks.Any())
        {
            foreach(AuthorTitleLinkTableObj authorLink in authorLinks)
            {
                AuthorTableObj authorTableObj = DB_Handler.GetAuthorById(authorLink.Author_ID);
                if(authorTableObj != null)
                {
                    Author author = new Author(authorTableObj.ID, authorTableObj.FirstName, authorTableObj.LastName);
                    authors.Add(author);
                }
            }
        }

        return authors;
    }

    public static SeriesTableObj ConvertToSeriesTitleObject(Title title)
    {
        SeriesTableObj seriesTableObj = new SeriesTableObj();

        seriesTableObj.SeriesID = title.SeriesID;
        seriesTableObj.SeriesTitle = title.SeriesTitle;

        return seriesTableObj;
    }

    public static SeriesTableObj ConvertToSeriesTitleObjectRawWithoutID(Title title)
    {
        SeriesTableObj seriesTableObj = new SeriesTableObj();

        seriesTableObj.SeriesID = title.SeriesID;
        seriesTableObj.SeriesTitle = title.SeriesTitle;
        return seriesTableObj;
    }
}


