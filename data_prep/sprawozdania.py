import os
import pandas as pd
from Convert import ConvertSprawozdaniaXMLToCSV

fileName = os.path.join('..', 'docs', 'Sprawozdania[2022][IVKwartał] Dochody.xml')
df = ConvertSprawozdaniaXMLToCSV(fileName,expenses=False)

fileName = os.path.join('..', 'docs', 'Sprawozdania[2022][IVKwartał] Wydatki.xml')
df = ConvertSprawozdaniaXMLToCSV(fileName,expenses=True)

fileName = os.path.join('..', 'docs', 'Sprawozdania[2023][IIKwartał] Dochody.xml')
df = ConvertSprawozdaniaXMLToCSV(fileName,expenses=False)

fileName = os.path.join('..', 'docs', 'Sprawozdania[2023][IIKwartał] Wydatki.xml')
df = ConvertSprawozdaniaXMLToCSV(fileName,expenses=True)



