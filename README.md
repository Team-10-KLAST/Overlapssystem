# Overlapssystem – Bostedet Slottet

Overlapssystemet fungerer som et digitalt overblik til bostedet Slottet, der erstatter deres eksisterende papirbaserede system. Det digitaliserede system giver personalet et fælles digitalt overblik over alle bostedets borgere i et borgerskema som viser borgerens faste medicin, PN, risikovurdering for vagten (trafiklys), pårørendekontakt, handledag, betalingsmetode samt status. Derudover har de nedenunder en overbliksrubrik der viser afdelingens opgaver, arbejdstelefoner og en huskeseddel.

Systemet opdateres manuelt af personalet løbende i vagten — det er ikke et realtidssystem, men et fælles digitalt overblik der holdes ajour af personalet selv så rettidigt som muligt. Vigtige oplysninger fra borgerskemaerne logges og kan af en administrator hentes frem igen.

Fordelen ved at overgå til det digitale system er et hurtigere overblik, sikring og beskyttelse af data, historik, tidsbesparende samt væsentlig formindsket risiko for fejl.

---

## Indholdsfortegnelse

- [Systembeskrivelse](#systembeskrivelse)
- [Krav til opsætning](#krav-til-opsætning)
- [Fremtidig brug](#fremtidig-brug)
- [Sikkerhed og adgangskontrol](#sikkerhed-og-adgangskontrol)
- [Udvidelsesmuligheder](#udvidelsesmuligheder)
- [CICD](#cicd)
- [Teknologier](#teknologier)
- [Implementering og ibrugtagning](#implementering-og-ibrugtagning)

---

## Systembeskrivelse

Systemet vises på en stor fladskærm og bruges af personalet under deres vagt og ved vagtskifte. Det er bygget til to afdelinger — Skoven og Slottet — og understøtter tre vagttyper: dagvagt, aftenvagt og nattevagt. Derudover er der opdelt vha. roller hvad man som medarbejder og admin har adgang til at ændre af data i systemet.

### Kernefunktioner

- Borgeroverblik med initialer, trafiklys (risikovurdering), medicin, pårørendekontakt og status/generel felt
- Faste og PN-medicintider per borger
- Medarbejdertildeling per borger (primær og sekundær)
- Log af vigtig borgerinformation fra en vagt som senere kan hentes: risikovurdering/trafiklys, fast medicin, PN og statusfelt
- Afdelingsopgaver med ansvarsfordeling per vagt (fx medicinansvar, hygiejne, aftensmad, etc.)
- Huskeseddel per afdeling — følger med på tværs af vagter
- Vagtinfo med dato, vagttype og afdeling i topbaren
- Data overføres automatisk til næste vagt — ingen blank start ved vagtskifte
- JWT-baseret login med roller (administrator og almindeligt personale)
- Admin kan ligesom employee opdatere i overlapsskemaet og se tidligere overlapsskemaer, men admin har desuden rettigheder til at se logs for ændringer i medicin, PN, status og trafiklys

### Arkitektur

Systemet er bygget ud fra Clean Architecture og består af følgende lag:

```
Overlapssystem.Api           → REST API (controllers, mappers)
Overlapssystem.Application   → Interfaces og services (forretningslogik)
Overlapssystem.Domain        → Entiteter og enums
Overlapssystem.Infrastructure → Database, Identity, Migrations
Overlapssystem.Shared        → DTOs delt mellem API og klient
Overlapssystem.BlazorClient  → Blazor WebAssembly frontend
Overlapssystem.Test          → Tests
```

---

## Krav til opsætning

### Forudsætninger

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) eller nyere (anbefalet)

### Trin 1 – Klon projektet

```bash
git clone https://github.com/Team-10-KLAST/Overlapssystem.git
cd Overlapssystem
```

### Trin 2 – Start databasen

Sørg for at Docker Desktop kører, og kør følgende kommando fra roden af projektet:

```bash
docker-compose up -d
```

### Trin 3 – Kør migrations

```bash
dotnet ef database update --project Overlapssystem.Infrastructure/Overlapssystem.Infrastructure.csproj --startup-project Overlapssystem.API/Overlapssystem.API.csproj
```

Dette opretter databasen og seeder to afdelinger, 16 borgere, medicintider, faste afdelingsopgaver og standardbrugere.

### Trin 4 – Start projektet

1. Højreklik på solution i Solution Explorer
2. Vælg **Set Startup Projects**
3. Vælg **Multiple startup projects** og sæt begge til **Start**
4. Kør projektet med **F5**

---

## Fremtidig brug

Projektet bruger **Docker Compose** til at orkestrere databasen, hvilket sikrer et ensartet udviklingsmiljø på tværs af teamet uden manuel installation af SQL Server.

Fremover skal du derfor:

- Have Docker Desktop kørende hver gang du vil bruge systemet
- Pulle og køre kommandoen i trin 3 igen når en i gruppen laver nye migrations og pusher til GitHub
- Stoppe databasen med `docker-compose down` — data bevares
- Slette databasen fuldstændigt med `docker-compose down -v` (bruges kun hvis du vil starte forfra)

---

## Sikkerhed og adgangskontrol

### JWT-baseret authentication

Systemet bruger JSON Web Tokens (JWT) til authentication. Tokenet udløber efter **30 minutter** og er konfigureret i `appsettings.Development.json`.

> **Vigtigt:** `SecretKey` må aldrig ligge i kildekoden i produktion. Brug environment variables eller en secret manager.

### Roller

| Rolle | Beskrivelse |
|-------|-------------|
| `Employee` | Begrænset adgang — kan se og udfylde i overlapsskemaer |
| `Admin` | Fuld adgang — kan også se logs for ændringer |

### Standardbrugere (seed)

| Rolle | Email | Password |
|-------|-------|----------|
| Employee | anna@example.com | abc123 |
| Admin | admin@example.com | abc123 |

> Standardadgangskoder bør ændres før systemet tages i brug.

---

## Udvidelsesmuligheder

Systemet er bygget med Clean Architecture og er designet til at kunne udvides uden at ændre eksisterende lag.

**Administratorpanel** — i det nuværende system styres stamdata som borgere, afdelinger og faste opgaver via seed data i databasen. En naturlig udvidelse er et administratorpanel i brugerfladen hvor en bruger med admin-rollen kan oprette, redigere og slette borgere, afdelinger og opgaver direkte — uden at skulle ændre i databasen manuelt.

**Realtidsopdatering** — systemet opdateres i dag manuelt. En naturlig udvidelse er SignalR så ændringer vises live for alle uden at genindlæse siden.

**Historik og rapportering** — data gemmes per vagt men der er ingen brugerflade til at søge i historik. En historik-visning der giver mulighed for at gå 10-30 dage tilbage ville understøtte hændelsessporing og dokumentation.

**Adgangsstyring** — i dag understøttes to roller. Systemet kan udvides med claims-baseret adgangskontrol så fx kun bestemte medarbejdere har adgang til specifikke afdelinger.

**Notifikationer** — medicintider og opgaver kunne udløse notifikationer til personalet via et eksternt notifikationssystem.

**Mobil adgang** — med regionens tilladelse kunne en mobilvenlig visning tilføjes så personalet kan tilgå systemet fra egne enheder.

**Integration til FMK** — en integration til Fælles Medicinkort ville reducere dobbeltregistrering og fejlrisiko.

---

## CI/CD

Projektet har på nuværende tidspunkt ikke et automatiseret CI/CD-pipeline, men følgende er overvejelser for en fremtidig opsætning.

**Continuous Integration** — ved pull requests og pushes til `main` kunne en GitHub Actions workflow automatisk bygge projektet, køre enhedstests og validere at migrations kan køres uden fejl.

**Continuous Deployment** — da systemet kører på en privat server uden internetadgang kræver deployment manuel indsats. En fremtidig løsning kunne være et internt deployment-script der automatisk henter seneste kode, kører migrations og genstarter applikationen.

---

## Teknologier

- **Frontend**: Blazor WebAssembly (.NET 8)
- **Backend**: ASP.NET Core Web API (.NET 8)
- **Database**: SQL Server i Docker
- **Authentication**: ASP.NET Core Identity med JWT
- **Arkitektur**: Clean Architecture

---

## Implementering og ibrugtagning

Systemet er udviklet som en digital erstatning for det eksisterende papirbaserede overlapsskema. Overgangen fra det gamle til det nye system kræver mere end teknisk opsætning — det kræver forberedelse af både hardware, personale og organisation.

**Hardware** — systemet er optimeret til visning på en stor fladskærm via HDMI. Der kræves en dedikeret computer eller mini-PC der kører systemet, forbundet til skærmen der hænger i personalerummet.

**Regionens godkendelse** — da systemet håndterer borgere på et bosted kræver ibrugtagning godkendelse fra regionen — også selvom det kører som et lukket system uden internetadgang. Dette bør afklares inden systemet tages i brug.

**Oplæring af personale** — systemet er designet til at være intuitivt og kræve så få klik som muligt. Det anbefales dog at gennemføre en kort introduktionssession for personalet inden ibrugtagning — særligt med fokus på vagtvalg, trafiklys og huskeseddel. Man kunne evt. vælge en nøgleperson i hver afdeling til dette ansvar.

**Overgangsperiode** — det anbefales at køre det digitale system parallelt med det papirbaserede overlapsskema i en kortere periode, så personalet kan vænne sig til det nye system uden at miste overblikket.
