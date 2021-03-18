# use certificates bound to localhost and compose dns name
https://mjarosie.github.io/dev/2020/09/24/running-identityserver4-on-docker-with-https.html

you can generate a certificate that binds to both localhost AND the service name (from docker compose)

```
	sudo openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout idserver.key -out idserver.crt -config idserver-ssl.conf --passin pass:password
	sudo openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout site.key -out site.crt -config site-ssl.conf --passin pass:password
	
	sudo openssl pkcs12 -export -out idserver.pfx -inkey idserver.key -in idserver.crt
	sudo openssl pkcs12 -export -out site.pfx -inkey site.key -in site.crt
```

in an enterprise like Checkout or Trainline the Public Key Infrastructure PKI would be setup with certificate authorities and we wouldn't need to:
	import the .crt into the local machine > Trusted Root Certificate Authority

hmm ok this cunt cheated his article only uses client credential flow (there is no redirect to login) and so the issue doesn't present and he had to disable issuer validation
~~making this another non-solution~~

!!!!WORKING!!!!!
if you add your local ip address to the certificate check out idserver-ssl.conf
need to import the idserver cert into the image for site certificate authorities

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