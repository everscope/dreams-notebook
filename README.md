  
\# Dreams Notebook

Dreams Notebook is an ASP.NET Core MVC website that provides abilities for makings notes, reading, sorting, sharing them by link, and authentication.

\## How to run

Go to the folder where you want to save the project and open the Command line there (or press Win + R, enter cmd, and click run, with cd \<path-name> command move to wanted folder).

\*\*\_Notice, that you have to have Git installed on your PC\_\*\*

In console, print:

   git clone https://github.com/everscope/dreams-notebook.git

After the project has been cloned, print this:

   cd dreams-notebook/DreamWeb   
   dotnet run

In the console, among logs, you will be able to find this text with the address:

   Microsoft.Hosting.Lifetime\[14\]  
   Now listening on: https://localhost:7174  
   info: Microsoft.Hosting.Lifetime\[14\]  
   Now listening on: http://localhost:5174  
   info: Microsoft.Hosting.Lifetime\[0\]

Open your browser and go to the first address (in this case \`localhost:7174\`) or second address (\`localhost:5174\`). (notice, that in your case your ports can be different)

When you finish, go back to the console and press Ctrl+C to stop the application.

Also, you can test the application without downloading it, it could be accessed by link: https://dreamsnotebook.social

\## Used technologies

\- ASP.NET Core  
    
   
\- Entity Framework

    
 

\- Razor Pages

\- HTML + CSS + JQuerry

\- XUnit

    
 

\- FluentAssertions

\## Project Architecture:

The solution contains 2 folders, one of them is a web application, other is tests. Tests are written with XUnit and FluentAssertions.

Web application contains 2 controllers, \`HomeController.cs\` and \`UserController.cs.\` HomeController is responsible for showing the Index (Main) page and FAQ, UserController is responsible for all actions contained on the user page (sorting, adding/reading/removing notes). Authentification is implemented with Identity and Razor pages.

DAL consists of \`DatabaseReaderSQL\` (for work with EntityFramework) which implements \`IDatabaseReader\`, and two entities: \`UserAccount\` (inherits Identity User) and \`DreamPublication\`. Using a Database reader as an interface gives the ability to easily move to other Databases or another framework. \`DreamPublication\` sorting and \`IDreamPublication\` are used to sort publications by keywords or date and order them by length or date. \`DreamContextConverter\` is a static class with two methods, which are used to convert an array of strings (received from \`DreamsInputModel\`) to one string (to store in the database). \`EmailService\` implements \`IEmailService\` and provides sanding messages for password recovery.
