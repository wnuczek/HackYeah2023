from tortoise import fields, models
from tortoise.models import Model


class CategoryDefinition(Model):

    id = fields.IntField(pk=True)
    symbol = fields.TextField()
    year = fields.IntField()
    description = fields.TextField()
    factor = fields.DecimalField(3,3)

    def __str__(self):
        return self.symbol
