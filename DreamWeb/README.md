# dreams-notebook
Dreams noteboos is ASP.NET Core MVC project, that provides abilities to keep your dream journal. It has authentification with login and password, password recovery by email.
Main activity is kept on the User page. User can create notes, delete, sort and share them with other people by link.

How to run a project

To run a project you have to add connection to your database in appsettins.json and update your database with entityframe work, then run it.

About the stucture

There are two conrollers: Home and User. Home only gives main page and faq page. 
User controller works with user page and works with requests from it (like sorting, adding new note).
In models, DreamsContext is main context class which includes DreamPublication and UserAccount. Email service is used by Identity to send password reset email.
DreamInput gets input data of new note and works with it, adds it to db or deletes it.

