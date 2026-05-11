# Overlapssystem

## Opsætning af database

### Forudsætninger

- [Docker Desktop](https://www.docker.com/products/docker-desktop) skal være installeret

### Trin 1 – Start databasen

Sørg for at Docker Desktop kører, og kør følgende kommando fra roden af projektet (samme mappe som `.sln`-filen):

```bash
docker-compose up -d
```

### Trin 2 – Kør migrations

Kør følgende kommando fra roden af projektet:

```bash
dotnet ef database update --project Overlapssystem.Infrastructure/Overlapssystem.Infrastructure.csproj --startup-project Overlapssystem.API/Overlapssystem.API.csproj
```

### Trin 3 – Kør projektet

Start projektet fra Visual Studio.

### Fremtidig brug

- Docker Desktop skal køre hver gang du vil bruge systemet
- Når en i gruppen laver nye migrations og pusher til GitHub, skal du pulle og køre kommandoen i trin 2 igen
- Stop databasen med `docker-compose down` – data bevares
- Slet databasen fuldstændigt med `docker-compose down -v` (bruges kun hvis du vil starte forfra)
