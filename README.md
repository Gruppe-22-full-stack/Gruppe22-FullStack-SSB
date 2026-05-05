\# StatistikkApp



\## Beskrivelse



StatistikkApp er en fullstack ASP.NET Core MVC-applikasjon laget for å vise statistikkdata fra SSB. Applikasjonen bruker MVC-designmønsteret med modeller, views og controllere.



Brukeren kan opprette, lese, redigere og slette statistikkdata. Data lagres i en SQLite-database ved hjelp av Entity Framework Core. Applikasjonen har også filtrering, grafvisning med Chart.js, API-integrasjon mot SSB og innlogging med ASP.NET Core Identity.



\## Teknologier brukt



\* C#

\* ASP.NET Core MVC

\* Entity Framework Core

\* SQLite

\* ASP.NET Core Identity

\* Bootstrap

\* Chart.js

\* xUnit

\* Git og GitHub



\## Funksjonalitet



\* CRUD for statistikkdata

\* Filtrering på kommune og kategori

\* Import av data fra SSB via service

\* Graf som viser utvikling over tid

\* Brukerregistrering og innlogging

\* Unit test med xUnit



\## Database



Prosjektet bruker SQLite og Entity Framework Core.



Kommandoer brukt:



dotnet ef migrations add InitialCreate

dotnet ef database update

dotnet ef migrations add AddIdentity

dotnet ef database update



\## Innlogging



Applikasjonen bruker ASP.NET Core Identity.



Testbruker:



E-post: test@test.no

Passord: Test123!





\## Testing



Kjør tester med:



dotnet test StatistikkApp.Tests/StatistikkApp.Tests.csproj



Forventet resultat:

Passed: 1

Failed: 0



\## Kjøring av prosjektet



cd StatistikkApp

dotnet run



Åpne nettleser på adressen som vises i terminalen.



\## Gitflow



Prosjektet bruker:



\* main

\* dev

\* extraFeature



Utvikling ble gjort på dev og extraFeature før merge til main.

