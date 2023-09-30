import pandas as pd
from lxml import etree
import os


def ConvertXmlToDf(fileName, sheetName):
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


def ConvertXMLToCSV(fileName, sheetName):
    # convert xml to pandas dataframe
    df = ConvertXmlToDf(fileName, sheetName)
    outputFileName = fileName.replace('.xml', '.csv')
    print(f'Filename: {outputFileName}')

		# cut header rows
    df.columns = df.iloc[5]
    df = df[6:]

		# save dataframe as csv
    df.to_csv(outputFileName)


fileName = os.path.join('..', 'docs', 'SIO 30.09.2022.xml')
sheetName = 'Szkoły i placówki'


ConvertXMLToCSV(fileName, sheetName)
