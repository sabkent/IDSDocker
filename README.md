# IDSDocker

https://mjarosie.github.io/dev/2020/09/24/running-identityserver4-on-docker-with-https.html





# use the host network

https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-5.0
use a docker volumne to mount an exported certificate 
you export the cert create already for you by .NET using 

```
	dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p password
	dotnet dev-certs https --trust	
```

after many tears and much nashing of teeth i've abandoned this as a solution
https://docs.docker.com/network/host/
 -- "The host networking driver only works on Linux hosts, and is not supported on Docker Desktop for Mac, Docker Desktop for Windows, or Docker EE for Windows Server."