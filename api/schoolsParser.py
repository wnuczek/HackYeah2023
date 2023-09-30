import sys
import os
# parent_dir = os.path.dirname(os.path.realpath(__file__))
# sys.path.insert(0, parent_dir)
# from .models import school
# import models.owner_type
import models.school as school

import parsers.XMLtoDFParser as xmlParser

def Parse(bytes):
    #bytes = plik ze szkołami
    fileName = os.path.abspath(r"C:\Users\ANI\source\repos\HackYeah\docs\SIO 30.09.2022.xml")
    sheetName = 'Szkoły i placówki'
    

    df = xmlParser.ParseXmlToDf(fileName, sheetName)
    df.columns = df.iloc[5]
    df = df.iloc[6:]
    
    schools = []
    for index, row in df.iterrows():
        _school = school.School(
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
        schools.append(_school)
    
    return schools

Parse(None)
    




