from tortoise import fields, models
from owner_type import OwnerType
from tortoise.models import Model


class CategoryValues(Model):
    schoolRSPO = fields.BigIntField()
    categoryId = fields.IntField()
    value = fields.DecimalField(3,3)
