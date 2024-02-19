[!Note]: On E-Citera as my final project for CS50: To keep the filesize down I did not include some of the .Net-MAUI standard files
as well as the standard project files (such as '.sln' for a Visual-Studio-Project) - otherwise this project would have had filesize
of more than 500MB. - But the project folder includes all important files where I wrote my code in.

# E-Citera

E-Citera is a reference administration software that helps users to keep track of all the literature (books, papers and articles etc.) they read and need for their academic work and private purposes and interests as well of course.

# Project Description

## Technologies

E-Citera is designed as a .NET-MAUI-App written in C# created with Visual Studio using a SQLITE-Database.

Therefore it is a platform independent software that should run on any device, be it a desktop machine or smartphone or tablet,
be it Windows (currently still 10), Android, MacOS (via MacCatalyst) or iOS as operating system.

In addition to the standard NuGet-Packages for .NET-MAUI E-Citera uses:

- The SQLite-net-pcl and SQLitePCLRaw.provider.dynamic_cdecl NuGetPackages as libraries for creating an interface to a SQLite-Database.

- E-Citera also uses the .NET-MAUI- and .NET-MVVM Community Toolkits to simplify the implementation of the INotifyPropertyChanged and ICommand-Interfaces
  which are necessary for data bindings to work in a MAUI-MVVM-App (and also to shorten the code produced by the developer of this app).


## What the app does:

- E-Citera allows users to create a database in which all of the titles of interest to them are stored according to the needs of complete bibliographical detail needed for academic research
- although it is of course possible to keep it as simple as just saving the title of a work for future reference.

- Furthermore users can not only store titles with detailed bibliographical information they can also assign collections of titles to reference lists related to a specific topic or bibliography for one of their own academic works,
  papers or books.

- Finally, E-Citera allows users to convert the bibliographical information of a single title or a whole reference list according to the needs of a specific academic citation style, like Harvard referencing,
Chicago- or MLA (Modern Language Association)-style and simply copy the created citation directly into your foot- or end notes or a reference list at the end of one of their own papers
without the need of formatting the citation themselves.

But, they are not bound to widely used citation styles because E-Citera let's them actually create and store as many citation styles as they like.
So users could easily adapt to the requirements of a small journal for publishing one of their papers without the need of manually (re-)writing all the titles cited.



-----------------------

# How to use E-Citera:

## Navigation

There a for pages (windows or views) you can navigate to:

- 'Title Overview' in which all the titles in your database are listed
- 'Edit': This is where users can create new titles and store them to their database or edit existing ones as well as create a citation of a single title.
- 'Citation Styles': This is where users can create new citation styles or edit existing ones.
- 'Reference Lists': This is where users can manage reference lists for bibliographies, foot- or endnotes or other purposes.

- You can navigate to all of the pages via the navigation bar (tab bar) at the top of every page.

- Depending on the operating system you are using the navigation bar might also appear at the bottom, e. g. on Android.

- If your are within one of the pages and navigated to this page from the title overview[^1]
you can also navigate back to the main page ('Title Overview') using the back button at the top left corner of the screen or the 'back-button' on your phone or tablet.

[^1]: *(see the page specification for 'Title Overview' for further information)

------------------

# The 'Title Overview'-Page:
- This is the main page (window or view) of the app where all of the titles in your data base are listed.
(If you have no titles in your database yet, head over to the 'Edit'-page. Click 'Edit' on the navigation bar.)

## How to work with the titles in 'Title Overview':

- Every title (item) in your data base will appear as a box/ card containing a condensed summary of the bibliographical information.

- Every card in the card box has three buttons you can click on to perform a specific operation on the title:

- The 'Edit'-Button <sup>(the icon with a pencil writing on a sheet of paper)</sup>
		- This button will navigate to the 'Edit'-Page where you can change the data belonging to the title you just clicked on or quote it as a single citation.
		- If you are finished (don't forget to save!) you can use the 'back'-button at the top left corner to navigate back to the main page or any of the navigation tabs to go to a different page of the app.

- The 'Add-to-Reference-List'-Button <sup>(= the 'Plus'-Button containing a 'plus'-sign)</sup>:

	- This button will navigate you to the reference lists where the selected title will be added to a reference list.
	- If there is no existing list loaded or active a new one will be created which you can add as many titles as you like to and which you can save after you entered a name for the list[^2]
	- If an existing list is already loaded the selected title will be added to this list when you click on the plus-button in title overview.
	- If you accidentally clicked on the 'add-to-reference-list'-button don't worry titles can be easily removed from the currently loaded list.[^2]

- The 'Delete'-Button <sup>(= the 'X'-button containing the letter 'X'</sup>

	- Clicking on this button is one way of deleting titles from your database.
	> [!Warning]
	> The title will be removed permanenttly from your data base! So, >[!Caution] be careful hitting the 'Delete'-button.

[^2]: See 'Reference List Page' for further information

### Searching for titles in the database from 'Title Overview':

- You can **search** the **titles** in your database by entering a **_search term_** in the **Search-Bar** _at the **top**_ of the **'Title Overview'**.
- Currently you can **search by title** (the actual the title of a book, paper, article, etc.) or **author names**.[^3]

- The **results of your search** will appear as a **list below the Search Bar**. You can _click on either one of the results_.

	- When you **click on one of the results** you will be **prompted to choose one of the actions** which can be performed on this result/ title.

		- When you **click on** the **'Edit'**-option (action) you will be **navigated to the Edit-page** where you can _change the data belonging to the title selected_ on or _quote it as a single citation_.
		- When you **click on** the **'Add to reference list'**-option (action) the selected title will be added to a reference list and you will be navigated to the 'Reference Lists'-Page.

			- If you want to add more titles to this reference list just navigate back via the 'back'-button or the tab-bar-menu and add another title via a new search or hitting the 'add'-button on a title-card.

[^3]: Future implementations will allow to search the full range of bibliographical data]

------------------

# How to work with titles on the 'Edit'-Page:

- On the (Title-)Edit-Page you have the opportunity to create new titles for you database or edit existing ones.

## Navigation to Edit-Page and Operations on the title:

### Modes of navigation from and to the Edit-Page:

- From the **Main Menu (= Tab-Bar)** by clicking on the **'Edit'-Tab.**

- From **Title-Selection on the Main-Page/ 'Title-View'** by **clicking on the Edit-Icon** <sup>(the pen or pencil over an empty sheet)</sup>
**OR** via **searching for titles** and than seleceting a title in the search results and clicking on 'edit' in the action-sheet-pop-up which will appear in consequence.

**Navigating from the tab bar:**

If you haven't edited any title during a session of E-Citera running and navigate from the tab bar you will get a clean slate for a new title not stored yet in the database.

**Navigating via Edit-Button or Search Results:**

The Edit-Page will always contain the most recently edited title during a session.
If there is already a Title loaded (because you already edited one or several titles during a session of the app running) but you need a clean slate just hit the 'New'-Button on the top of the Edit-Page.
If you don't create a new 'Title' any changes will be saved to the currently loaded title.

### Actions (Buttons) on the Edit-Page:

**At the top of the Edit-Page**

**'New'**-Button: Clicking this button will result in a clean slate for adding a new title to the data base
<sub>(a new instance of the 'Title'-class will be created and replace any previously existing instance of type 'Title' present in 'TitleEditView')</sub>

**'Save'**-Button: Clicking this button will save any currently loaded title.
- If the title already exists in the database the data will be updated (changes saved), if it doesn't a new title will be saved to the database.

**'Delete'**-Button: [!Warning] Clicking on this button **will remove any currently loaded title from the database permanenttly.**

**At the bottom of the Edit-Page**

**'Cite'**-Button: If a citation style is selected on the corresponding Picker (Selection-Menu) clicking on the button **will create a formatted citation of the currently edited title** (if there is any)
The result can be seen, edited and copied from the 'editor' (box) below the 'Cite'-Button.

[!Note] **Make sure to select a citation style** from the selection-menu ('Picker' in MAUI-UI-Terms) **next to the 'Cite'-button** before you're trying to cite any title.

## Input Fields on the Edit-Page:

Most of the (input-)fields should be pretty self explanatory for anyone who is not completely unfamiliar with bibliographical data, thus the academic target group of this app.
But even users who are not well versed to the world of libraries, titles and citations (yet) should hopefully quickly understand which kind of information should be put in which field.

The input-content of each field has an explanatory label above as well as an explanatory placeholder within the field appearing when there is no input yet.

[!Note] _Any text put into the fields or changed on the fields_ ('Entries' in MAUI-UI-Terms) **will be saved only after you clicked the 'Save'-button.**

Currently there are **12 fields** able to **hold all crucial bibliographical data** for a title or work:

**Author(s)** [^4]:
- [!Important] By default E-Citera let's you/ the user input one author at first.
	But the **'Add Author'**-Button let's you add as many authors as you like.
	Because of the functionality/ design of the 'Title'/ 'Citation Styles'-classes users
	have the opportunity to save 5 authors to their database but mention only 3 in a citation,
	possibly followed by an 'et al.'-tag when they wish to.


**Title**: The actual title of a specific work (book or paper).[^5]

**Editors:** The same principles mentioned for authors apply to editors as well.
			(one or many, number of editors mentioned in citation may be smaller than the number stored in the data base).

**SeriesTitle:** This field let's the user input and save a series as a sub-category whether it be series,
collective volume (anthology) or journal title[^6]

**Volume**:[^7] This input field can be used either to store the volume of a series, the volume of a multi-volume work
or the edition of a monography.

**Issue**[^7]

**Publisher**
**PlaceOfPublication**
**YearOfPublication**[^8]
[Pages:] Pages-Begin/Pages-End**:[^7]
- Can store page notes in any form: the beginning and end page of an article or relevant pages only
and independent of the type of ciphers (see below).
- If the user wishes to have an **'p.' or 'pp.' in front of the page number in a citation**
they should **enter it HERE**.
	<sub>I decided against an automatic insertion of page notation - like 'p.' or 'pp.' - since those annotation can differ
	from language to language, or publisher requirements.
	Future versions app might offer a few _default automatic page annotation suggestions_
	which than would be automatically added in
	when a title is cited.</sub>

**WebAdress**

------------------

[^4]: [!Note] Authors and editors put in by the user are stored in a separate table of the database and the links/ relation between a title and its authores/ editors in another.
When, for example, a title is deleted so will be the entries in the relational 'AuthorsAndTitles'-Table in the database but not the entry for an author or editor in the 'Authors'-Table.
_Future versions of this app_ will expand further on the possibilities that come with this approach:
Mainly, **letting the user choose between a _title-based_ OR _author-based approach_.**
The author-based approach would for example allow the user to grab all the works of a person independent of whether they assumed the role of an author or the role of an editor in the case.
On the 'Edit'-Page I'm planing to implement a mechanism for automatic author/editor suggestion etc.

[^5]: Due to the different meanings of 'title' in the english language in a bibliographical context there might arise some confusion which I found hard to avoid completely.
This is why also the context of this app 'title' can have two meanings:

Most importantly it refers to the whole 'thing' (object, class (model) or item in the database) which includes all bibliographical data belonging to the specific work of an author.
But secondly, it can refer to the actual title of a piece of literature - e. g. '**_On the Origin of Species_**', by Charles Darwin.
To differentiate the entire object (= 'Title'as a class) from a title in the sense of a single string containing the actual title of a book or paper the respective member property of the 'Title'-class (model) is called 'ItemTitle'.

[^6]: Series are kept in a separate database-table independent of the title they are linked to on first creation (inserting).
This allows of course to link several titles to a single series (e. g. a specific academic journal).
Future versions of this app will expand on this, for example let's the user search for a series
and then add all relevant titles of a series (journal) collectively into a new reference list,
thus _for example_ a series of articles in the _'American Historical Review'_ which are _of relevance to the user_.

[^7]: The data types of volume, issue and pages could have also been a numeric format - but I decided against and declared them as strings,
because this gives the user the opportunity to enter roman ciphers and the like or add further special characters required by a citation style
and is not covered (yet) by the model of the 'CitatioStyle'-class, thus allowing users at least to store titles in the most recent format they require them to be.

[^8]: The only member of type 'int' in the 'Title'-class (model) since a year can hardly be anything else but a number.
[!Note:] In the future I might include 'Month' and 'Day' in the model as well since some 'MINT'-discipline journals require that amount of precision when it comes to the time of publication of a paper.

---------------------------

# How to work with Citation Styles on the 'Citation Styles'-Page:

- The 'Citation Styles'-Page is where users can create, save, edit or delete Citation Styles.

## What is a Citation Style?

A citation style is a specific way of referencing the works of others in academic papers and books. They might either either serve as a source for research or as secondary literature (the results of the research of others).
Usually, publishers or academic journals require authors to cite their sources or references to the works of others in a specific format in the foot- or endnotes of a text as well as in reference lists or bibliographies.
This **specific way of formatting the text of references in foot- and endnotes or reference lists is** what **a citation style** is.

## 'Citation Fields' and 'Separators':

- In the center of the 'Citation-Styles'-Page you (the user) will quickly note a number of **drop-down menus** (pickers in MAUI-UI-terms) **all labeled 'citation field' or 'separator'** accompanied with ascending numbers
- This is **where users can choose _which parts of a title or work_ they want to appear in a citation _in whatever order they like_** or need the fields to be.

- Each **'Citation Field'** can represent **any part of the bibliographical data** belonging to a title.
- Each **'Separator'** is the special character that will follow the corresponding 'Citation Field'[^9] in the citation of a title,

<sub>e. g. when a citation style requires the _publisher to be followed by a colon_ before the _place of publication_ is mentioned you would select **Citation Field[n]** = 'Publisher', followed by **Separator[n]** = ':',
followed by **Citation Field[n - 1]** = 'Place of Publication'.</sub>

At first glance, the naming of the fields might seem a little cryptic, but the abstract naming and numeration is fully intended in this case.
It serves the purpose of giving **users as much freedom as possible.**
<sub>(The only restriction beeing that the maximum number of citation fields may not be greater than the underlying member properties of the data model for the Title-class or the data base respectively).</sub>

To give you an example of why this level of abstraction is useful:
Most of the times a citation style will begin with mentioning the author(s) <sup>(but with E-Citera it could be any other)</sup>.
But already second part of a citation might vary greatly:
Whereas some citation styles require that the author be followed by the title others put the year of publication after the author(s) which is only then followed by the actual title.

## The citation of authors:

- The adjustments in this section will be applied to both authors and editors since in none of the widely used citation styles they are treated differently within one citation style.

- The first thing users of E-Citera may decide is whether the last name of any of the authors should be mentioned first, represented by the checkbox 'Last Name first'.
- Checking the check box will have the result that the last name of any of the authors or editors will always appear in front of the first name. Leaving it unchecked will result in the opposite order.

- **Author Separator:** Will be used to separate authors in citation if there are several, e.g. if the _slash is selected_ in the drop-down-menu the result in a citation will be: Doe, John[/] Doe, Jane[/] etc.
<sup>the square brackets represent the place in which the separator will be inserted into in citation. It will not appear in the actual citation.</sup>

- The **Enable et al.**'-Checkbox: If this checkbox is checked **only a limited number of authors will appear in a citation**, **specified in** the **number of authors mentioned**-Entry-field (and must be of type integer).

- The **Et al. Tag**: The sequence of characters entered in this text input (Entry) _will appear after a limited number of authors if the 'et al.'-Checkbox is enabled_.
	- In English papers and books this will usually be 'et al.' but in other languages there might be the need to enter another sequence here, e. g. 'u. a.' (short for 'und andere'/ and others) in German speaking countries.

- The same adjustments can be set for editors with the exception **'Mark as Editors'** which is an additional field (checkbox) for editors because in some citation styles they are explicitly mentioned as editors
for example by adding an 'Eds.' after their names - this **result** can be accomplished **by checking** the **'Mark as Editors'**-Checkbox **and entering 'Eds.' in the tag field**.
Whereas in other citation styles **only the names** might appear - this result can simply be accomplished by **leaving the 'Mark as Editors'-Checkbox unchecked**.

**Main title in quotes:** The main title will appear in double quotes in the citation.
**Series title in quotes:** The series title will appear in double quotes in the citation.

**Add 'in'-Prefix:** If this option is enabled (the checkbox checked) an 'in' will automatically be inserted in front of the editors and the series title.
For example: 'Doe, John: Depictions of turtles in classical antiquity; [in:] Classical Antiquity 143(2007), pp. 23-107.'

- **If the 'In' should be capitalized check the 'Capitalize 'In'citation'-Checkbox**.

**Issue number in braces**/ **Year in braces**: These fields will appear in braces in the citation.

## The 'New', 'Save', 'Load' and 'Delete'-Buttons:

- These buttons do essentially what their text/label says, but NOTE:
- The **'Load'-button** will **load a Citation Style for editing**. It is not loaded to cite titles. Please use the 'Cite'-Buttons on the 'Edit' and 'Reference Lists'-Pages to do so,
after you selected a Citation Style from the drop-down-menus (pickers) close by.

[!Warning] **Clicking on the Delete-Button** will **remove the loaded Citation Style from the database permanently**.

--------------------------

#How to create and work with Reference Lists:

- You can navigate via the tab bar navigation or from the Main-Page/ 'Title Overview' by selecting titles for adding (see below).

## Adding titles to a reference list

- There are two ways how you can **add titles to a reference list**:

	- By clicking the **add-button (_Plus-Icon_) on a title-card** on the 'Title Overview' page
	- By **searching for titles** - also from 'Title Overview' and **clicking on a title from the search results** at the top of the page:
		- You will be **prompted** whether to 'Edit' or **'Add-to-Reference-List'** the selected title, click on 'Add-to-Reference-List'

## Removing titles from a reference list:

- If there are any titles in the active reference list each title can be deleted from the list by clicking on the **'X'-Button next to the title.**
	- [!Note] Removing a title from the reference list will at first only remove it from any active list (existing in memory).
	- It will **NOT be removed from the database unless** the active reference list is **saved** in its current state.

## Actions (Buttons) on the Reference-List-Page:

- **'New List'-Button**: As the name suggests, a new reference list will be created. This new list will only exist in the computers/the app's memory unless you save it.
<sub>(if there is any reference list already loaded, this list will be discarded BUT NOT deleted from the database)</sub>

- **'Edit List Name'-Button**: You can edit the name of the reference list.

	- When you work on an yet unsaved list the list name will be editable anyway.
	- But when you are working on an already saved list/ recently loaded reference list this input field for the name will be barred ('IsEnabled' is set to false)
		thus preventing users to accidently change the name of a reference list.

- **'Load Selected'**-Button**: **Load an existing list** from the data base **for editing**. You'll have to select a list name from the drop-down-menu (picker) next to the button first.

- **'Save'-Button**: As the name suggests, currently active reference list will be saved to the database.
			If it is en **existing list** this list **will be overwritten.**
			If the reference list doesn't exist in the database a new entry will be created.

- **'Delete'-Button**: As the name suggests, by clicking on this button the currently active list will be deleted.
	- [!Warning]: will **remove the _active reference list_ from the database _permanently_**.


- **'Cite'-Button**: E-Citera has the capability of generating formatted reference lists in accordance with specific citation styles for foot-/endnotes or bibliographies.
	- If there is any active list loaded or freshly created (at runtime) you can **create** such **a formatted list ready for citation _by clicking the 'Cite'-button_ on this page.**


-------------------------------

# How to create bibliographies:

- E-Citera has the capabilty of **generating formatted reference lists for bibliographies** in accordance with specific citation styles.
- To do so, **add titles to a reference list** (see **Adding titles to a reference list**) **or load an existing list from the 'Reference Lists'-Page**.
- When your reference list or bibliography is complete **select a citation style** from the drop-down-menu (picker) **on the 'Reference Lists'-Page**.
- Then _click the **'Cite'-Button** on the 'Reference Lists'-Page_.


-------------------------------

# CODE FILES AND CODE ARCHITECTURE:

## MAUI-Standard-Files:
The automatically generated files for any MAUI-project will not be discussed here in any detail neither any automatically generated code from the Community Toolkit.
If you encounter code files not mentioned/discussed here you may assume these files are part of the MAUI-standard-structure, see the Microsoft documentation for further information.

## THE SOFTWARE ARCHITECTURE of E-CITERA:

E-Citera largely follows the **MVVM**-design-pattern (**Model-View-ViewModel**).

Therefore 'code-behind' in cs-files that are linked to XAML-files (the 'Views') is reduced to a minimum if there is any code at all.

Instead the content ('what to display') of the UI designed with XAML ('how to display') and notification of classes handling the back-end (e. g. database operations)
and/or performing operations on the model-classes is mediated via View-Models.

- The Views (XAML and XAML.cs) are to be found in the 'Views'-folder of the project.
- The View-Models are to be found in the 'ViewModels'-folder of the project.
- The Models, namely the 'Title' and the 'Citation Style'-class, are to be found in the 'Models'-folder.


In accordance with this design pattern every **'View' = 'ContentPage'** of E-Citera **corresponds to a View-Model** (defining what to display on UI and how to handle user input):

**'MainPage.XAML/MainPage.XAML.cs'** corresponds with **'TitleView.cs'**

**'TitleEditPage.XAML/TitleEditPage.XAML.cs'** corresponds with **'TitleEditView.cs'**

**'CitationStylesPage.XAML/CitationStylesPage.XAML.cs'** corresponds with **'CitationStyleView.cs'**

**'ReferenceListPage.XAML/ReferenceListPage.XAML.cs'** corresponds with **'ReferenceListView.cs'**

------------------

## The MainPage ('Title Overview') and the 'TitleView'-ViewModel

- The MainPage, titled 'Title Overview', displays all of the titles currently in the data base.
- TitleView fetches the Titles from the database and keeps it as a runtime List (ObservableCollection).

- If the objects in the database change, namely if titles are added or removed, TitleView will be informed via the Messaging-Service of .NET-MAUI and receiving the 'AddItemMessage' or the 'ItemDeletedMessage'.

- It then updates the runtime list by fetching current state of the database and forwards it to MainPage (after the list is updated 'ShouldRefresh' is set to true, so the 'RefreshView' updates to the current state of the TitleList).

- MainPage allows for searching the titles in the database (currently by 'ItemTitle' and 'Author').

	- The Search (user input in the Search-Bar) is handled by member methods of TitleView.
	- Search-Results are kept as separate ObservableCollection to fill the ListView on MainPage bound to the Search-Results and also for not messing with the original list of type Title corresponding with database-objects.

- From MainPage users will be forwarded to the sub-pages to perform different operations (see 'How to Use E-Citera') on or with the Titles:

	- Editing Titles -> TitleEditPage - handled by TitleEditView
	- Adding Titles to Reference Lists -> ReferenceListPage - handled by ReferenceListView

- This forwarding is handled by TitleView via navigation commands (bound to UI-Elements on the MainPage, (for those elements see 'How to Use E-Citera'))
  which also passes the data needed (objects of type 'Title') to TitleEditView or ReferenceListView.

- From the MainPage Titles can also be deleted, which is handled by TitleView and forwarded to the 'DB_Handler'-class

------------------

## The TitleEditPage (titled just 'Edit' on UI) and the 'TitleEditView'-ViewModel

- Title-Edit-Page displays the main mask for creating new titles and changing existing ones
- Title-Edit-View handles the user-input on this mask and serves as an interface to the database.

- It contains member methods to handle a single Title-Runtime-Object (a loaded title or a newly created one) and forward this object to the DB_Handler to perform the actual CRUD-Operations on the different database tables.[^9]

- Its most important method is the Save()-function which is also a RelayCommand bound to the Save-Button on the TitleEditPage.
- This method delegates the different parts of the runtime-object Authors/Editors, the actual Title and Series to the respective methods of DB_Handler.
- For each of these parts it retrieves information from the DB_Handler-class whether these parts already correspond to objects in the different tables of the database,
  the authors-table, the title-table, the series-table and the table for linking authors to titles and if so to update the table(-objects) or else let DB_Handler create new entries in these tables.

- Title-Edit-View also has also a Delete-Command-Method to notify the DB_Handler that the currently loaded title should be removed permanently from the database (or the database equivalent(s) of 'CurrentTitle').

- Title-Edit-Page also allows for the citation of a single title -> Title-Edit-View passes the 'CurrentTitle'-Object/ instance to Citation-Handler to receive a formatted citation-string in return.

------------------
[^9]: See the sections about 'DatabaseTables.cs' and 'DB_Handler.cs'

------------------

## The CitationStylesPage and the 'CitationStylesView'-ViewModel:

- The Citation-Styles-Page is more or less a standalone page where new citation styles can be created or existing ones changed (loaded and opened them for editing) or deleted if not needed anymore.

- Citation-Style-View handles the user-input on the Page (its bound member properties) and forwards the runtime instance of a CitationStyle-object to the DB_Handler-class which performs the actual CRUD-operations on the database-equivalent.

- If a new citation style is created Citation-Style-View notifies Title-Edit-View and Reference-List-View via the 'StylesChangedMessage', so they may make the new citation style available for citing:

	- Basically, at first the names of existing citation styles corresponding to their 'styleNames'-member are updated which are bound the respective picker-selection-menus on these pages.
	- These makes the new style available for selection on the corresponding pages and thus passing instances of Title-Objects to Citation-Handler for conversion to formatted citation-strings.


------------------


## The ReferenceListPage and the 'ReferenceListView'-ViewModel:

- The Reference-List-Page displays a collection of Title-Objects which are either passed to it (one at a time) from the MainPage-TitleView or loaded and opened for editing from the database via DB_Handler.

- In accordance with this Reference-List-View receives these Title-Objects via injection* or sends a request to DB_Handler to retrieve the equivalent database-objects which are than converted to runtime-objects by 'TitleConverter'.

- *: Reference-List-View has a member inheriting from 'Observable-Property' called 'lastInjectTitle'.

	- Any Title-Object received from the MainPage-TitleView via the _GoToCommand_ is first injected as value of this member property into the instance of 'ReferenceListView' held by its parent-page.
	- Then it also added to the ObservableCollection 'ReferenceListTitles' which serves as the 'ItemSource' for 'ListView' on the Reference-List-Page responsable for displaying the collection.

- Besides of loading a list of Title-Objects serving as a 'Reference-List' Reference-List-View also contains member methods to notify DB_Handler whether the database equivalent of a runtime-list should be created or updated.
- Updating in this case includes notifying DB_Handler that some entries shall be removed because they do not belong to the current 'ReferenceList' anymore.
- All of this happens when the 'SaveRefenceList()'-method is called which is also a Command bound the equivalent button on the Reference-List_Page.

- Titles may be removed from the current 'Reference List' by clicking on the 'X'-Button next to each single item of the ListView on Reference-List-Page which will at first only remove the object from the runtime-list.
	- The database-equivalent of any removed title-objects will only be removed from the database when the changes are saved via the SaveReferenceList()-Command.

- Reference-List-View also entails a method/Command for deleting the entire Reference-List - a corresponding request will be sent to DB_Handler (after the user is asked if they are sure they want to delete the entire list).

- Finally, Reference-List-View has 'CiteReferenceList()'-method/Command which will upon call pass each Title-object in the current Reference-List (an ObservableCollection of Title-Objects) to 'CitationHandler'
- A concatenated string of formatted citation strings is created and passed to 'CitationOut' which is bound to an 'Editor'-View on Reference-List-Page.


------------------


## The Data-Models of 'E-Citera':


### The 'Title'-class:

- Is the most important class of this app since its fields define the essential properties of a 'Title' which in the context of this app means the virtual representation of 'a piece of literature'.
- Instances of this class are needed all across this app and passed between its parts.
- Due to the nature of this app most of its member properties are strings (with the only exception of 'YearOfPublication'):

	- As literature is about the written word - strings, sequences of characters - so is the meta data representing them.
	- In other parts of this Readme-File and in the comments in the source code you will explanations of why also fields like pages are not treated as generic numeric fields.

### Authors:

- In the same file as the 'Title'-class you will find another class called 'Author' (defining an ID, a first name and a last name as its member properties).
- Since 'Authors' - which can also represent editors, so persons associated with 'pieces of literature' - are a crucial part of every 'Title'-object I kept them in the same file.

- At least 'Author' could have also been a simple struct, but for the bindings of a MAUI-app to work 'Author' and 'Title' had to be classes.

- Nested properties - such as Author - have also have to be declared 'observable' otherwise MAUI doesn't treat everything that is not a generic type very well
  or more plainly: at least two-way-bindings to these nested properties do not work. (getting and setting the values on the ViewModel properties)

### The 'CitationStyle'-class:

- See 'What is a Citation-Style?' for a detailed definition of what this class represents:

	- A way to order and structure the meta-data of instances of the 'Title'-class for an output-string which users may use in citations in reference lists (bibliographies),
	  foot- or endnotes.

- The 'CitationStyle'-class serves as the model for producing a formatted strings - the data following the pattern of the values stored in instances of this class.

### The 'DatabaseTables'-class:

- 'DatabaseTables' contains the class-definitions for the objects which should be stored in the database of this app (which therefore entails the table-defintions).

- So in a way 'DatabaseTables' also defines models, but, since they are somewhat out of the MVVM-pattern - the database being another layer in the structure of the app -
  which is less closely related to Views and ViewModels I decided (for now) to keep 'DatabaseTables.cs' outside of the 'Models'-folder.

'DatabaseTables' defines object-models for the tables 'Authors', 'AuthorsAndTitles', 'Titles', 'Series' and 'Reference_Lists'.

- The tables 'Authors', 'Titles', 'Series' store generic data for different parts of a 'Title'-runtime-object.

- 'AuthorsAndTitles' and 'Reference_Lists' store the links (relations) between those different parts.

	- The 'AuthorsAndTitles'-Table links the objects of the 'Authors'-Table to specific 'Titles' ('TitleTableObj'(ects)).
	- The 'Reference_Lists'-Table links a number of 'Titles' to a reference list.

- Finally, there is another database-table for saving 'CitationStyles' (accordingly named).
- Here, the declaration is to be found in the definition of the 'CitationStyle'-class since in this case the runtime-object and the database-object have an identical structure (in contrast to the other table objects).

## Helpers and Services:

### DB_Handler:

- As the name suggests, DB_Handler handles all essential CRUD-Operations and requests on the database.


### CitationHandler:

- Receives objects of type 'Title' and converts them into formatted strings following the pattern of a 'CitationStyle' (see above for further information).

- Also declares and defines a number of readonly-strings which help the 'CitationStylesView'-ViewModel to structure the Citation-Styles-Page

- [!Note] If anyone wonders why these are declared as strings and not just a 'char'-array:

[!Quote]:
	"You cannot have const arrays in C#.[^10] That is one of the limitations.
	You can make the variable that stores an array readonly so it cannot be pointed to something else.
	The size of the array is fixed at creation time (it is an attribute of the type, not like C--) so it also cannot be resized without creating a new array.
	But the contents of an array is writable and there is nothing you can do about it."

Contribution of Michael Taylor on "learn.microsoft.com" replying to the following request:

https://learn.microsoft.com/en-us/answers/questions/978752/const-arrays-in-functions
-------------------
[^10]: And this is exactly what I would have needed in this case - the author of this Readme-file.

-------------------

### TitleConverter:
- TitleConverter serves the purpose of converting the elements of the runtime-object 'Title' (instance of this class) to objects which will be stored in the database and vice versa.
- See: 'DatabaseTables', 'The 'Title'-class'' and the comments in the source-code-files.
