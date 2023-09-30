from tortoise.contrib.postgres import fields
from tortoise.models import Model

class School(Model):
    rspo = fields.IntField(pk=True)
    regon = fields.TextField()
    schoolType = fields.IntField()
    name = fields.TextField()
    address = fields.TextField()
    buildingNumber = fields.TextField()
    flaNumber = fields.TextField()
    town = fields.TextField()
    postCode = fields.TextField()
    post = fields.TextField()
    

    def __str__(self):
        return self.name
