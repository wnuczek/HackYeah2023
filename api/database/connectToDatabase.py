from tortoise import Tortoise
async def connectToDatabase():
    await Tortoise.init(
        db_url='postgresql://146.59.35.43:',
        modules={'models': ['models']}
    )