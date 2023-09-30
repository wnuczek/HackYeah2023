from tortoise.contrib.postgres import fields
from tortoise.models import Model

class Teacher(Model):
    id = fields.IntField(pk=True)
    name = fields.TextField()
    secondname = fields.TextField()
    surname = fields.TextField()
    created = fields.DatetimeField(auto_now_add=True)

    def __str__(self):
        return self.name
