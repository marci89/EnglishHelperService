# EnglishHelperService

With this free application, you can learn English words more easily than before. After registration, you can create any words you like and practice them in four different ways.
This is the application's back-end part.

## Setup Requirements
- You need .NET 6
- Clone the repository.
- Configure the connection string for database.
- EnglishHelper is the front-end part, so you have to use that, too.

## Appsettings structure
- ApplicationSettings.ClientDomain = your frontend url
- ApplicationSettings.ApplicationName = your app name
- SecuritySettings.TokenKey = required for jwt token generating
- SecuritySettings.TokenExpirationDays = Token expiration days
- LogSettings.LogFilesPath = log file location
- DatabaseSettings.Provider = databes type (PostgreSQL, MSSQL, SQLite)
- DatabaseSettings.ConnectionString = database ConnectionString
- DatabaseSettings.AutoMigrationEnabled = allowed to generate migration update automatically
- EmailSettings = your email settings (sender email)

## Key Features
- code first approach for database
- PostgreSQL, MSSQL, SQLite database choosing
- Repository and unitofwork pattern
- Database seeding
- Swagger
- JWT token auth
- Password hash
- Role management
- Email sending
- Export and import txt and excel files
- User activity watcher
- Logging
- Unique error message handling
