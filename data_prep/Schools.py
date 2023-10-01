import requests
from requests.packages.urllib3.exceptions import InsecureRequestWarning
import pandas as pd

# Disable SSL certificate verification warning (not recommended for production)
requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

url = "http://localhost:5286/categoryvalues/computed"

payload = """{
}"""
headers = {
  'Content-Type': 'application/json'
}

response = requests.request("POST", url, headers=headers, data=payload)

df = pd.read_json(response.text)

print(df)
