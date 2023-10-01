# HACKYEAH 2023

## Projekt: HackSQL: Kutno - uDane kształcenie złotówki

Dashboard z danymi znajduje się w folderze dashboard.
Można zaktualizować dane używając skryptu dataloader.
Przetwarzanie odbywa się na zewnętrznym serwerze (docker API).

### Struktura projektu

- Ekstrakcja danych
- API
- Frontend

Aby uruchomić skrypt tworzący kontener Docker z API należy użyć skryptu api\KutnoAPI\Properties\docker.ps1

Dataloader to skrypt przeznaczony do aktualizacji danych umieszczonych w folderach grupujących dane z jednego roku oraz odpowiednio nazwanych. Aby uruchomić skrypt należy użyć komendy w folderze loadera: venv\Scripts\python.py main.py

[ENG]

The dashboard with data is located in the dashboard folder.
You can update the data using the dataloader script.
Processing takes place on an external server (docker API).

### Project structure

- Data extraction
- API
- Frontend

To run the script that creates a Docker container from the API, use the script api\KutnoAPI\Properties\docker.ps1
\
Dataloader is a script designed to update data placed in folders collecting appropriately named data from one year. To run the script, use the command in the loader folder: venvScriptspython.py main.py
