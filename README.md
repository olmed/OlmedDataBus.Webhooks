# OlmedDataBus.Webhooks.Client

## Opis

`OlmedDataBus.Webhooks.Client` to biblioteka .NET umo¿liwiaj¹ca bezpieczn¹ obs³ugê webhooków pochodz¹cych z szyny danych OLMED. Umo¿liwia ona:
- weryfikacjê podpisu HMAC SHA256 przesy³anych danych,
- odszyfrowanie zaszyfrowanego payloadu (AES-256-CBC),
- prost¹ integracjê z aplikacjami .NET (np. ASP.NET Core, Windows Service, itp.).

Dziêki tej bibliotece partnerzy OLMED mog¹ w ³atwy sposób odbieraæ, weryfikowaæ i odszyfrowywaæ dane przesy³ane przez webhooki.

---

## Przyk³adowe u¿ycie (na podstawie projektu TestHost)

Poni¿ej znajduje siê uproszczony przyk³ad u¿ycia biblioteki w projekcie ASP.NET Core Web API (`OlmedDataBus.Webhooks.TestHost`):

---

## Instrukcja do³¹czenia biblioteki do projektu

### 1. Dodanie przez NuGet (zalecane, jeœli biblioteka jest publikowana)

W Mened¿erze pakietów NuGet wyszukaj `OlmedDataBus.Webhooks.Client` i zainstaluj do swojego projektu.

**lub** przez konsolê:

### 2. Rêczne do³¹czenie pliku DLL

Jeœli posiadasz tylko plik `OlmedDataBus.Webhooks.Client.dll`:

1. Skopiuj plik DLL do katalogu swojego projektu (np. do folderu `libs`).
2. Kliknij prawym przyciskiem myszy na projekt w Visual Studio ? **Dodaj** ? **Odwo³anie...**.
3. Wybierz **Przegl¹daj** i wska¿ plik DLL.
4. ZatwierdŸ dodanie odwo³ania.

---

## Konfiguracja kluczy

W pliku `appsettings.json` lub innym Ÿródle konfiguracji nale¿y umieœciæ klucze:
Klucze powinny byæ przekazane w formacie Base64 i mieæ d³ugoœæ 32 bajtów (256 bitów).

---

## Wymagania

- .NET Standard 2.0 (kompatybilnoœæ z .NET Core 2.0+, .NET Framework 4.6.1+, .NET 5/6/7/8)
- Do testowania: ASP.NET Core Web API (np. .NET 8)

---

## Licencja

Biblioteka przeznaczona do u¿ytku partnerów Grupy OLMED.