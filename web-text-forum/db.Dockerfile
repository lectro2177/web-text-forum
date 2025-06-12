# Use the official SQL Server image
FROM mcr.microsoft.com/mssql/server:latest AS sqlserver
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=enforcement_nonillions_cadagis_01

# Add optional environment variable for database name
ENV MSSQL_DBNAME=webtextforum_db