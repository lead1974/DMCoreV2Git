1. Setup MailKit
2. Re-structure project (Models -> ViewModels, add DataAccess)
3. create DB locally in App_Data folder:
    a. run your app and create the DB in the default location
    b. Open Microsoft Sql Server Management Studio (or you prefer IDE) and create a new connection to point to (localdb)\mssqllocaldb
    c. Script Database as CREATE
    d. Change the path in the FILENAME
    e. Remove the DB you created on step 1 (choose close existing connections)
    f. Run the script
Migration:
a. PM> cd C:\Andrey\Notes\VF\WebApps\labweb\DMCoreV2\src\DMCoreV2
b. dotnet ef
