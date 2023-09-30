import sys
import os
sciezka = r"C:\Users\ANI\source\repos\HackYeah\models"
sys.path.append(sciezka)
from models import school
import pandas as pd
from lxml import etree
import xml.etree.ElementTree as ET

def Parse(bytes):
    #bytes = plik ze szkołami
    fileName = os.path.abspath(r"C:\Users\ANI\source\repos\HackYeah\docs\SIO 30.09.2022.xml")
    sheetName = 'Szkoły i placówki'
    

    df = ParseXmlToDf(fileName, sheetName)
    df.columns = df.iloc[5]
    df = df.iloc[6:]
    
    schools = []
    for index, row in df.iterrows():
        school = school.School(
            rspo=row['Numer RSPO'],
            regon=row['REGON'],
            schoolType=row['Typ szkoły/placówki'],
            name=row['Nazwa szkoły/placówki'],
            address=row['Ulica'],
            buildingNumber=row['Nr domu'],
            flaNumber=row['Nr lokalu'],
            town=row['Miejscowość'],
            postCode=row['Kod pocztowy'],
            post=row['Poczta']
        )
        schools.append(school)
    
    return schools

def ParseXmlToDf(fileName, sheetName):
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

Parse(None)
    




