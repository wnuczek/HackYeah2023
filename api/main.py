from fastapi import FastAPI
from routers import schools
from routers import teachers
from database.connectToDatabase import connectToDatabase
from tortoise.contrib.fastapi import HTTPNotFoundError, register_tortoise


app = FastAPI()

app.include_router(schools.router)
app.include_router(teachers.router)

@app.get("/")
def read_root():
    return {"HACKYeah": ""}

register_tortoise(
    app,
    db_url="postgres://api:hackyeahapi@146.59.35.43:5432/postgres",
    modules={"models": ["models"]},
    generate_schemas=False,
    add_exception_handlers=True,
)