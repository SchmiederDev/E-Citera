using SQLite;
namespace E_Citera_MAUI.Models;

/* [NOTE ON THE PURPOSE OF THIS CLASS/ MODEL:] 
 * What is a Citation Style?
 * 
 * A citation style is a specific way of referencing the works of others in academic papers and books. 
 * They might either either serve as a source for research or as secondary literature 
 * (the results of the research of others).
 * Usually, publishers or academic journals require authors to cite their sources or references 
 * to the works of others in a specific format in the foot- or endnotes of a text  as well as 
 * in reference lists or bibliographies.
 * This specific way of formatting the text of references in foot- and endnotes or reference lists is 
 * what a citation style is.
 */


/* [NOTE ON THE RELATIONSHIP TO 'CitationHandler']
 * Whereas this class is the model for a citation style the actual formatted string 
 * following the pattern of the model is created by 'CitationHandler'.
 * 
 * Constants, possible field names etc. for citation styles
 * are also declared and definded in the 'CitationHandler'-Class */


/* [TABLE SCHEMA FOR CITATION STYLE]
 * Since in the case of the 'Citation-Style'-Object-model runtime object and table structure (schema) 
 * are 100%-identical or SQLITE-compatible respectively - in contrast to the runtime objects/ data base-table 
 * for title, authors, authorlinks or series - the table decorators are here directly placed on the 
 * runtime object-class itself and not defined in 'DatabaseTables'.
 */

[Table("CitationStyles")]
public class CitationStyle : ObservableObject
{
    [PrimaryKey, AutoIncrement, NotNull]
    [Column("Style_ID")]
    public int StyleID { get; set; }

    [Column("Style_Name")]
    public string StyleName { get; set; }

    [Column("Last_Name_First")]
    public bool LastNameFirst {  get; set; }

    [Column("Author_Separator")]
    public string AuthorSeparator { get; set; }

    [Column("Authors_Et_Al")]
    public bool EnableAsEtAl_Authors {  get; set; }

    [Column("Authors_Et_Al_Tag")]
    public string EtAlTag_Authors { get; set; }

    [Column("Mentioned_Authors")]
    public int NumberOfAuthorsMentioned { get; set; }

    [Column("Mark_Editors")]
    public bool MarkAsEditors { get; set; }

    [Column("Editors_Et_Al")]
    public bool EnableAsEtAl_Editors { get; set; }

    [Column("Editor_Tag")]
    public string EditorTag { get; set; }

    [Column("Editors_Mentioned")]
    public int NumberOfEditorsMentioned { get; set; }

    [Column("Titel_in_Quotes")]
    public bool TitleInQuotes { get; set; }

    [Column("Series_in_Quotes")]
    public bool SeriesInQuotes { get; set; }

    [Column("Series_In_Prefix")]
    public bool In_Prefix {  get; set; }

    [Column("In_Capitals")]
    public bool In_IsCapitalized { get; set; }

    [Column("IssueInBraces")]
    public bool IssueInBraces { get; set; }

    [Column("YearInBraces")]
    public bool YearInBraces { get; set; }

    [Column("Citation_Field_00")]
    public string CitationField_00 { get; set; }

    [Column("Separator_00")]
    public string Separator_00 { get; set; }

    [Column("Citation_Field_01")]
    public string CitationField_01 { get; set; }

    [Column("Separator_01")]
    public string Separator_01 { get; set; }

    [Column("Citation_Field_02")]
    public string CitationField_02 { get; set; }

    [Column("Separator_02")]
    public string Separator_02 { get; set; }

    [Column("Citation_Field_03")]
    public string CitationField_03 { get; set; }

    [Column("Separator_03")]
    public string Separator_03 { get; set; }

    [Column("Citation_Field_04")]
    public string CitationField_04 { get; set; }

    [Column("Separator_04")]
    public string Separator_04 { get; set; }

    [Column("Citation_Field_05")]
    public string CitationField_05 { get; set; }

    [Column("Separator_05")]
    public string Separator_05 { get; set; }

    [Column("Citation_Field_06")]
    public string CitationField_06 { get; set; }

    [Column("Separator_06")]
    public string Separator_06 { get; set; }

    [Column("Citation_Field_07")]
    public string CitationField_07 { get; set; }

    [Column("Separator_07")]
    public string Separator_07 { get; set; }

    [Column("Citation_Field_08")]
    public string CitationField_08 { get; set; }

    [Column("Separator_08")]
    public string Separator_08 { get; set; }

    [Column("Finalizer")]
    public string Finalizer { get; set; }

    /* In the near future E-Citera will come with a few ready made CitationStyles 
     * (such as 'MLA' (Modern Language Association) for example)
     * I'm considering providing them in the form of a JSON-file
     * The citation styles created by a user would than be merged
     * with default styles provided at runtime
     * To be none the less able to differentiate between a default style
     * and one created by the user this bool will serve as the distinction criteria.
    */
    public bool IsDefaultStyle { get; set; } = false;
}
