import pandas as pd
from lxml import etree
import os
from ConvertXmlToDf import ConvertXMLtoDF

def SIO():
    fileName = os.path.join('..', 'docs', 'SIO 30.09.2022.xml')
    newFileName = os.path.join('..', 'docs', 'SIO 30.09.2022.xlsx')

    # fileName = os.path.abspath(r"C:\Users\ANI\source\repos\HackYeah\docs\SIO 30.09.2022.xml")
    # newFileName = os.path.abspath(r"C:\Users\ANI\source\repos\HackYeah\data_prep\SIO 30.09.2022.xlsx")
    

    dfSchools = Schools(fileName)
    dfTeachers = Teachers(fileName)

    # Create an ExcelWriter object
    with pd.ExcelWriter(newFileName, engine='openpyxl') as writer:

        dfSchools.to_excel(writer, sheet_name='Szkoly i placowki', index=False)

        dfTeachers.to_excel(writer,  sheet_name='Nauczyciele', index=False)




def Schools(fileName):    

    # Define the worksheet name
    sheetName = 'Szkoły i placówki'  

    df = ConvertXMLtoDF(fileName, sheetName)

    df.columns = df.iloc[5]
    df = df[6:]
    return df

def Teachers(fileName):

    # Define the worksheet name
    sheetName = 'Nauczyciele'  

    df = ConvertXMLtoDF(fileName, sheetName)
    
    df.columns = df.iloc[5]
    df = df[6:]
    return df

SIO()
