# This is a sample Python script.
import base64
import json
import os

import requests
from requests.packages.urllib3.exceptSions import InsecureRequestWarning
import pandas as pd

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.

PACKAGE_DEFINITION = ['DOCHODY.XML', 'EGZAMINY.XLSX',
                      'ETATY.XML', 'SZKOŁY.XML', 'WYDATKI.XML']

ROOT_DIR = 'data'


def update_data():

    with os.scandir(ROOT_DIR) as year_dir:
        dirs = [i.name for i in year_dir if i.is_dir()]
        for directory in dirs:
            if directory.isdigit():
                year = int(directory)
                with os.scandir(os.path.join(ROOT_DIR, directory)) as content_dir:
                    content = [i.name.upper()
                               for i in content_dir if i.is_file()]
                    if set(content) == set(PACKAGE_DEFINITION):
                        print('Znaleziono rok:', year)
                        PullFiles(year, os.path.join(ROOT_DIR, directory))
                    else:
                        print('Niepełne dane dla roku:', year)


def PullFiles(year, path):

    # Disable SSL certificate verification warning (not recommended for production)
    requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

    # Read files
    with open(os.path.join(path, "DOCHODY.XML"), 'rb') as file_t:
        # read the file as xxd do
        blob_data1 = file_t.read()
        # print(blob_data1)

    with open(os.path.join(path, "EGZAMINY.XLSX"), 'rb') as file_t:
        blob_data2 = file_t.read()
        # print(blob_data2)

    with open(os.path.join(path, "ETATY.XML"), 'rb') as file_t:
        blob_data3 = bytearray(file_t.read())
        # print(blob_data3)

    with open(os.path.join(path, "SZKOŁY.XML"), 'rb') as file_t:
        blob_data4 = bytearray(file_t.read())
        # print(blob_data4)

    with open(os.path.join(path, "WYDATKI.XML"), 'rb') as file_t:
        blob_data5 = bytearray(file_t.read())
        # print(blob_data5)

    url = "http://localhost:5286/UploadSchoolData"

    x = {"year": year, "schoolsworksheet": base64.b64encode(
        blob_data4).decode(), "jobsworksheet":  base64.b64encode(blob_data3).decode()}
    # print(x)

    payload = json.dumps(x)
    headers = {
        'Content-Type': 'application/json'
    }

    response = requests.request("POST", url, headers=headers, data=payload)

    x = {"year": year, "incomesummary": base64.b64encode(
        blob_data1).decode(), "costssummary":  base64.b64encode(blob_data5).decode()}
    payload = json.dumps(x)

    url = "http://localhost:5286/UploadFinancialData"
    response = requests.request("POST", url, headers=headers, data=payload)


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    update_data()

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
