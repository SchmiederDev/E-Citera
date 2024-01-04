namespace E_Citera_MAUI;

/* [PURPOSE OF CITATION HANDLER:]
 * This static class serves the purpose of converting 'Title'-Objects 
 * (see the model-class in the 'Models' folder) into formatted strings
 * used as output on the 'TitleEditPage' and the 'ReferenceListPage'
 * following the pattern of a 'CitationStyle'-Object 
 * (also, see the model-class in the 'Models' folder)
 * which the user may copy directly from the output into foot-/endnotes
 * or bibliographies.
 * The formatted string created by the CitationHandler 
 * is passed to string class members of 'TitleEditView' and 'ReferenceListView' 
 * which are bound to the Text-property of 'Editor'-UI-Elements = the output.
 */
public static class CitationHandler
{
    /* The following readonly fields/ array's are in use on 'CitationStyleView'
     * Because only some special should be allowed as separators 
     * (see the readme-file for the purpose of this)
     * there are selected from picker-menus on CitationStylesPage
     * instead of input fields.*/

    public static readonly char[] Separators = 
        { '.', ',', ';', ':', '-', '/', '\\', '&'};

    public static readonly char[] AuthorSeparatorCharacters = {',', ';',  '/', '&'};
    public static readonly char[] FinalizerCharacters = { '.', ',', ';', ':' };

    public static readonly string[] CitationFieldNames =
    {"Author", "Title", "Editor", "Series", "Volume", "Issue", "Publisher",
         "Place of Publication", "Year"};

    /* [NOTE ON: why the above declared arrays are declared as strings and not just a 'char'-array]
     * If anyone wonders why these are declared as strings and not just a 'char'-array:
     * 'You cannot have const arrays in C#.[And this is exactly what I would have needed here] That is one of the limitations.
     * You can make the variable that stores an array readonly so it cannot be pointed to something else. 
     * The size of the array is fixed at creation time(it is an attribute of the type, not like C++) so it also cannot be resized without creating a new array.
     * But the contents of an array is writable and there is nothing you can do about it.'
     * Contribution of Michael Taylor on "learn.microsoft.com" replying to the following request:
     * 
     * https://learn.microsoft.com/en-us/answers/questions/978752/const-arrays-in-functions */

    public static string CreateCitationString(Title citationTitle, CitationStyle style)
    {
        string citation = string.Empty;
        
        if (citationTitle != null && style != null)
        {
            if (!string.IsNullOrEmpty(style.CitationField_00))
            {
                string citationField = TransformCitationField(citationTitle, style.CitationField_00, style, citation);
                if (!string.IsNullOrEmpty(citationField))
                {
                    citation += citationField;
                    if (!string.IsNullOrEmpty(style.Separator_00))
                        citation += style.Separator_00 + " ";
                    else
                        citation += " ";
                }
            }

            if (!string.IsNullOrEmpty(style.CitationField_01))
            {
                string citationField = TransformCitationField(citationTitle, style.CitationField_01, style, citation);
                if (!string.IsNullOrEmpty(citationField))
                {
                    citation += citationField;
                    if (!string.IsNullOrEmpty(style.Separator_01))
                        citation += style.Separator_01 + " ";
                    else
                        citation += " ";
                }
            }

            if (!string.IsNullOrEmpty(style.CitationField_02))
            {
                string citationField = TransformCitationField(citationTitle, style.CitationField_02, style, citation);
                if (!string.IsNullOrEmpty(citationField))
                {
                    citation += citationField;
                    if (!string.IsNullOrEmpty(style.Separator_02))
                        citation += style.Separator_02 + " ";
                    else
                        citation += " ";
                }
            }

            if (!string.IsNullOrEmpty(style.CitationField_03))
            {
                string citationField = TransformCitationField(citationTitle, style.CitationField_03, style, citation);
                if (!string.IsNullOrEmpty(citationField))
                {
                    citation += citationField;
                    if (!string.IsNullOrEmpty(style.Separator_03))
                        citation += style.Separator_03 + " ";
                    else
                        citation += " ";
                }
            }

            if (!string.IsNullOrEmpty(style.CitationField_04))
            {
                string citationField = TransformCitationField(citationTitle, style.CitationField_04, style, citation);
                if (!string.IsNullOrEmpty(citationField))
                {
                    citation += citationField;
                    if (!string.IsNullOrEmpty(style.Separator_04))
                        citation += style.Separator_04 + " ";
                    else
                        citation += " ";
                }
            }

            if (!string.IsNullOrEmpty(style.CitationField_05))
            {
                string citationField = TransformCitationField(citationTitle, style.CitationField_05, style, citation);
                if (!string.IsNullOrEmpty(citationField))
                {
                    citation += citationField;
                    if (!string.IsNullOrEmpty(style.Separator_05))
                        citation += style.Separator_05 + " ";
                    else
                        citation += " ";
                }
            }

            if (!string.IsNullOrEmpty(style.CitationField_06))
            {
                string citationField = TransformCitationField(citationTitle, style.CitationField_06, style, citation);
                if (!string.IsNullOrEmpty(citationField))
                {
                    citation += citationField;
                    if (!string.IsNullOrEmpty(style.Separator_06))
                        citation += style.Separator_06 + " ";
                    else
                        citation += " ";
                }
            }

            if (!string.IsNullOrEmpty(style.CitationField_07))
            {
                string citationField = TransformCitationField(citationTitle, style.CitationField_07, style, citation);
                if (!string.IsNullOrEmpty(citationField))
                {
                    citation += citationField;
                    if (!string.IsNullOrEmpty(style.Separator_07))
                        citation += style.Separator_07 + " ";
                    else
                        citation += " ";
                }
            }

            if (!string.IsNullOrEmpty(style.CitationField_08))
            {
                string citationField = TransformCitationField(citationTitle, style.CitationField_08, style, citation);
                if (!string.IsNullOrEmpty(citationField))
                {
                    citation += citationField;                    
                }
            }

            if (!string.IsNullOrEmpty(citationTitle.PagesBegin) || !string.IsNullOrEmpty(citationTitle.PagesEnd))
            {
                if (!string.IsNullOrEmpty(style.Separator_08))
                    citation += style.Separator_08 + " ";
                else
                    citation += " ";

                bool pagesBeginWasEmpty = false;
                if (!string.IsNullOrEmpty(citationTitle.PagesBegin))
                    citation += citationTitle.PagesBegin;
                else
                    pagesBeginWasEmpty = true;

                if (!string.IsNullOrEmpty(citationTitle.PagesEnd))
                { 
                    if(pagesBeginWasEmpty)
                        citation += citationTitle.PagesEnd;
                    else
                        citation += "-" + citationTitle.PagesEnd;
                }
            }

            if (!string.IsNullOrEmpty(style.Finalizer))
                citation += style.Finalizer;

            if (!string.IsNullOrEmpty(citationTitle.WebAdress))
                citation += " " + citationTitle.WebAdress + ".";

        }

        return citation;
    }

    private static string TransformCitationField(Title citationTitle, string citationField, CitationStyle style, string currentCitationState)
    {
        string fieldCitation = string.Empty;
        switch(citationField)
        {
            case "Author":
                fieldCitation = CreateAuthorsString(citationTitle.Authors.ToList(), style.EnableAsEtAl_Authors, style.NumberOfAuthorsMentioned, style.LastNameFirst, style.AuthorSeparator, style.EtAlTag_Authors);
                break;

            case "Title":
                fieldCitation = QuoteTitle(citationTitle.ItemTitle, style.TitleInQuotes);
                break;

            case "Editor":
                string editorCitationRaw = CreateAuthorsString(citationTitle.Editors.ToList(), style.MarkAsEditors, style.NumberOfEditorsMentioned, style.LastNameFirst, style.AuthorSeparator, style.EditorTag);
                if (style.In_Prefix && citationTitle.Editors.Count > 0)
                {
                    if (!string.IsNullOrEmpty(citationTitle.Editors[0].LastName))
                        fieldCitation = AddInPrefix(currentCitationState, editorCitationRaw, style.In_IsCapitalized);
                }
                else
                    fieldCitation = editorCitationRaw;
                break;

            case "Series":
                string seriesCitationRaw = QuoteTitle(citationTitle.SeriesTitle, style.SeriesInQuotes);
                if (style.In_Prefix && !string.IsNullOrEmpty(citationTitle.SeriesTitle))
                    fieldCitation = AddInPrefix(currentCitationState, seriesCitationRaw, style.In_IsCapitalized);
                else
                    fieldCitation = seriesCitationRaw;
                break;

            case "Volume":
                fieldCitation = citationTitle.Volume;
                break;

            case "Issue":
                if(style.IssueInBraces)
                {
                    if(!string.IsNullOrEmpty(citationTitle.Issue))
                        fieldCitation = new string("(" + citationTitle.Issue + ")");
                }
                else
                    fieldCitation = citationTitle.Issue;
                break;

            case "Publisher":
                fieldCitation = citationTitle.Publisher;
                break;

            case "Place of Publication":
                fieldCitation = citationTitle.PlaceOfPublication;
                break;

            case "Year":
                if(citationTitle.YearOfPublication > 0)
                {
                    if (style.YearInBraces)
                    {
                        if(!string.IsNullOrEmpty(citationTitle.YearOfPublication.ToString()))
                        {
                            fieldCitation = new string("(" + citationTitle.YearOfPublication.ToString() + ")");
                        }
                    }
                    else
                        fieldCitation = citationTitle.YearOfPublication.ToString();
                }
                break;

            default:
                fieldCitation = string.Empty;
                break;
        }

        return fieldCitation;
    }

    private static string CreateAuthorsString(List<Author> authors, bool etAlEnabled, int numberMentioned, bool lastNameFirst, string separator, string tag)
    {
        string authorsString = string.Empty;

        if(authors != null && authors.Count > 0)
        {
            if (!string.IsNullOrEmpty(authors[0].LastName))
            {
                if (etAlEnabled && authors.Count > 1)
                {
                    if (numberMentioned != 0)
                    {
                        for (int i = 0; i < authors.Count; i++)
                        {
                            if (i < numberMentioned)
                            {
                                if (lastNameFirst)
                                    authorsString += authors[i].LastName + ", " + authors[i].FirstName;

                                else
                                    authorsString += authors[i].FirstName + " " + authors[i].LastName;
                                if (i < numberMentioned - 1 && i < authors.Count - 1)
                                    authorsString += separator + " ";
                            }

                            else
                                break;
                        }

                        if (!string.IsNullOrEmpty(tag))
                            authorsString += " " + tag;
                        else
                            authorsString += " et al.";
                    }

                    else
                    {
                        for (int i = 0; i < authors.Count; i++)
                        {
                            if (lastNameFirst)
                                authorsString += authors[i].LastName + ", " + authors[i].FirstName;

                            else
                                authorsString += authors[i].FirstName + " " + authors[i].LastName;
                            if (i < authors.Count - 1)
                                authorsString += separator + " ";
                        }
                    }
                }

                else
                {
                    for (int i = 0; i < authors.Count; i++)
                    {
                        if (lastNameFirst)
                            authorsString += authors[i].LastName + ", " + authors[i].FirstName;

                        else
                            authorsString += authors[i].FirstName + " " + authors[i].LastName;
                        if (i < authors.Count - 1)
                            authorsString += separator + " ";
                    }
                }
            }
        }

        return authorsString;
    }

    private static string QuoteTitle(string title, bool inQuotes)
    {
        if(inQuotes && !string.IsNullOrEmpty(title))
            return new string("\"" + title + "\"");
        else 
            return title;
    }

    private static string AddInPrefix(string currentCitationState, string previouStr, bool isCapitalized)
    {

        if (!currentCitationState.Contains("in", StringComparison.CurrentCultureIgnoreCase))
        {
            if (isCapitalized)
                return previouStr.Insert(0, "In: ");
            else
                return previouStr.Insert(0, "in: ");
        }

        else
            return previouStr;
        
    }
}


