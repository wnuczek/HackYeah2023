import pandas as pd
from lxml import etree
import os
import csv

def ConvertExcelXmlToDf(fileName, sheetName):
    # Read the XML data from the file
    tree = etree.parse(fileName)
    root = tree.getroot()

    # Define the namespace mapping
    namespace_mapping = {
        'ss': 'urn:schemas-microsoft-com:office:spreadsheet',
        'excel': 'urn:schemas-microsoft-com:office:excel',
        'html': 'http://www.w3.org/TR/REC-html40'
    }

    # Construct an XPath expression to select the rows from the specified worksheet
    xpath_expression = f'//ss:Worksheet[@ss:Name="{sheetName}"]//ss:Row'

    # Extract the data for the specified worksheet
    data = []
    for row in root.xpath(xpath_expression, namespaces=namespace_mapping):
        row_data = []
        for cell in row.xpath('.//ss:Data', namespaces=namespace_mapping):
            row_data.append(cell.text)
        data.append(row_data)

    # Create a Pandas DataFrame from the extracted data
    df = pd.DataFrame(data)
    return df


def ConvertSprawozdaniaXmlToDf(fileName, expenses=True):
    # Read the XML data from the file
    tree = etree.parse(fileName)
    root = tree.getroot()

    # Extract the data for the specified worksheet
    data = []

    jednostkaTags = ['Nazwa', 'Typ', 'Regon', 'WK', 'PK', 'GK', 'GT', 'PT']
    okresTags = ['Rok', 'TypOkresu', 'Okres']
    pozycjaTags = ['Dzial', 'Rozdzial',
                   'Paragraf', 'P4', 'PL']

    if expenses is True:
        pozycjaTags += ['ZA', 'WW']
    else:
        pozycjaTags += ['NA', 'DW']

    headerRow = jednostkaTags + ['id'] + okresTags + pozycjaTags

    for jednostka in root.xpath('Jednostki/Jednostka'):
        row_data = []
        print('\n\n\n')
        for tag in jednostkaTags:
            for cell in jednostka.xpath(tag):
                print(f'{cell.tag}: {cell.text}')
                row_data.append(cell.text)

        if expenses is True:
            sprawozdanieTag = 'Rb-28s'
        else:
            sprawozdanieTag = 'Rb-27s'

        for sprawozdanie in jednostka.xpath(f'Sprawozdania/{sprawozdanieTag}'):
            sprawozdanieId = sprawozdanie.attrib.get('Id')
            print(f'Id: {sprawozdanieId}')
            row_data.append(sprawozdanieId)

            for okres in sprawozdanie.xpath('Okres'):
                for tag in okresTags:
                    for cell in okres.xpath(tag):
                        print(f'{cell.tag}: {cell.text}')
                        row_data.append(cell.text)

            for pozycja in sprawozdanie.xpath('Pozycje/Pozycja'):
                current_row = row_data
                pozycja_row = []
                for tag in pozycjaTags:

                    for cell in pozycja.xpath(tag):
                        print(f'{cell.tag}: {cell.text}')
                        pozycja_row.append(cell.text)

                data.append(current_row + pozycja_row)

    # Create a Pandas DataFrame from the extracted data
    df = pd.DataFrame(data)
    df.columns = headerRow
    return df


def ConvertSprawozdaniaXMLToCSV(filename, expenses):
    df = ConvertSprawozdaniaXmlToDf(filename, expenses)
    outputFileName = os.path.split(
        filename)[-1].replace('.xml', '.csv')
    outputFilePath = os.path.join('output', outputFileName)
    df.to_csv(outputFilePath, quoting=csv.QUOTE_ALL)


def ConvertXMLToCSV(fileName, sheetName, headerRow):
    # convert xml to pandas dataframe
    df = ConvertExcelXmlToDf(fileName, sheetName)
    outputFileName = os.path.split(
        fileName)[-1].replace('.xml', f'_{sheetName}.csv')
    print(f'Filename: {outputFileName}')

    # cut header rows
    df.columns = df.iloc[headerRow]
    df = df[headerRow+1:]

    # save dataframe as csv
    outputFilePath = os.path.join('output', outputFileName)
    df.to_csv(outputFilePath)


def ConvertSIOFiles():
    fileName = os.path.join('..', 'docs', 'SIO 30.09.2022.xml')
    sheetName = 'Szkoły i placówki'
    headerRow = 5
    ConvertXMLToCSV(fileName, sheetName, headerRow)

    fileName = os.path.join('..', 'docs', 'SIO 30.09.2022.xml')
    sheetName = 'Nauczyciele'
    headerRow = 5
    ConvertXMLToCSV(fileName, sheetName, headerRow)

    fileName = os.path.join('..', 'docs', 'SIO 30.09.2021.xml')
    sheetName = 'Szkoły i placówki'
    headerRow = 5
    ConvertXMLToCSV(fileName, sheetName, headerRow)

    fileName = os.path.join('..', 'docs', 'SIO 30.09.2021.xml')
    sheetName = 'Nauczyciele'
    headerRow = 5
    ConvertXMLToCSV(fileName, sheetName, headerRow)

    fileName = os.path.join('..', 'docs', 'SIO etaty.xml')
    sheetName = 'Arkusz1'
    headerRow = 2
    ConvertXMLToCSV(fileName, sheetName, headerRow)

    fileName = os.path.join('..', 'docs', 'SIO uczniowie.xml')
    sheetName = 'Arkusz1'
    headerRow = 2
    ConvertXMLToCSV(fileName, sheetName, headerRow)
