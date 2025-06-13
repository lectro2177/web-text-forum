# web-text-forum
A web text forum demo, using C#, ASP.NET etc

How to run locally
------------------
This assumes a clean Windows 11 PC with Git already installed:

1) Clone this repo with Git

2) Download and install Docker Desktop for Windows from: https://www.docker.com/products/docker-desktop/

3) Download and install Visual Studio Community edition from: https://visualstudio.microsoft.com/vs/community/

4) Open the the 'web-text-forum.sln' solution in Visual Studio

5) Make sure your startup item (next to the Run button) has 'docker-compose' selected

6) You may need to grant permissions for Docker to various folders, ports etc. Click on Yes when asked

7) Click on the Run button or press F5 to start debugging

8) This should pull SQL server etc. via Docker, and create your Docker container with 'sqlserver' and 'web-text-forum-c1' inside.

9) If you encounter any errors from Docker which mention "Ports are not available", try this guide:  https://www.herlitz.io/2020/12/01/docker-error-ports-are-not-available-on-windows-10/ ; Alternatively, make sure you don't already have services running on ports 8080 (HTTP), 8081 (HTTPS), 1433 (SQL Server)

9) The DB should then be created and be populated with some test data.

10) Your browser should then launch with the Swagger UI (e.g. on https://localhost:8081/swagger/index.html)

11) You can click on the 'Authorize' button in Swagger (near the top-right), and enter any of the following credentials to a login as a moderator or user:

	11.1) - Username: moderator1 
		  - Password: SuperSecure1024!
	
	11.2) - Username: user1 
		  - Password: MyPetsName2025
	
	11.3) - Username: user2 
		  - Password: MyBirthday1980
	
	11.4) - Username: user3 
		  - Password: TooShort1234



