import pandas as pd
from lxml import etree
import os
from ConvertXmlToDf import Convert

def SIO():
    fileName = os.path.join('..', 'docs', 'SIO 30.09.2022.xml')
    fileName = '..\docs\SIO 30.09.2022.xml'
    newFileName = os.path.join('..', 'docs', 'SIO 30.09.2022.xlsx')

    dfSchools = Schools(fileName)
    dfTeachers = Teachers(fileName)

    dfSchools.to_excel(newFileName, sheet_name='Szkoly i placowki')
    dfTeachers.to_excel(newFileName,  sheet_name='Nauczyciele')

def Schools(fileName):    

    # Define the worksheet name
    sheetName = 'Szkoły i placówki'  

    df = Convert(fileName, sheetName)

    df.columns = df.iloc[5]
    df = df[6:]
    return df

def Teachers(fileName):

    # Define the worksheet name
    sheetName = 'Nauczyciele'  

    df = Convert(fileName, sheetName)
    
    df.columns = df.iloc[5]
    df = df[6:]
    return df

SIO()
