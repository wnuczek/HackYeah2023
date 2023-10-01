import requests
from requests.packages.urllib3.exceptions import InsecureRequestWarning
import pandas as pd

# Disable SSL certificate verification warning (not recommended for production)
requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

url = "http://localhost:5286/Schools"

payload = """{
}"""
headers = {
  'Content-Type': 'application/json'
}

response = requests.request("POST", url, headers=headers, data=payload)

df = pd.read_json(response.text)

df = pd.json_normalize(response, 'categories', 
                      meta=["rspo", "regon", "schooltype", "name", "address", "buildingnumber",
                            "flatnumber", "town", "postcode", "post", "owner"])

print(df)
