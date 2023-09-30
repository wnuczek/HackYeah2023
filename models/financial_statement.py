from tortoise import fields
from tortoise.models import Model
from enum import IntEnum
import school

class FinancialStatementPeriodType(IntEnum):
    YEARLY = 1
    QUARTERLY = 2
    MONTHLY = 3

class FinancialStatement(Model):
    id = fields.IntField(pk=True)
    # schoolRegon = fields.ForeignKeyRelation(school.School, regon)
    year = fields.IntField()
    period = fields.IntField()
    periodType = fields.IntEnumField(FinancialStatementPeriodType)
    
    def __str__(self):
        
        return self.id


class FinancialStatementPosition(Model):
    id = fields.IntField(pk=True)
    # financialStatementId = fields.ForeignKeyRelation(FinancialStatement,id)
    dzial = fields.IntField()
    rozdzial = fields.IntField()
    paragraf = fields.IntField()
    P4 = fields.DecimalField(10,2)
    PL = fields.DecimalField(10,2)
    NA = fields.DecimalField(10,2)
    DW = fields.DecimalField(10,2)
    ZA = fields.DecimalField(10,2)
    WW = fields.DecimalField(10,2)

    def __str__(self):
        return self.id
    
