# OlmedDataBus.Webhooks.Client

## Opis

`OlmedDataBus.Webhooks.Client` to biblioteka .NET umo�liwiaj�ca bezpieczn� obs�ug� webhook�w pochodz�cych z szyny danych OLMED. Umo�liwia ona:
- weryfikacj� podpisu HMAC SHA256 przesy�anych danych,
- odszyfrowanie zaszyfrowanego payloadu (AES-256-CBC),
- prost� integracj� z aplikacjami .NET (np. ASP.NET Core, Windows Service, itp.).

Dzi�ki tej bibliotece partnerzy OLMED mog� w �atwy spos�b odbiera�, weryfikowa� i odszyfrowywa� dane przesy�ane przez webhooki.

---

## Przyk�adowe u�ycie (na podstawie projektu TestHost)

Poni�ej znajduje si� uproszczony przyk�ad u�ycia biblioteki w projekcie ASP.NET Core Web API (`OlmedDataBus.Webhooks.TestHost`):

---

## Instrukcja do��czenia biblioteki do projektu

### 1. Dodanie przez NuGet (zalecane, je�li biblioteka jest publikowana)

W Mened�erze pakiet�w NuGet wyszukaj `OlmedDataBus.Webhooks.Client` i zainstaluj do swojego projektu.

**lub** przez konsol�:

### 2. R�czne do��czenie pliku DLL

Je�li posiadasz tylko plik `OlmedDataBus.Webhooks.Client.dll`:

1. Skopiuj plik DLL do katalogu swojego projektu (np. do folderu `libs`).
2. Kliknij prawym przyciskiem myszy na projekt w Visual Studio ? **Dodaj** ? **Odwo�anie...**.
3. Wybierz **Przegl�daj** i wska� plik DLL.
4. Zatwierd� dodanie odwo�ania.

---

## Konfiguracja kluczy

W pliku `appsettings.json` lub innym �r�dle konfiguracji nale�y umie�ci� klucze:
Klucze powinny by� przekazane w formacie Base64 i mie� d�ugo�� 32 bajt�w (256 bit�w).

---

## Wymagania

- .NET Standard 2.0 (kompatybilno�� z .NET Core 2.0+, .NET Framework 4.6.1+, .NET 5/6/7/8)
- Do testowania: ASP.NET Core Web API (np. .NET 8)

---

## Licencja

Biblioteka przeznaczona do u�ytku partner�w Grupy OLMED.