# BarberBookingApp - Plan projektu

## Cel projektu

Stworzenie aplikacji do rezerwacji wizyt u fryzjera. Projekt ma charakter edukacyjny,
którego głównym celem jest nauka technologii .NET i Angular.

## Technologie

- **Backend**: C# .NET 9 ASP.NET Core
- **Frontend**: Angular 20
- **Baza danych**: Microsoft SQL Server
- **Autentykacja**: JWT + Identity
- **Hosting**: (do rozważenia w przyszłości)

## Role w projekcie

- Programista: [Twoje imię]
- Mentor: GitHub Copilot

## Funkcjonalności MVP

1. **System użytkowników**:

   - Rejestracja i logowanie
   - Role: Klient, Fryzjer, Administrator

2. **Dla klientów**:

   - Przeglądanie dostępnych fryzjerów i usług
   - Sprawdzanie dostępnych terminów
   - Rezerwacja wizyt
   - Historia wizyt

3. **Dla fryzjerów**:

   - Zarządzanie swoim kalendarzem dostępności
   - Przegląd zaplanowanych wizyt
   - Oznaczanie wizyt jako zrealizowane

4. **Dla administratorów**:
   - Zarządzanie kontami fryzjerów
   - Dodawanie/edycja usług fryzjerskich
   - Podstawowe statystyki

## Architektura aplikacji

### Backend (.NET)

- **Clean Architecture**:
  - **Domain** - encje biznesowe, reguły biznesowe
  - **Application** - przypadki użycia, interfejsy repozytoriów
  - **Infrastructure** - implementacje repozytoriów, dostęp do bazy danych
  - **API** - kontrolery, middlewares, konfiguracja

### Frontend (Angular)

- **Modułowa architektura**:
  - **Core** - podstawowe usługi, interceptory HTTP, guards
  - **Shared** - współdzielone komponenty, pipes, directives
  - **Features** - moduły funkcjonalne (np. auth, rezerwacje, zarządzanie)
  - **Layout** - komponenty układu strony
- **Biblioteka komponentów UI**: Angular Material
  - Spójny wygląd aplikacji
  - Gotowe komponenty (formularze, tabele, datepickery, itd.)
  - Wsparcie dla dostępności i RWD

### Model danych

#### Główne encje

1. **User (Użytkownik)**

   - Id, Email, Password (hash), Role
   - Podstawowa encja dla systemu autentykacji

2. **Customer (Klient)**

   - UserId, FirstName, LastName, PhoneNumber
   - Rozszerzenie User dla klientów

3. **Barber (Fryzjer)**

   - UserId, FirstName, LastName, Description, Rating, ImageUrl
   - Rozszerzenie User dla fryzjerów

4. **Service (Usługa fryzjerska)**

   - Id, Name, Description, BasePrice, Duration, ImageUrl
   - Usługi oferowane przez salon

5. **Appointment (Wizyta)**

   - Id, CustomerId, BarberId, ServiceId, DateTime, Status, Price
   - Konkretna wizyta zarezerwowana przez klienta

6. **Schedule (Harmonogram)**

   - Id, BarberId, DayOfWeek, StartTime, EndTime
   - Określa dostępność fryzjera

7. **BarberService**
   - BarberId, ServiceId, Price
   - Łączy fryzjerów z usługami, które oferują

#### Relacje między encjami

- User -> Customer/Barber: One-to-One
- Customer -> Appointment: One-to-Many
- Barber -> Appointment: One-to-Many
- Barber -> Schedule: One-to-Many
- Barber <-> Service: Many-to-Many (przez BarberService)
- Service -> Appointment: One-to-Many

## Harmonogram prac

### Etap 1: Przygotowanie i planowanie

- [x] Określenie wymagań funkcjonalnych
- [x] Zaprojektowanie architektury aplikacji
- [x] Zaprojektowanie modelu danych
- [x] Przygotowanie środowiska deweloperskiego

### Etap 2: Struktura projektu i konfiguracja

- [x] Utworzenie solucji .NET i projektów
- [x] Konfiguracja Entity Framework Core
- [x] Implementacja bazowych klas domenowych
- [x] Utworzenie projektu Angular

### Etap 3: Implementacja backendu

- [ ] System autentykacji i autoryzacji
- [ ] API dla zarządzania użytkownikami
- [ ] API dla zarządzania usługami
- [ ] API dla zarządzania harmonogramem
- [ ] API dla rezerwacji wizyt

### Etap 4: Implementacja frontendu

- [ ] Komponenty layoutu i nawigacji
- [ ] Moduł autentykacji
- [ ] Panel klienta
- [ ] Panel fryzjera
- [ ] Panel administratora

### Etap 5: Integracja i testowanie

- [ ] Integracja backendu z frontendem
- [ ] Testowanie funkcjonalności
- [ ] Poprawki i usprawnienia

### Etap 6: Wdrożenie (opcjonalnie)

- [ ] Przygotowanie środowiska produkcyjnego
- [ ] Deployment aplikacji

## Dodatkowe aspekty projektu

### Organizacja kodu i współpracy

- **Kontrola wersji**: Git + GitHub/Azure DevOps
- **Strategia gałęzi**:
  - `main` - kod produkcyjny
  - `develop` - integracja zmian
  - Feature branches dla nowych funkcji

### Konwencje kodowania

- **.NET**: Microsoft C# Coding Conventions
- **Angular**: Angular Style Guide
- **Nazewnictwo**: PascalCase dla C#, camelCase dla TypeScript/JavaScript

### Strategia testowania

- **Backend**: xUnit dla testów jednostkowych, testów integracyjnych
- **Frontend**: Jasmine/Karma dla komponentów
- **Podejście**: TDD tam, gdzie to możliwe

### Dokumentacja

- Dokumentacja API (Swagger)
- Dokumentacja kodu (XML Comments w C#, JSDoc w TS)
- Instrukcja użytkownika (opcjonalnie)

### Narzędzia deweloperskie

- Visual Studio 2022/VS Code
- SQL Server Management Studio
- Postman/Insomnia do testowania API
- Rozszerzenia VS Code (zalecane):
  - C# Dev Kit
  - Angular Language Service
  - ESLint/TSLint
  - SQL Server

## Potencjalne wyzwania i ryzyka

1. **System rezerwacji terminów**:

   - Obsługa nakładających się terminów
   - Efektywne wyszukiwanie dostępnych slotów czasowych
   - Blokowanie terminów podczas procesu rezerwacji

2. **Autentykacja i autoryzacja**:

   - Bezpieczne zarządzanie tokenami JWT
   - Wdrożenie różnych poziomów dostępu dla ról
   - Odświeżanie tokenów bez wylogowywania użytkownika

3. **UI/UX**:

   - Intuicyjny kalendarz rezerwacji
   - Responsywność na urządzeniach mobilnych
   - Czytelna prezentacja dostępności fryzjerów

4. **Wydajność**:

   - Optymalizacja zapytań do bazy danych
   - Paginacja i lazy loading danych
   - Cachowanie częstych zapytań

5. **Testowanie**:
   - Pokrycie różnych scenariuszy rezerwacji
   - Symulacja konfliktów czasowych
   - Testy równoległych rezerwacji
