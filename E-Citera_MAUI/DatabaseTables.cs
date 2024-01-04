using SQLite;

namespace E_Citera_MAUI
{
    #region TABLE-OBJECTS RELATED TO THE 'TITLE'-CLASS
    /* The following classes are all related to the 'Title'-Class
     * but which is 'split' into several sub-categories for representation in the database.
     * This serves several reasons for this - but the most important one is the standard principle
     * of relational databases: the same object (one author or series e. g.) can be linked to several
     * other objects, first and foremost Titles.
     * At runtime on the contrary, it serves the needs of this better when these sub-categories
     * are merged again into one Model (The 'Title'-class).
     */

    [Table("Authors")]
    public class AuthorTableObj
    {
        [PrimaryKey, AutoIncrement, NotNull]
        [Column("Author_ID")]
        public int ID { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }
    }

    [Table("AuthorsAndTitles")]
    public class AuthorTitleLinkTableObj
    {
        [PrimaryKey, AutoIncrement, NotNull]
        [Column("Entry_ID")]
        public int Entry_ID { get; set; }

        [Column("Title_ID")]
        public int Title_ID { get; set; }

        [Column("Author_ID")]
        public int Author_ID { get; set; }

        [Column("Role")]
        public string Role { get; set; }

        /* The following field 'IsActive' represents a boolean value.
        /* As the on SQLITE documentation states:
        /* "SQLite recognizes the keywords "TRUE" and "FALSE", as of version 3.23.0 (2018-04-02)
        /* but those keywords are really just alternative spellings for the integer literals 1 and 0 respectively."
        /* Therefore those will be used directly here.*/

        [Column("Active_Status")]
        public int IsActive { get; set; }

        [Column("Position")]
        public int Position { get; set; }
    }

    [Table("Titles")]
    public class TitleTableObj
    {
        [PrimaryKey, AutoIncrement, NotNull]
        [Column("Title_ID")]
        public int ID { get; set; }

        [NotNull]
        [Column("Title")]
        public string ItemTitle { get; set; }

        [Column("Type")]
        public string ItemType { get; set; }

        [Column("Series")]
        public int SeriesID { get; set; }

        /*
         * The data types of volume, issue and pages could have also been a numeric format - 
         * but I decided against this option and declared them as also strings, 
         * because this gives the user the opportunity to enter roman ciphers and the like 
         * or add further special characters required by a citation style not covered (yet) 
         * by the 'CitatioStyle'-class or the CitationHandler-class.
         */

        [Column ("Volume")]
        public string Volume { get; set; }

        [Column("Issue")]
        public string Issue { get; set; }

        [Column("Publisher")]
        public string Publisher { get; set; }

        [Column("Place")]
        public string PlaceOfPublication { get; set; }
        
        [Column("Year")]
        public int YearOfPublication { get; set; }

        [Column("Pages_Begin")]
        public string PagesBegin { get; set; }

        [Column("Pages_End")]
        public string PagesEnd { get; set; }

        [Column("Web_Adress")]
        public string WebAdress { get; set; }
    }    

    [Table("Series")]
    public class SeriesTableObj
    {
        [PrimaryKey, AutoIncrement, NotNull]
        [Column("ID")]
        public int SeriesID { get; set; }

        [Column("Name")]
        public string SeriesTitle { get; set; }
    }

    #endregion

    // TABLE SCHEMA FOR CITATION STYLE
    /* There is a another table definition to be found under Models -> Class: 'CitationStyle'.
     * Since runtime object and table structure (schema) are in this case 100%-identical 
     * or SQLITE-compatible respectively - in contrast to the runtime objects/ data base-table 
     * for title, authors, authorlinks, series or reference list entries 
     * - therefore in this case the table decorators were directly placed 
     * on the runtime object-class itself.
     */

    [Table("Reference_Lists")]
    public class ReferenceListTableObj
    {
        [PrimaryKey, AutoIncrement, NotNull]
        [Column("Entry_ID")]
        public int Entry_ID { get; set; }

        [Column("List_ID")]
        public string ListID { get; set; }

        [Column("List_Name")]
        public string ReferenceListName { get; set; }

        [Column("Title_ID")]
        public int TitleID { get; set; }
    }
}
