import pandas as pd
from lxml import etree
import xml.etree.ElementTree as ET
import os

def ConvertSprawozdanie():
    # Parse the XML data
    filePath = os.path.join('..', 'docs', 'Sprawozdania[2022][IVKwartał] Dochody.xml')
    tree = ET.parse(filePath)
    root = tree.getroot()

    # Define the namespace mapping if necessary
    namespace_mapping = {
        'ns': 'urn:schemas-microsoft-com:office:spreadsheet',
    }

    # Initialize lists to store extracted data
    rows = []

    # Iterate through the <Pozycje> elements
    for pozycje_elem in root.findall('.//Pozycje'):
        pozycje_data = {}
        for pozycja_elem in pozycje_elem.findall('./Pozycja'):
            pozycja_data = {}
            for child_elem in pozycja_elem:
                pozycja_data[child_elem.tag] = child_elem.text.strip()
            pozycje_data.update(pozycja_data)
        rows.append(pozycje_data)

    # Create a DataFrame from the extracted data
    df = pd.DataFrame(rows)



def ConvertXMLtoDF(fileName, sheetName):
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


def ConvertXMLToCSV(fileName, sheetName, headerRow):
    # convert xml to pandas dataframe
    df = ConvertXMLtoDF(fileName, sheetName)
    outputFileName = os.path.split(fileName)[-1].replace('.xml', f'_{sheetName}.csv')
    print(f'Filename: {outputFileName}')

    # cut header rows
    df.columns = df.iloc[headerRow]
    df = df[headerRow+1:]

    # save dataframe as csv
    outputFilePath = os.path.join('output', outputFileName)
    df.to_csv(outputFilePath)


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
