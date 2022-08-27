To build the database, in Package Manager Console, run Update-Database and this will create the database from the migration file saved in the Migrations folder of the API.
API should be the default startup project so it can see the migration file.
The database that is created is called NotesDb and should be created as a local SQL .mdf file and is configured in appsettings.json

Running the solution should start up both the API and Web projects together in seperate browser windows.
By default, the API URL should be https://localhost:7245/, if this is different, please update the bottom line in site.js in the web project to the new URL.

No notes or categories are seeded by default as checks are made for empty tables.
They can be created on the web page with the New Category button and then are loaded dyamically in when you click on New Note.

Logging is saved as txt file in the Logs folder and is captured for every request made to the API.

All external packages were installed via nuget
