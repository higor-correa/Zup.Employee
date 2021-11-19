docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=employee@123" -p 1433:1433 --name EmployeeDB -d mcr.microsoft.com/mssql/server:2019-latest
