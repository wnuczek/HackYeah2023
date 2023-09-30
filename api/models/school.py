from tortoise import fields, models
from owner_type import OwnerType
from tortoise.models import Model
from teacher import Teacher

class School(Model):
    rspo = fields.IntField(pk=True)
    regon = fields.TextField()
    schoolType = fields.IntField()
    name = fields.TextField()
    address = fields.TextField()
    buildingNumber = fields.TextField()
    flatNumber = fields.TextField()
    town = fields.TextField()
    postCode = fields.TextField()
    post = fields.TextField()
    # owner = fields.IntEnumField(OwnerType)

    def __str__(self):
        return self.name
